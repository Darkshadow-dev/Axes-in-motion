IF DB_ID(N'AxesInMotion') IS NULL
BEGIN
    CREATE DATABASE [AxesInMotion];
END;
GO

USE [AxesInMotion];
GO

IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        UserId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
        UserName NVARCHAR(80) COLLATE Latin1_General_100_CI_AS NOT NULL,
        CONSTRAINT UQ_Users_UserName UNIQUE (UserName)
    );
END;
GO

IF OBJECT_ID(N'dbo.Games', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Games
    (
        GameId INT NOT NULL CONSTRAINT PK_Games PRIMARY KEY,
        GameName NVARCHAR(200) NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.EntrySnapshots', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.EntrySnapshots
    (
        SnapshotId BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_EntrySnapshots PRIMARY KEY,
        UserId INT NOT NULL,
        GameId INT NOT NULL,
        GameName NVARCHAR(200) NOT NULL,
        SavedAt DATETIMEOFFSET(7) NOT NULL CONSTRAINT DF_EntrySnapshots_SavedAt DEFAULT (SYSUTCDATETIME()),
        CONSTRAINT FK_EntrySnapshots_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId),
        CONSTRAINT FK_EntrySnapshots_Games FOREIGN KEY (GameId) REFERENCES dbo.Games(GameId)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_EntrySnapshots_UserGameDate' AND object_id = OBJECT_ID(N'dbo.EntrySnapshots'))
BEGIN
    CREATE INDEX IX_EntrySnapshots_UserGameDate
        ON dbo.EntrySnapshots(UserId, GameId, SavedAt DESC, SnapshotId DESC);
END;
GO

IF OBJECT_ID(N'dbo.EntryValues', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.EntryValues
    (
        SnapshotId BIGINT NOT NULL,
        FieldName NVARCHAR(100) NOT NULL,
        FieldValue NVARCHAR(4000) NOT NULL,
        CONSTRAINT PK_EntryValues PRIMARY KEY (SnapshotId, FieldName),
        CONSTRAINT FK_EntryValues_EntrySnapshots FOREIGN KEY (SnapshotId)
            REFERENCES dbo.EntrySnapshots(SnapshotId) ON DELETE CASCADE
    );
END;
GO

-- Quick check after creating the schema.
SELECT DB_NAME() AS CurrentDatabase, name AS TableName
FROM sys.tables
WHERE schema_id = SCHEMA_ID(N'dbo')
ORDER BY name;
GO
