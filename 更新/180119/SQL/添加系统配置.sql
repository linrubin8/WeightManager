if not exists(select 1 from dbo.DbSysConfigField where SysConfigFieldName='AmountNotEnough')
begin
	insert dbo.DbSysConfigField
	( SysConfigFieldName, SysConfigFieldText, SysConfigDataType, SysConfigDefaultValue)
	values('AmountNotEnough','预付客户充值余额预警值',1,10000)
end