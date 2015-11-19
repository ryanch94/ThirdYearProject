alter PROC [dbo].[TodaysTimetable] 
@StudentID int, @weekdaynumber int
AS
DECLARE @CourseID int

SELECT @CourseID = CourseId
FROM CourseStudent
WHERE StudentId = @StudentID

SELECT Name, FirstName, LastName, DayBlock, Code
FROM TimeTable INNER JOIN Module on TimeTable.ModuleId = Module.Id
INNER JOIN [dbo].[User] on Module.LecturerId = [User].Id
INNER JOIN Room on TimeTable.RoomId = Room.Id
WHERE CourseId = @CourseID AND WeekDayNumber = @weekdaynumber
AND WeekNumber = DATEPART(wk, GETDATE())
ORDER BY DayBlock
