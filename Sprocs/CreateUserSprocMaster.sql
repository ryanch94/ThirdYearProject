create proc CreateUserMaster
@studentno char(9), @fname varchar(50), @lname varchar(50), @email varchar(50)
as
declare @type char(1), @number varchar(20), @prilvl tinyint, @testemail varchar(50)

select @testemail = ITemail
from [dbo].[User]
where ITemail = @email

select @type = left(@studentno,1)
select @number = substring(@studentno,2,len(@studentno)-1)

if @type='L'
begin
select @prilvl = 1
end

if (@testemail !=null)
begin
raiserror('This user is already registered',16,1)
return -1
end

