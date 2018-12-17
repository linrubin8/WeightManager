CREATE TABLE [dbo].[SysSession](
	[SessionID] [bigint] IDENTITY(1,1) NOT NULL,
	[LoginName] [varchar](50) NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[LogoutTime] [datetime] NULL,
	[ClientIP] [varchar](50) NOT NULL,
	[ClientSerial] [varchar](50) NOT NULL,
	[LastCheckTime] [datetime] NULL,
 CONSTRAINT [PK_SBSession] PRIMARY KEY CLUSTERED 
(
	[SessionID] ASC
)) ON [PRIMARY]
