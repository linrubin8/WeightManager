CREATE PROCEDURE [dbo].[SaleCarInBill_SprcialBillCode]
(
	@CustomerID				bigint,
	@ItemID					bigint,
	@BillCode				varchar(50) output
)
as
begin
	set @BillCode = ''
end