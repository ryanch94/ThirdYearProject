alter proc CreateUserMaster
@studentno char(9), @fname varchar(50), @lname varchar(50), @email varchar(50)
as
declare @type char(1), @number varchar(20), @prilvl tinyint, @testemail varchar(50), @returnval int

set transaction isolation level REPEATABLE READ
begin transaction

select @testemail = ITemail
from [dbo].[User]
where ITemail = @email

select @type = left(@studentno,1)
select @number = substring(@studentno,2,len(@studentno)-1)

if @type='L'
begin
select @prilvl = 1
end
print @testemail
if (@testemail is not null)
begin
raiserror('This user is already registered',16,1)
rollback transaction
return -1
end

EXEC @returnval = [dbo].[InsertUser]
@type = @type, 
@number = @number,
@fname = @fname,
@lname =@lname,
@email = @email,
@prilvl = @prilvl

if @returnval <> 0
begin
raiserror ('User insert failed', 16,1)
rollback transaction
return 1
end

commit transaction
raiserror ('User Inserted',16,1)
return 0