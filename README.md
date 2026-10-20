# Axes in Motion

Axes in Motion is a Windows desktop tool for recording game-specific player data. Choose a game, enter values in the form, preview what was filled in, save a timestamped snapshot, and review earlier saves. The WinForms client sends records to an ASP.NET Core API, which stores them in SQL Server.

## Open the project website

Start the API with `Start Server.cmd`, then open [http://localhost:5080](http://localhost:5080). The website describes the project, shows the client-to-database flow, includes a sample SQL query, and links to the project API and official Microsoft resources. The API also serves `/health`, `/api/info`, `/AxesInMotion.sql`, and `/ViewSavedData.sql`.

You can also start **Axes in Motion Server** in Visual Studio and browse to `http://localhost:5080`.

## Set up SQL Server

1. In SQL Server Management Studio, connect to your local server (the default connection is `localhost`).
2. Open the repository's `AxesInMotion.sql` and execute it once. It creates the `AxesInMotion` database and its tables.
3. Run `Start Server.cmd`, or start **Axes in Motion Server** from Visual Studio. The API listens on `http://localhost:5080` by default.
4. Start **Axes in Motion** in Visual Studio. Enter a user name, select a game, fill in the values in the table, and click **Save all data**.
5. Open `ViewSavedData.sql` in SSMS and execute it to view recent saved fields.

The API uses Windows authentication with `Server=localhost;Database=AxesInMotion;Integrated Security=True`. The Windows account running the API needs permission to read and write the database. For a different SQL Server instance, set `AXES_IN_MOTION_SQL_CONNECTION` before starting the server. To change the API address, set `AXES_IN_MOTION_SERVER_URLS` for the server or `AXES_IN_MOTION_API_URL` for the desktop client.

## Query saved data

Connect a new SSMS query window to `AxesInMotion`, then run:

```sql
SELECT
    u.UserName,
    s.GameName,
    s.SavedAt,
    v.FieldName,
    v.FieldValue
FROM dbo.EntrySnapshots AS s
JOIN dbo.Users AS u ON u.UserId = s.UserId
JOIN dbo.EntryValues AS v ON v.SnapshotId = s.SnapshotId
ORDER BY s.SavedAt DESC;
```

The database stores users in `dbo.Users`, each submission in `dbo.EntrySnapshots`, and the fields for that submission in `dbo.EntryValues`.

## API routes

- `GET /health` checks the API connection and expected database tables.
- `GET /api/info` describes the API and its data routes.
- `GET /api/history?userName=ahmed` returns a user's saved snapshots.
- `GET /api/entries/latest?userName=ahmed&gameId=1` returns the latest save for a game.
- `POST /api/entries` saves a snapshot and its field values.
