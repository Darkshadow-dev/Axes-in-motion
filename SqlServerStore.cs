using System.Data;
using Microsoft.Data.SqlClient;

namespace Axes_in_Motion_Server;

public sealed class SaveEntryRequest
{
    public string UserName { get; set; } = "";
    public int GameId { get; set; }
    public string GameName { get; set; } = "";
    public Dictionary<string, string> Values { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}

public sealed class SavedEntryDto
{
    public long SnapshotId { get; set; }
    public string UserName { get; set; } = "";
    public int GameId { get; set; }
    public string GameName { get; set; } = "";
    public DateTimeOffset SavedAt { get; set; }
    public Dictionary<string, string> Values { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}

public sealed class SqlServerStore
{
    private readonly string _connectionString;

    public SqlServerStore(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<bool> CheckDatabaseAsync(CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand("""
            SELECT CASE
                WHEN DB_NAME() = N'AxesInMotion'
                    AND OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL
                    AND OBJECT_ID(N'dbo.Games', N'U') IS NOT NULL
                    AND OBJECT_ID(N'dbo.EntrySnapshots', N'U') IS NOT NULL
                    AND OBJECT_ID(N'dbo.EntryValues', N'U') IS NOT NULL
                THEN 1 ELSE 0
            END;
            """, connection);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) == 1;
    }

    public async Task<SavedEntryDto> SaveAsync(SaveEntryRequest request, CancellationToken cancellationToken)
    {
        string userName = request.UserName.Trim();
        string gameName = request.GameName.Trim();
        var values = request.Values
            .Where(pair => !string.IsNullOrWhiteSpace(pair.Key) && pair.Value is not null)
            .ToDictionary(pair => pair.Key.Trim(), pair => pair.Value.Trim(), StringComparer.OrdinalIgnoreCase);

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

        try
        {
            int userId;
            await using (var userCommand = new SqlCommand("""
                DECLARE @UserId INT;
                SELECT @UserId = UserId
                FROM dbo.Users WITH (UPDLOCK, HOLDLOCK)
                WHERE UserName = @UserName;
                IF @UserId IS NULL
                BEGIN
                    INSERT INTO dbo.Users(UserName) VALUES (@UserName);
                    SET @UserId = CONVERT(INT, SCOPE_IDENTITY());
                END;
                SELECT @UserId;
                """, connection, transaction))
            {
                userCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 80).Value = userName;
                userId = Convert.ToInt32(await userCommand.ExecuteScalarAsync(cancellationToken));
            }

            await using (var gameCommand = new SqlCommand("""
                UPDATE dbo.Games WITH (UPDLOCK, HOLDLOCK)
                SET GameName = @GameName
                WHERE GameId = @GameId;
                IF @@ROWCOUNT = 0
                    INSERT INTO dbo.Games(GameId, GameName) VALUES (@GameId, @GameName);
                """, connection, transaction))
            {
                gameCommand.Parameters.Add("@GameId", SqlDbType.Int).Value = request.GameId;
                gameCommand.Parameters.Add("@GameName", SqlDbType.NVarChar, 200).Value = gameName;
                await gameCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            long snapshotId;
            DateTimeOffset savedAt;
            await using (var snapshotCommand = new SqlCommand("""
                INSERT INTO dbo.EntrySnapshots(UserId, GameId, GameName, SavedAt)
                OUTPUT INSERTED.SnapshotId, INSERTED.SavedAt
                VALUES (@UserId, @GameId, @GameName, SYSUTCDATETIME());
                """, connection, transaction))
            {
                snapshotCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                snapshotCommand.Parameters.Add("@GameId", SqlDbType.Int).Value = request.GameId;
                snapshotCommand.Parameters.Add("@GameName", SqlDbType.NVarChar, 200).Value = gameName;
                await using SqlDataReader reader = await snapshotCommand.ExecuteReaderAsync(cancellationToken);
                if (!await reader.ReadAsync(cancellationToken)) throw new InvalidOperationException("SQL Server did not return the saved record ID.");
                snapshotId = reader.GetInt64(0);
                savedAt = reader.GetFieldValue<DateTimeOffset>(1);
            }

            await using (var valueCommand = new SqlCommand("""
                INSERT INTO dbo.EntryValues(SnapshotId, FieldName, FieldValue)
                VALUES (@SnapshotId, @FieldName, @FieldValue);
                """, connection, transaction))
            {
                valueCommand.Parameters.Add("@SnapshotId", SqlDbType.BigInt).Value = snapshotId;
                SqlParameter fieldNameParameter = valueCommand.Parameters.Add("@FieldName", SqlDbType.NVarChar, 100);
                SqlParameter fieldValueParameter = valueCommand.Parameters.Add("@FieldValue", SqlDbType.NVarChar, 4000);

                foreach ((string fieldName, string fieldValue) in values)
                {
                    fieldNameParameter.Value = fieldName;
                    fieldValueParameter.Value = fieldValue;
                    await valueCommand.ExecuteNonQueryAsync(cancellationToken);
                }
            }

            await transaction.CommitAsync(cancellationToken);
            return new SavedEntryDto
            {
                SnapshotId = snapshotId,
                UserName = userName,
                GameId = request.GameId,
                GameName = gameName,
                SavedAt = savedAt,
                Values = values
            };
        }
        catch
        {
            await transaction.RollbackAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IReadOnlyList<SavedEntryDto>> GetHistoryAsync(string userName, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand("""
            SELECT s.SnapshotId, u.UserName, s.GameId, s.GameName, s.SavedAt, v.FieldName, v.FieldValue
            FROM dbo.EntrySnapshots AS s
            INNER JOIN dbo.Users AS u ON u.UserId = s.UserId
            LEFT JOIN dbo.EntryValues AS v ON v.SnapshotId = s.SnapshotId
            WHERE u.UserName = @UserName
            ORDER BY s.SavedAt DESC, s.SnapshotId DESC, v.FieldName;
            """, connection);
        command.Parameters.Add("@UserName", SqlDbType.NVarChar, 80).Value = userName;

        var entries = new List<SavedEntryDto>();
        SavedEntryDto? current = null;
        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            long snapshotId = reader.GetInt64(0);
            if (current is null || current.SnapshotId != snapshotId)
            {
                current = ReadEntry(reader);
                entries.Add(current);
            }

            if (!reader.IsDBNull(5)) current.Values[reader.GetString(5)] = reader.GetString(6);
        }

        return entries;
    }

    public async Task<SavedEntryDto?> GetLatestAsync(string userName, int gameId, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand("""
            SELECT s.SnapshotId, u.UserName, s.GameId, s.GameName, s.SavedAt, v.FieldName, v.FieldValue
            FROM dbo.EntrySnapshots AS s
            INNER JOIN dbo.Users AS u ON u.UserId = s.UserId
            LEFT JOIN dbo.EntryValues AS v ON v.SnapshotId = s.SnapshotId
            WHERE s.SnapshotId =
            (
                SELECT TOP (1) latest.SnapshotId
                FROM dbo.EntrySnapshots AS latest
                INNER JOIN dbo.Users AS owner ON owner.UserId = latest.UserId
                WHERE owner.UserName = @UserName AND latest.GameId = @GameId
                ORDER BY latest.SavedAt DESC, latest.SnapshotId DESC
            )
            ORDER BY v.FieldName;
            """, connection);
        command.Parameters.Add("@UserName", SqlDbType.NVarChar, 80).Value = userName;
        command.Parameters.Add("@GameId", SqlDbType.Int).Value = gameId;

        SavedEntryDto? entry = null;
        await using SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            entry ??= ReadEntry(reader);
            if (!reader.IsDBNull(5)) entry.Values[reader.GetString(5)] = reader.GetString(6);
        }

        return entry;
    }

    private static SavedEntryDto ReadEntry(SqlDataReader reader) => new()
    {
        SnapshotId = reader.GetInt64(0),
        UserName = reader.GetString(1),
        GameId = reader.GetInt32(2),
        GameName = reader.GetString(3),
        SavedAt = reader.GetFieldValue<DateTimeOffset>(4)
    };
}
