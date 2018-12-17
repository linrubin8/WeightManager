
CREATE TABLE [dbo].[DbCustomerType](
	[CustomerTypeID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerTypeCode] [nvarchar](50) NULL,
	[CustomerTypeName] [nvarchar](50) NULL,
	[CreateBy] varchar(50) NULL,
	CreateTime datetime NULL,
	ChangeBy varchar(50) NULL,
	ChangeTime datetime NULL,
 CONSTRAINT [PK_DbCustomerType] PRIMARY KEY CLUSTERED 
(
	[CustomerTypeID] ASC
)) ON [PRIMARY]

GO


alter table dbo.DbCustomer
	add CustomerTypeID bigint