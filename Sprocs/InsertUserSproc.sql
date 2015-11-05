alter proc InsertUser
@type char(1), @number varchar(20),@fname varchar(50), @lname varchar(50), @email varchar(50), @prilvl tinyint
As
begin try
insert into [dbo].[User]
(SLType, Number, FirstName,LastName,ITemail,Notifications,PrivacyLvl)
values
(@type, @number, @fname, @lname, @email,0, @prilvl)
end try
begin catch
return 99
end catch
return 0