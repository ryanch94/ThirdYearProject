alter PROC [dbo].[CourseWeek]
@CourseID int, @weeknumber int
AS

SELECT Name, ShortName, FirstName, LastName, DayBlock, Code,ClassLength
FROM TimeTable INNER JOIN Module on TimeTable.ModuleId = Module.Id
INNER JOIN [dbo].[User] on Module.LecturerId = [User].Id
INNER JOIN Room on TimeTable.RoomId = Room.Id
WHERE CourseId = @CourseID
AND WeekNumber = @weeknumber
ORDER BY StartTime,DayBlock