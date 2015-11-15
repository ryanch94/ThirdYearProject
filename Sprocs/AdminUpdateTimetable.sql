alter proc adminupdatetimetable
as
declare @dayweekcalc tinyint, @starttime smalldatetime, @timetableid int, @whilecount int=0,@daynumber tinyint

while (@whilecount <= (select count(*) from dbo.TimeTable))
begin
select @whilecount+=1
select @starttime=StartTime,
@daynumber=WeekDayNumber
from dbo.TimeTable
where Id=@whilecount

if (@daynumber != null)
begin
select @dayweekcalc= DATEPART(dw,@starttime)

update dbo.TimeTable
set WeekDayNumber=@dayweekcalc
where Id=@whilecount
end

continue
end

select @starttime
select @timetableid
select @dayweekcalc