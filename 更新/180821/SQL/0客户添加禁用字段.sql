alter table dbo.DbCustomer
	add ForbidBy	varchar(50)
	
alter table dbo.DbCustomer
	add ForbidTime	datetime

alter table dbo.DbCustomer
	alter column IsForbid tinyint null