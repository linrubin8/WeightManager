alter table dbo.SaleCarInBill
	add IsSynchronousToK3OutBill	tinyint

alter table dbo.SaleCarInBill
	add SynchronousK3ByOutBill	varchar(50)
	
alter table dbo.SaleCarInBill
	add SynchronousToTimeOutBill	datetime

alter table dbo.SaleCarInBill
	add SynchronousK3OutBillResult	varchar(2000)
	
alter table dbo.SaleCarInBill
	add IsSynchronousToK3Receive	tinyint

alter table dbo.SaleCarInBill
	add SynchronousK3ByReceive	varchar(50)
	
alter table dbo.SaleCarInBill
	add SynchronousToTimeReceive	datetime

alter table dbo.SaleCarInBill
	add SynchronousK3ReceiveResult	varchar(2000)