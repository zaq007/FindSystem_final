CREATE PROCEDURE [dbo].[GetCurrentTask]
	@userId int
AS
	SELECT t.* FROM CurrentTasks ct
		LEFT JOIN Tasks t ON t.Id = ct.TaskId
	WHERE ct.UserId = @userId  
RETURN 0
