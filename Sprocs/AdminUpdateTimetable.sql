alter proc adminupdatetimetable
as
declare @dayweekcalc tinyint, @weekcalc tinyint, @starttime smalldatetime, @timetableid int
DECLARE @whilecount int=0, @dayblock tinyint

while (@whilecount <= (select count(*) from dbo.TimeTable))
begin
select @whilecount+=1
select @starttime=StartTime
from dbo.TimeTable
where Id=@whilecount

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

update dbo.TimeTable
set WeekDayNumber=@dayweekcalc,WeekNumber=@weekcalc,DayBlock=@dayblock
where Id=@whilecount

continue
end

select @starttime
select @timetableid
select @dayweekcalc