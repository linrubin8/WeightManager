alter PROCEDURE [dbo].[SaleCarInBill_SprcialBillCode]
(
	@CustomerID				bigint,
	@ItemID					bigint,
	@BillCode				varchar(50) output
)
as
begin
	set @BillCode = ''
	
	declare @CustomerTypeCode varchar(10)
	declare @LastSaleCarInBillCode varchar(50)
	declare @NewCodeIndex	int
	declare @NewCodeIndexStr	varchar(50)
	declare @DateStr	varchar(10)
	
	select @CustomerTypeCode = rtrim(CustomerTypeCode)
	from dbo.DbCustomerType t
		inner join dbo.DbCustomer c on
			c.CustomerTypeID = t.CustomerTypeID
	where c.CustomerID = @CustomerID
	
	set @CustomerTypeCode = ISNULL(@CustomerTypeCode,'')
	set @DateStr =@CustomerTypeCode+'J'+SUBSTRING( convert(varchar(8),getdate(),112),1,6 )+'-'
	
	select top 1 @LastSaleCarInBillCode = SaleCarInBillCode
	from dbo.SaleCarInBill
	where SaleCarInBillCode like @DateStr+'%'
	order by SaleCarInBillCode desc
		
	if isnull(@LastSaleCarInBillCode,'')<>''
	begin
		set @NewCodeIndex = cast(REPLACE(@LastSaleCarInBillCode,@DateStr,'') as int)+1
		
		if @NewCodeIndex<10
			set @NewCodeIndexStr = '000'+CAST(@NewCodeIndex as varchar(10))
		else if @NewCodeIndex<100
			set @NewCodeIndexStr = '00'+CAST(@NewCodeIndex as varchar(10))
		else if @NewCodeIndex<1000
			set @NewCodeIndexStr = '0'+CAST(@NewCodeIndex as varchar(10))
		else
			set @NewCodeIndexStr = CAST(@NewCodeIndex as varchar(10))
			
		set @BillCode = @DateStr+@NewCodeIndexStr
	end
	else
	begin
		set @BillCode = @DateStr+'0001'
	end
end