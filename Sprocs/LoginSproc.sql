alter proc Loginsproc
@email varchar(50), @userid int output, @fname varchar(50) output
as 

set transaction isolation level READ UNCOMMITTED
begin transaction

select @userid = Id,
@fname = FirstName
from [dbo].[User]
where ITemail = @email

if ((@userid is null)or(@fname is null))
begin
raiserror('User does not exist',16,1)
rollback transaction
return -1
end

commit transaction
raiserror ('Login succesful',16,1)
return 0