if not exists(select 1 from dbo.DbSystemConst where FieldName = 'ReceiveType' and ConstValue = 5)
begin
	insert DbSystemConst( FieldName, ConstValue, ConstText)
	values('ReceiveType',5,'Œ¢–≈÷ß∏∂±¶')
end