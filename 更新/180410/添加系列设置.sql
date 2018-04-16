if not exists(
	select 1 
	from dbo.DbSysConfigField
	where SysConfigFieldName = 'SaleInBillCode')
begin
	insert dbo.DbSysConfigField( SysConfigFieldName, SysConfigFieldText, SysConfigDataType, SysConfigDefaultValue)
	values('SaleInBillCode','入场单编码前缀',0,'JC')
end

if not exists(
	select 1 
	from dbo.DbSysConfigField
	where SysConfigFieldName = 'SaleOutBillCode')
begin
	insert dbo.DbSysConfigField( SysConfigFieldName, SysConfigFieldText, SysConfigDataType, SysConfigDefaultValue)
	values('SaleOutBillCode','出场单编码前缀',0,'XS')
end

if not exists(
	select 1 
	from dbo.DbSysConfigField
	where SysConfigFieldName = 'SaleReturnBillCode')
begin
	insert dbo.DbSysConfigField( SysConfigFieldName, SysConfigFieldText, SysConfigDataType, SysConfigDefaultValue)
	values('SaleReturnBillCode','退货单编码前缀',0,'TH')
end