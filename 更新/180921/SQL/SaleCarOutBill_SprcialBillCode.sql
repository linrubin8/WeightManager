CREATE PROCEDURE [dbo].[SaleCarOutBill_SprcialBillCode]
(
	@CustomerID				bigint,
	@ItemID					bigint,
	@BillCode				varchar(50) output
)
as
begin
	set @BillCode = ''
end