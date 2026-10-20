USE [AxesInMotion];
GO

SELECT TOP (500)
    u.UserName,
    s.GameName,
    s.SavedAt,
    v.FieldName,
    v.FieldValue
FROM dbo.EntrySnapshots AS s
INNER JOIN dbo.Users AS u ON u.UserId = s.UserId
INNER JOIN dbo.EntryValues AS v ON v.SnapshotId = s.SnapshotId
ORDER BY s.SavedAt DESC, s.SnapshotId DESC, v.FieldName;
GO
