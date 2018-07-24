alter table dbo.SaleCarInBill
	add IsSynchronousToServer tinyint
	
go

alter table dbo.SaleCarInBill
	add SynchronousToServerTime datetime

go
alter table dbo.SaleCarInBill
	add SaleCarInBillIDFromClient	bigint
	
go
alter table dbo.SaleCarOutBill
	add SaleCarOutBillIDFromClient	bigint



