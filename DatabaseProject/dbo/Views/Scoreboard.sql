CREATE VIEW [dbo].[Scoreboard]
	AS
SELECT dbo.UserProfiles.Id,
	dbo.UserProfiles.UserName,
	dbo.States.Position,
	CASE WHEN dbo.UserProfiles.EndTime IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) END AS 'IsFinished'
FROM dbo.States 
	INNER JOIN dbo.UserProfiles ON dbo.States.UserId = dbo.UserProfiles.Id
