CREATE TABLE [dbo].[SaleCarBillRemoved](
	[SaleCarBillRemovedID] [bigint] IDENTITY(1,1) NOT NULL,
	[SaleInBillRemoveJson] [ntext] NULL,
	[SaleOutBillRemoveJson] [ntext] NULL,
	[RemovedTime] [datetime] NULL,
	[RemovedBy] [varchar](50) NULL,
 CONSTRAINT [PK_SaleCarBillRemoved] PRIMARY KEY CLUSTERED 
(
	[SaleCarBillRemovedID] ASC
)) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
