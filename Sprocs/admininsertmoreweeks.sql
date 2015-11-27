alter proc [dbo].[admininsertmoreweeks]
as
declare @starttime smalldatetime, @timetableid int,@dayweekcalc tinyint, @weekcalc tinyint,@dayblock tinyint
DECLARE @whilecount int=0, @moduleid int, @roomid int, @courseid int, @classlength int

DECLARE @MyCursor CURSOR
DECLARE @MyField int
BEGIN
    SET @MyCursor = CURSOR FOR
	SELECT cur.*
	FROM
	(
    select top 28 Id from dbo.TimeTable
	order by Id desc
	) cur
	ORDER BY cur.Id ASC

    OPEN @MyCursor 
    FETCH NEXT FROM @MyCursor 
    INTO @MyField

    WHILE @@FETCH_STATUS = 0
    BEGIN
    select @moduleid = ModuleId,
@roomid = RoomId,
@courseid = CourseId,
@starttime = StartTime,
@classlength= ClassLength
from dbo.TimeTable
where Id=@MyField

select @starttime=DATEADD(day,7,@starttime)
select @dayweekcalc= DATEPART(dw,@starttime)
select @weekcalc= DATEPART(wk,@starttime)
set @dayblock=
case
when DATEPART(hour,@starttime) = 09 then 1
when DATEPART(hour,@starttime) = 10 then 2
when DATEPART(hour,@starttime) = 11 then 3
when DATEPART(hour,@starttime) = 12 then 4
when DATEPART(hour,@starttime) = 13 then 5
when DATEPART(hour,@starttime) = 14 then 6
when DATEPART(hour,@starttime) = 15 then 7
when DATEPART(hour,@starttime) = 16 then 8
when DATEPART(hour,@starttime) = 17 then 9
end

insert into dbo.TimeTable
(ModuleId,RoomId,CourseId,StartTime,WeekDayNumber,WeekNumber,DayBlock,ClassLength)
values
(@moduleid,@roomid,@courseid,@starttime,@dayweekcalc,@weekcalc,@dayblock,@classlength)
      FETCH NEXT FROM @MyCursor 
      INTO @MyField 
    END; 

    CLOSE @MyCursor ;
    DEALLOCATE @MyCursor;
END;

--while (@whilecount <= 28)
--begin
--select @whilecount+=1
--select @moduleid = ModuleId,
--@roomid = RoomId,
--@courseid = CourseId,
--@starttime = StartTime,
--@classlength= ClassLength
--from dbo.TimeTable
--where Id=@whilecount

--select @starttime=DATEADD(day,7,@starttime)
--select @dayweekcalc= DATEPART(dw,@starttime)
--select @weekcalc= DATEPART(wk,@starttime)
--set @dayblock=
--case
--when DATEPART(hour,@starttime) = 09 then 1
--when DATEPART(hour,@starttime) = 10 then 2
--when DATEPART(hour,@starttime) = 11 then 3
--when DATEPART(hour,@starttime) = 12 then 4
--when DATEPART(hour,@starttime) = 13 then 5
--when DATEPART(hour,@starttime) = 14 then 6
--when DATEPART(hour,@starttime) = 15 then 7
--when DATEPART(hour,@starttime) = 16 then 8
--when DATEPART(hour,@starttime) = 17 then 9
--end

--insert into dbo.TimeTable
--(ModuleId,RoomId,CourseId,StartTime,WeekDayNumber,WeekNumber,DayBlock,ClassLength)
--values
--(@moduleid,@roomid,@courseid,@starttime,@dayweekcalc,@weekcalc,@dayblock,@classlength)

--continue
--end