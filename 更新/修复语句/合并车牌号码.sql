declare @DataCar table
(
	CarNum varchar(100)
)

insert @DataCar(CarNum)
select CarNum
from DbCar
group by CarNum
having COUNT(CarNum)>1

declare @CarNum	varchar(100)
declare @CarID	bigint

declare	Detail_Cursor_PurchaseQty cursor local static read_only
for	select	CarNum
	from	@DataCar
	open	Detail_Cursor_PurchaseQty
				
	while	1 = 1
	begin
		fetch next from Detail_Cursor_PurchaseQty
		into	@CarNum
				
		if	@@fetch_status <> 0
			break
			
		select top 1 @CarID = CarID
		from DbCar 
		where RTRIM(CarNum)=RTRIM(@CarNum)
		
		update d
		set CarID = @CarID
		from dbo.SaleCarInBill d
		where CarID in (
			select CarID 
			from DbCar 
			where CarID<>@CarID and RTRIM(CarNum)=RTRIM(@CarNum) 
			)
		
		update d
		set CarID = @CarID
		from dbo.SaleCarOutBill d
		where CarID in (
			select CarID 
			from DbCar 
			where CarID<>@CarID and RTRIM(CarNum)=RTRIM(@CarNum) 
			)
		
		update d
		set CarID = @CarID
		from dbo.ModifyBillDetail d
		where CarID in (
			select CarID 
			from DbCar 
			where CarID<>@CarID and RTRIM(CarNum)=RTRIM(@CarNum) 
			)	
			
		delete dbo.DbCustomerCar
		where CarID in (
			select CarID 
			from DbCar 
			where CarID<>@CarID and RTRIM(CarNum)=RTRIM(@CarNum) 
			) 
		
		delete dbo.DbCarWeight
		where CarID in (
			select CarID 
			from DbCar 
			where CarID<>@CarID and RTRIM(CarNum)=RTRIM(@CarNum) 
			) 
		
		delete dbo.DbCar
		where CarID<>@CarID and RTRIM(CarNum)=RTRIM(@CarNum) 
	end
close Detail_Cursor_PurchaseQty
deallocate Detail_Cursor_PurchaseQty