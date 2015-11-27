alter PROC [dbo].[RoomWeek]
@RoomID int, @weeknumber int
AS

SELECT distinct Name, ShortName, FirstName, LastName, DayBlock, Code, ClassLength, StartTime --StartTime has to be returned by distinct
FROM TimeTable INNER JOIN Module on TimeTable.ModuleId = Module.Id
INNER JOIN [dbo].[User] on Module.LecturerId = [User].Id
INNER JOIN Room on TimeTable.RoomId = Room.Id
WHERE Room.Id = @RoomID
AND WeekNumber = @weeknumber
ORDER BY StartTime,DayBlock