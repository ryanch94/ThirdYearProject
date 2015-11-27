alter PROC [dbo].[ModuleWeek]
@StudentID int, @moduleID int, @weeknumber int
AS
DECLARE @CourseID int

SELECT @CourseID = CourseId
FROM CourseStudent
WHERE StudentId = @StudentID


SELECT Name, ShortName, FirstName, LastName, DayBlock, Code, ClassLength
FROM TimeTable INNER JOIN Module on TimeTable.ModuleId = Module.Id
INNER JOIN [dbo].[User] on Module.LecturerId = [User].Id
INNER JOIN Room on TimeTable.RoomId = Room.Id
WHERE  CourseId = @CourseID
AND WeekNumber = @weeknumber
AND ModuleId = @moduleID
ORDER BY StartTime,DayBlock