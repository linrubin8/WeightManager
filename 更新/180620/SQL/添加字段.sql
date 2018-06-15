alter table dbo.DbCustomer
	add K3CustomerCode	varchar(100)
go
alter table dbo.ModifyBillDetail
	add MaterialPrice	numeric(18,5)
go
alter table dbo.ModifyBillDetail
	add FarePrice	numeric(18,5)
go
alter table dbo.ModifyBillDetail
	add TaxPrice	numeric(18,5)
go
alter table dbo.ModifyBillDetail
	add BrokerPrice	numeric(18,5)
go
alter table dbo.SaleCarOutBill
	add MaterialPrice	numeric(18,5)
go
alter table dbo.SaleCarOutBill
	add FarePrice	numeric(18,5)
go
alter table dbo.SaleCarOutBill
	add TaxPrice	numeric(18,5)
go
alter table dbo.SaleCarOutBill
	add BrokerPrice	numeric(18,5)
go
alter  table dbo.SaleCarOutBill
	add ChangedBy	varchar(100)
go
alter  table dbo.SaleCarOutBill
	add ChangeTime	varchar(100)
go
alter  table dbo.SaleCarOutBill
	add ChangeDetail	varchar(1000)
