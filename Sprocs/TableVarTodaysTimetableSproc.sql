alter proc TodaysTimetable 1
@userid int
--@outmodulename varchar(75) output,
--@outfname varchar(50) output,
--@outlname varchar(50) output,
--@outroomcode char(5) output,
--@outdayblock tinyint output
as
declare @moduleid int,@modulename varchar(75), @lectureid int, @fname varchar(50), @lname varchar(50)
declare @courseid int, @roomid int, @roomcode char(5), @startime smalldatetime
declare @weeknum tinyint, @daynum tinyint, @dayblock tinyint

CREATE TABLE ##LocalTempTable(
Module varchar(75),
Lecturer varchar(100), 
RoomCode char(5),
DayBlock tinyint)

--Gives current week and Day
select @weeknum = 47 --DATEPART(wk,getdate())
select @daynum = DATEPART(dw,getdate())

--reads course student is in
select @courseid = CourseId
from dbo.CourseStudent
where StudentId = @userid

--reads Variables for output or to get Variables for other read
select 
@moduleid = ModuleStudent.ModuleId,
@modulename = Name,
@lectureid = LecturerId,
@roomid = TimeTable.RoomId,
@dayblock = DayBlock

from dbo.ModuleStudent
inner join dbo.Module
on ModuleStudent.ModuleId=Module.Id
inner join dbo.TimeTable
on Module.Id=TimeTable.ModuleId
where ModuleStudent.StudentId = @userid
and TimeTable.CourseId=@courseid
and WeekNumber = @weeknum
and WeekDayNumber = @daynum

--reads lecture name for module
select 
@fname = FirstName,
@lname = LastName
from [dbo].[User]
where Id=@lectureid

--reads roomcode
select 
@roomcode = Code
from [dbo].[Room]
where Id=@roomid



--temp output
insert into ##LocalTempTable
(Module,Lecturer,RoomCode,DayBlock)
values (@modulename,@fname+' '+@lname,@roomcode,@dayblock)

select *
from ##LocalTempTable

drop table ##LocalTempTable