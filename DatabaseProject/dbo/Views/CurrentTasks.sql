CREATE VIEW [dbo].[CurrentTasks]
	AS 
SELECT S.UserId, 
	PT.TaskId
FROM dbo.States S
	LEFT JOIN dbo.Path_Task PT ON S.Position = PT.Position AND S.PathId = PT.PathId
