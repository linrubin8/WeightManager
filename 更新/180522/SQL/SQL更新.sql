delete dbo.DbSystemConst
delete dbo.DbBillType
delete dbo.DbPermissionData
delete dbo.DbPermission
delete dbo.DbSysConfigField
delete dbo.SysSPType
delete dbo.SysViewType

if not exists(select 1 from DbSystemConst where SystemConstID = 1)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(1,'UserType',0,'地磅文员')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UserType',
ConstValue=0,
ConstText='地磅文员'
    where SystemConstID = 1
end

if not exists(select 1 from DbSystemConst where SystemConstID = 2)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(2,'UserType',1,'办公室文员')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UserType',
ConstValue=1,
ConstText='办公室文员'
    where SystemConstID = 2
end

if not exists(select 1 from DbSystemConst where SystemConstID = 3)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(3,'UserType',2,'超级管理员')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UserType',
ConstValue=2,
ConstText='超级管理员'
    where SystemConstID = 3
end

if not exists(select 1 from DbSystemConst where SystemConstID = 4)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(4,'UserSex',0,'男')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UserSex',
ConstValue=0,
ConstText='男'
    where SystemConstID = 4
end

if not exists(select 1 from DbSystemConst where SystemConstID = 6)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(6,'UserSex',1,'女')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UserSex',
ConstValue=1,
ConstText='女'
    where SystemConstID = 6
end

if not exists(select 1 from DbSystemConst where SystemConstID = 7)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(7,'PermissionType',0,'操作')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='PermissionType',
ConstValue=0,
ConstText='操作'
    where SystemConstID = 7
end

if not exists(select 1 from DbSystemConst where SystemConstID = 8)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(8,'PermissionType',1,'查询')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='PermissionType',
ConstValue=1,
ConstText='查询'
    where SystemConstID = 8
end

if not exists(select 1 from DbSystemConst where SystemConstID = 9)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(9,'BackUpType',0,'每周备份一次')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpType',
ConstValue=0,
ConstText='每周备份一次'
    where SystemConstID = 9
end

if not exists(select 1 from DbSystemConst where SystemConstID = 10)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(10,'BackUpType',1,'每天备份一次')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpType',
ConstValue=1,
ConstText='每天备份一次'
    where SystemConstID = 10
end

if not exists(select 1 from DbSystemConst where SystemConstID = 11)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(11,'BackUpWeek',1,'一')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpWeek',
ConstValue=1,
ConstText='一'
    where SystemConstID = 11
end

if not exists(select 1 from DbSystemConst where SystemConstID = 13)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(13,'BackUpWeek',2,'二')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpWeek',
ConstValue=2,
ConstText='二'
    where SystemConstID = 13
end

if not exists(select 1 from DbSystemConst where SystemConstID = 14)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(14,'BackUpWeek',3,'三')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpWeek',
ConstValue=3,
ConstText='三'
    where SystemConstID = 14
end

if not exists(select 1 from DbSystemConst where SystemConstID = 15)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(15,'BackUpWeek',4,'四')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpWeek',
ConstValue=4,
ConstText='四'
    where SystemConstID = 15
end

if not exists(select 1 from DbSystemConst where SystemConstID = 16)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(16,'BackUpWeek',5,'五')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpWeek',
ConstValue=5,
ConstText='五'
    where SystemConstID = 16
end

if not exists(select 1 from DbSystemConst where SystemConstID = 17)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(17,'BackUpWeek',6,'六')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpWeek',
ConstValue=6,
ConstText='六'
    where SystemConstID = 17
end

if not exists(select 1 from DbSystemConst where SystemConstID = 18)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(18,'BackUpWeek',7,'日')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BackUpWeek',
ConstValue=7,
ConstText='日'
    where SystemConstID = 18
end

if not exists(select 1 from DbSystemConst where SystemConstID = 19)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(19,'AmountType',0,'整数')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='AmountType',
ConstValue=0,
ConstText='整数'
    where SystemConstID = 19
end

if not exists(select 1 from DbSystemConst where SystemConstID = 20)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(20,'AmountType',1,'保留一位小数')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='AmountType',
ConstValue=1,
ConstText='保留一位小数'
    where SystemConstID = 20
end

if not exists(select 1 from DbSystemConst where SystemConstID = 21)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(21,'AmountType',2,'保留两位小数')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='AmountType',
ConstValue=2,
ConstText='保留两位小数'
    where SystemConstID = 21
end

if not exists(select 1 from DbSystemConst where SystemConstID = 22)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(22,'ReceiveType',0,'现金')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReceiveType',
ConstValue=0,
ConstText='现金'
    where SystemConstID = 22
end

if not exists(select 1 from DbSystemConst where SystemConstID = 23)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(23,'ReceiveType',1,'预付')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReceiveType',
ConstValue=1,
ConstText='预付'
    where SystemConstID = 23
end

if not exists(select 1 from DbSystemConst where SystemConstID = 24)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(24,'ReceiveType',2,'挂账')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReceiveType',
ConstValue=2,
ConstText='挂账'
    where SystemConstID = 24
end

if not exists(select 1 from DbSystemConst where SystemConstID = 25)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(25,'UOMType',0,'体积')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UOMType',
ConstValue=0,
ConstText='体积'
    where SystemConstID = 25
end

if not exists(select 1 from DbSystemConst where SystemConstID = 26)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(26,'UOMType',1,'重量')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UOMType',
ConstValue=1,
ConstText='重量'
    where SystemConstID = 26
end

if not exists(select 1 from DbSystemConst where SystemConstID = 27)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(27,'UOMType',2,'数量')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='UOMType',
ConstValue=2,
ConstText='数量'
    where SystemConstID = 27
end

if not exists(select 1 from DbSystemConst where SystemConstID = 29)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(29,'CalculateType',0,'按重量计价')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='CalculateType',
ConstValue=0,
ConstText='按重量计价'
    where SystemConstID = 29
end

if not exists(select 1 from DbSystemConst where SystemConstID = 30)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(30,'CalculateType',1,'按车辆计价')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='CalculateType',
ConstValue=1,
ConstText='按车辆计价'
    where SystemConstID = 30
end

if not exists(select 1 from DbSystemConst where SystemConstID = 31)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(31,'WeightDeviceType',0,'自定义')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='WeightDeviceType',
ConstValue=0,
ConstText='自定义'
    where SystemConstID = 31
end

if not exists(select 1 from DbSystemConst where SystemConstID = 32)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(32,'WeightDeviceType',1,'柯力')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='WeightDeviceType',
ConstValue=1,
ConstText='柯力'
    where SystemConstID = 32
end

if not exists(select 1 from DbSystemConst where SystemConstID = 33)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(33,'DeviceFrameType',0,'ASCII')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceFrameType',
ConstValue=0,
ConstText='ASCII'
    where SystemConstID = 33
end

if not exists(select 1 from DbSystemConst where SystemConstID = 34)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(34,'DeviceFrameType',1,'UTF-8')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceFrameType',
ConstValue=1,
ConstText='UTF-8'
    where SystemConstID = 34
end

if not exists(select 1 from DbSystemConst where SystemConstID = 35)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(35,'BillStatus',0,'未保存')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BillStatus',
ConstValue=0,
ConstText='未保存'
    where SystemConstID = 35
end

if not exists(select 1 from DbSystemConst where SystemConstID = 36)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(36,'BillStatus',1,'未审核')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BillStatus',
ConstValue=1,
ConstText='未审核'
    where SystemConstID = 36
end

if not exists(select 1 from DbSystemConst where SystemConstID = 37)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(37,'BillStatus',2,'已审核')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BillStatus',
ConstValue=2,
ConstText='已审核'
    where SystemConstID = 37
end

if not exists(select 1 from DbSystemConst where SystemConstID = 38)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(38,'WeightType',0,'入场磅')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='WeightType',
ConstValue=0,
ConstText='入场磅'
    where SystemConstID = 38
end

if not exists(select 1 from DbSystemConst where SystemConstID = 39)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(39,'WeightType',1,'出场磅')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='WeightType',
ConstValue=1,
ConstText='出场磅'
    where SystemConstID = 39
end

if not exists(select 1 from DbSystemConst where SystemConstID = 40)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(40,'BillStatus',3,'已作废')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BillStatus',
ConstValue=3,
ConstText='已作废'
    where SystemConstID = 40
end

if not exists(select 1 from DbSystemConst where SystemConstID = 41)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(41,'SysSaleBillType',0,'必须入场才能出场')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='SysSaleBillType',
ConstValue=0,
ConstText='必须入场才能出场'
    where SystemConstID = 41
end

if not exists(select 1 from DbSystemConst where SystemConstID = 42)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(42,'SysSaleBillType',1,'只生成出场磅单')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='SysSaleBillType',
ConstValue=1,
ConstText='只生成出场磅单'
    where SystemConstID = 42
end

if not exists(select 1 from DbSystemConst where SystemConstID = 43)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(43,'FieldType',0,'字符串')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='FieldType',
ConstValue=0,
ConstText='字符串'
    where SystemConstID = 43
end

if not exists(select 1 from DbSystemConst where SystemConstID = 44)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(44,'FieldType',1,'数字')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='FieldType',
ConstValue=1,
ConstText='数字'
    where SystemConstID = 44
end

if not exists(select 1 from DbSystemConst where SystemConstID = 45)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(45,'FieldType',2,'日期')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='FieldType',
ConstValue=2,
ConstText='日期'
    where SystemConstID = 45
end

if not exists(select 1 from DbSystemConst where SystemConstID = 46)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(46,'DeviceXType',0,'光耦输入X0')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceXType',
ConstValue=0,
ConstText='光耦输入X0'
    where SystemConstID = 46
end

if not exists(select 1 from DbSystemConst where SystemConstID = 47)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(47,'DeviceXType',1,'光耦输入X1')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceXType',
ConstValue=1,
ConstText='光耦输入X1'
    where SystemConstID = 47
end

if not exists(select 1 from DbSystemConst where SystemConstID = 48)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(48,'DeviceXType',2,'光耦输入X2')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceXType',
ConstValue=2,
ConstText='光耦输入X2'
    where SystemConstID = 48
end

if not exists(select 1 from DbSystemConst where SystemConstID = 49)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(49,'DeviceXType',3,'光耦输入X3')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceXType',
ConstValue=3,
ConstText='光耦输入X3'
    where SystemConstID = 49
end

if not exists(select 1 from DbSystemConst where SystemConstID = 50)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(50,'DeviceYType',0,'继电器输出Y0')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceYType',
ConstValue=0,
ConstText='继电器输出Y0'
    where SystemConstID = 50
end

if not exists(select 1 from DbSystemConst where SystemConstID = 51)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(51,'DeviceYType',1,'继电器输出Y1')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceYType',
ConstValue=1,
ConstText='继电器输出Y1'
    where SystemConstID = 51
end

if not exists(select 1 from DbSystemConst where SystemConstID = 52)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(52,'DeviceYType',2,'继电器输出Y2')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceYType',
ConstValue=2,
ConstText='继电器输出Y2'
    where SystemConstID = 52
end

if not exists(select 1 from DbSystemConst where SystemConstID = 53)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(53,'DeviceYType',3,'继电器输出Y3')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='DeviceYType',
ConstValue=3,
ConstText='继电器输出Y3'
    where SystemConstID = 53
end

if not exists(select 1 from DbSystemConst where SystemConstID = 66)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(66,'BillType',0,'未审核')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BillType',
ConstValue=0,
ConstText='未审核'
    where SystemConstID = 66
end

if not exists(select 1 from DbSystemConst where SystemConstID = 67)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(67,'BillType',1,'已审核')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BillType',
ConstValue=1,
ConstText='已审核'
    where SystemConstID = 67
end

if not exists(select 1 from DbSystemConst where SystemConstID = 68)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(68,'BillType',2,'已作废')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='BillType',
ConstValue=2,
ConstText='已作废'
    where SystemConstID = 68
end

if not exists(select 1 from DbSystemConst where SystemConstID = 69)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(69,'RPReceiveType',0,'现金充值')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='RPReceiveType',
ConstValue=0,
ConstText='现金充值'
    where SystemConstID = 69
end

if not exists(select 1 from DbSystemConst where SystemConstID = 70)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(70,'RPReceiveType',1,'银行充值')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='RPReceiveType',
ConstValue=1,
ConstText='银行充值'
    where SystemConstID = 70
end

if not exists(select 1 from DbSystemConst where SystemConstID = 71)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(71,'RPReceiveType',2,'承兑')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='RPReceiveType',
ConstValue=2,
ConstText='承兑'
    where SystemConstID = 71
end

if not exists(select 1 from DbSystemConst where SystemConstID = 72)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(72,'RPReceiveType',3,'其他')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='RPReceiveType',
ConstValue=3,
ConstText='其他'
    where SystemConstID = 72
end

if not exists(select 1 from DbSystemConst where SystemConstID = 73)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(73,'ReturnType',0,'完全退货')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnType',
ConstValue=0,
ConstText='完全退货'
    where SystemConstID = 73
end

if not exists(select 1 from DbSystemConst where SystemConstID = 74)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(74,'ReturnType',1,'部分退货')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnType',
ConstValue=1,
ConstText='部分退货'
    where SystemConstID = 74
end

if not exists(select 1 from DbSystemConst where SystemConstID = 75)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(75,'ReturnReason',0,'车坏了')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnReason',
ConstValue=0,
ConstText='车坏了'
    where SystemConstID = 75
end

if not exists(select 1 from DbSystemConst where SystemConstID = 76)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(76,'ReturnReason',1,'质量问题')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnReason',
ConstValue=1,
ConstText='质量问题'
    where SystemConstID = 76
end

if not exists(select 1 from DbSystemConst where SystemConstID = 77)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(77,'ReturnReason',2,'超载')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnReason',
ConstValue=2,
ConstText='超载'
    where SystemConstID = 77
end

if not exists(select 1 from DbSystemConst where SystemConstID = 78)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(78,'ReturnReason',3,'无料装')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnReason',
ConstValue=3,
ConstText='无料装'
    where SystemConstID = 78
end

if not exists(select 1 from DbSystemConst where SystemConstID = 79)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(79,'ReturnReason',4,'换装其他料')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnReason',
ConstValue=4,
ConstText='换装其他料'
    where SystemConstID = 79
end

if not exists(select 1 from DbSystemConst where SystemConstID = 80)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(80,'ReturnReason',5,'其他')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReturnReason',
ConstValue=5,
ConstText='其他'
    where SystemConstID = 80
end

if not exists(select 1 from DbSystemConst where SystemConstID = 81)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(81,'ReceiveType',3,'免费')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReceiveType',
ConstValue=3,
ConstText='免费'
    where SystemConstID = 81
end

if not exists(select 1 from DbSystemConst where SystemConstID = 82)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(82,'SaleBillType',0,'销售磅单')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='SaleBillType',
ConstValue=0,
ConstText='销售磅单'
    where SystemConstID = 82
end

if not exists(select 1 from DbSystemConst where SystemConstID = 83)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(83,'SaleBillType',1,'采购油磅单')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='SaleBillType',
ConstValue=1,
ConstText='采购油磅单'
    where SystemConstID = 83
end

if not exists(select 1 from DbSystemConst where SystemConstID = 84)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(84,'ReceiveType',4,'采购汽油')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='ReceiveType',
ConstValue=4,
ConstText='采购汽油'
    where SystemConstID = 84
end

if not exists(select 1 from DbSystemConst where SystemConstID = 86)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(86,'FieldType',3,'客户')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='FieldType',
ConstValue=3,
ConstText='客户'
    where SystemConstID = 86
end

if not exists(select 1 from DbSystemConst where SystemConstID = 87)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(87,'FieldType',4,'物料')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='FieldType',
ConstValue=4,
ConstText='物料'
    where SystemConstID = 87
end

if not exists(select 1 from DbSystemConst where SystemConstID = 88)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(88,'FieldType',5,'车牌')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='FieldType',
ConstValue=5,
ConstText='车牌'
    where SystemConstID = 88
end

if not exists(select 1 from DbSystemConst where SystemConstID = 89)
begin
    set IDENTITY_INSERT dbsystemconst on
insert dbsystemconst (SystemConstID,FieldName,ConstValue,ConstText ) values(89,'WeightDeviceType',2,'衡天')
set IDENTITY_INSERT dbsystemconst off

end


else
begin
    update DbSystemConst
    set FieldName='WeightDeviceType',
ConstValue=2,
ConstText='衡天'
    where SystemConstID = 89
end


if not exists(select 1 from DbBillType where BillTypeID = 1)
begin
    insert dbbilltype (BillTypeID,BillTypeName,BillCodingType ) values(1,'调价单',2)

end


else
begin
    update DbBillType
    set BillTypeName='调价单',
BillCodingType=2
    where BillTypeID = 1
end


if not exists(select 1 from DbPermission where PermissionID = 1)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(1,'用户权限管理',5,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='用户权限管理',
ParentPermissionID=5,
Forbid=null
    where PermissionID = 1
end

if not exists(select 1 from DbPermission where PermissionID = 5)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(5,'系统管理',null,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='系统管理',
ParentPermissionID=null,
Forbid=null
    where PermissionID = 5
end

if not exists(select 1 from DbPermission where PermissionID = 6)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(6,'修改密码',5,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='修改密码',
ParentPermissionID=5,
Forbid=null
    where PermissionID = 6
end

if not exists(select 1 from DbPermission where PermissionID = 7)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(7,'操作日志',5,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='操作日志',
ParentPermissionID=5,
Forbid=null
    where PermissionID = 7
end

if not exists(select 1 from DbPermission where PermissionID = 8)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(8,'地磅系统',null,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='地磅系统',
ParentPermissionID=null,
Forbid=null
    where PermissionID = 8
end

if not exists(select 1 from DbPermission where PermissionID = 9)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(9,'地磅-系统管理',8,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='地磅-系统管理',
ParentPermissionID=8,
Forbid=null
    where PermissionID = 9
end

if not exists(select 1 from DbPermission where PermissionID = 10)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(10,'修改密码',9,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='修改密码',
ParentPermissionID=9,
Forbid=null
    where PermissionID = 10
end

if not exists(select 1 from DbPermission where PermissionID = 11)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(11,'地磅仪表设置',9,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='地磅仪表设置',
ParentPermissionID=9,
Forbid=null
    where PermissionID = 11
end

if not exists(select 1 from DbPermission where PermissionID = 12)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(12,'摄像头设置',9,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='摄像头设置',
ParentPermissionID=9,
Forbid=null
    where PermissionID = 12
end

if not exists(select 1 from DbPermission where PermissionID = 13)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(13,'磅房设置',9,1)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='磅房设置',
ParentPermissionID=9,
Forbid=1
    where PermissionID = 13
end

if not exists(select 1 from DbPermission where PermissionID = 15)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(15,'地磅-称重',8,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='地磅-称重',
ParentPermissionID=8,
Forbid=null
    where PermissionID = 15
end

if not exists(select 1 from DbPermission where PermissionID = 20)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(20,'帐套备份设置',5,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='帐套备份设置',
ParentPermissionID=5,
Forbid=null
    where PermissionID = 20
end

if not exists(select 1 from DbPermission where PermissionID = 21)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(21,'系统设置',5,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='系统设置',
ParentPermissionID=5,
Forbid=null
    where PermissionID = 21
end

if not exists(select 1 from DbPermission where PermissionID = 22)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(22,'设备管理',null,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='设备管理',
ParentPermissionID=null,
Forbid=null
    where PermissionID = 22
end

if not exists(select 1 from DbPermission where PermissionID = 23)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(23,'地磅仪表管理',22,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='地磅仪表管理',
ParentPermissionID=22,
Forbid=null
    where PermissionID = 23
end

if not exists(select 1 from DbPermission where PermissionID = 24)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(24,'摄像头管理',22,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='摄像头管理',
ParentPermissionID=22,
Forbid=null
    where PermissionID = 24
end

if not exists(select 1 from DbPermission where PermissionID = 25)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(25,'基础资料管理',null,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='基础资料管理',
ParentPermissionID=null,
Forbid=null
    where PermissionID = 25
end

if not exists(select 1 from DbPermission where PermissionID = 26)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(26,'物料管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='物料管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 26
end

if not exists(select 1 from DbPermission where PermissionID = 27)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(27,'计量单位管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='计量单位管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 27
end

if not exists(select 1 from DbPermission where PermissionID = 28)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(28,'备注管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='备注管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 28
end

if not exists(select 1 from DbPermission where PermissionID = 29)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(29,'客户资料管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='客户资料管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 29
end

if not exists(select 1 from DbPermission where PermissionID = 30)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(30,'车辆管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='车辆管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 30
end

if not exists(select 1 from DbPermission where PermissionID = 31)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(31,'调价单管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='调价单管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 31
end

if not exists(select 1 from DbPermission where PermissionID = 32)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(32,'销售管理',null,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='销售管理',
ParentPermissionID=null,
Forbid=null
    where PermissionID = 32
end

if not exists(select 1 from DbPermission where PermissionID = 33)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(33,'销售磅单管理',32,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='销售磅单管理',
ParentPermissionID=32,
Forbid=null
    where PermissionID = 33
end

if not exists(select 1 from DbPermission where PermissionID = 34)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(34,'收款管理',null,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='收款管理',
ParentPermissionID=null,
Forbid=null
    where PermissionID = 34
end

if not exists(select 1 from DbPermission where PermissionID = 35)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(35,'充值记录',34,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='充值记录',
ParentPermissionID=34,
Forbid=null
    where PermissionID = 35
end

if not exists(select 1 from DbPermission where PermissionID = 36)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(36,'收款银行管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='收款银行管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 36
end

if not exists(select 1 from DbPermission where PermissionID = 37)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(37,'充值方式管理',25,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='充值方式管理',
ParentPermissionID=25,
Forbid=null
    where PermissionID = 37
end

if not exists(select 1 from DbPermission where PermissionID = 38)
begin
    set IDENTITY_INSERT dbpermission on
insert dbpermission (PermissionID,PermissionName,ParentPermissionID,Forbid ) values(38,'数据同步',null,null)
set IDENTITY_INSERT dbpermission off

end


else
begin
    update DbPermission
    set PermissionName='数据同步',
ParentPermissionID=null,
Forbid=null
    where PermissionID = 38
end


if not exists(select 1 from DbPermissionData where PermissionDataID = 1)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(1,1,'PMUserManager_Add','添加新用户',0,10000,'UserID,LoginName',null,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=1,
PermissionCode='PMUserManager_Add',
PermissionDataName='添加新用户',
PermissionType=0,
PermissionSPType=10000,
LogFieldName='UserID,LoginName',
PermissionViewType=null,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 1
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 2)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(2,1,'PMUserManager_Edit','修改用户',0,10001,'UserID,LoginName',null,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=1,
PermissionCode='PMUserManager_Edit',
PermissionDataName='修改用户',
PermissionType=0,
PermissionSPType=10001,
LogFieldName='UserID,LoginName',
PermissionViewType=null,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 2
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 4)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(4,1,'PMUserManager_Del','删除用户',0,10002,'UserID',null,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=1,
PermissionCode='PMUserManager_Del',
PermissionDataName='删除用户',
PermissionType=0,
PermissionSPType=10002,
LogFieldName='UserID',
PermissionViewType=null,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 4
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 8)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(8,1,'PMUserManager_Query','查询用户信息',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=1,
PermissionCode='PMUserManager_Query',
PermissionDataName='查询用户信息',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 8
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 9)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(9,7,'LogManager_Query','操作日志_查询',1,0,'',null,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=7,
PermissionCode='LogManager_Query',
PermissionDataName='操作日志_查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=null,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 9
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 10)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(10,7,'LogManager_Delete','操作日志_删除',0,13001,'SysLogID,LoginName,LogTime,LogModule',null,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=7,
PermissionCode='LogManager_Delete',
PermissionDataName='操作日志_删除',
PermissionType=0,
PermissionSPType=13001,
LogFieldName='SysLogID,LoginName,LogTime,LogModule',
PermissionViewType=null,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 10
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 11)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(11,1,'PMUserManager_Permission','设置权限',0,10003,'UserID',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=1,
PermissionCode='PMUserManager_Permission',
PermissionDataName='设置权限',
PermissionType=0,
PermissionSPType=10003,
LogFieldName='UserID',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 11
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 12)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(12,6,'PMChangePassword','确认修改',0,10003,'UserID',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=6,
PermissionCode='PMChangePassword',
PermissionDataName='确认修改',
PermissionType=0,
PermissionSPType=10003,
LogFieldName='UserID',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 12
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 13)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(13,10,'WeightChangePassword','修改密码',0,10003,'UserID',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=10,
PermissionCode='WeightChangePassword',
PermissionDataName='修改密码',
PermissionType=0,
PermissionSPType=10003,
LogFieldName='UserID',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 13
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 14)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(14,11,'WeightDeviceConfigView','查看',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=11,
PermissionCode='WeightDeviceConfigView',
PermissionDataName='查看',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 14
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 15)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(15,11,'WeightDeviceConfigSave','保存',0,13800,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=11,
PermissionCode='WeightDeviceConfigSave',
PermissionDataName='保存',
PermissionType=0,
PermissionSPType=13800,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 15
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 16)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(16,12,'WeightCameraConfigView','查看设置',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=12,
PermissionCode='WeightCameraConfigView',
PermissionDataName='查看设置',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 16
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 17)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(17,12,'WeightCameraConfigSave','保存',0,13900,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=12,
PermissionCode='WeightCameraConfigSave',
PermissionDataName='保存',
PermissionType=0,
PermissionSPType=13900,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 17
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 18)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(18,13,'WeightFactoryView','磅房设置',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=13,
PermissionCode='WeightFactoryView',
PermissionDataName='磅房设置',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 18
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 19)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(19,13,'WeightFactorySave','保存',0,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=13,
PermissionCode='WeightFactorySave',
PermissionDataName='保存',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 19
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 22)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(22,15,'WeightSalesInBill_Save','保存-入场',0,14100,'SaleCarInBillID,SaleCarInBillCode',0,0,1)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesInBill_Save',
PermissionDataName='保存-入场',
PermissionType=0,
PermissionSPType=14100,
LogFieldName='SaleCarInBillID,SaleCarInBillCode',
PermissionViewType=0,
DetailIndex=0,
Forbid=1
    where PermissionDataID = 22
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 23)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(23,15,'WeightSalesOutBill_Save','保存并打印',0,14102,'SaleCarOutBillID,SaleCarInBillCode',0,3,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_Save',
PermissionDataName='保存并打印',
PermissionType=0,
PermissionSPType=14102,
LogFieldName='SaleCarOutBillID,SaleCarInBillCode',
PermissionViewType=0,
DetailIndex=3,
Forbid=null
    where PermissionDataID = 23
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 24)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(24,15,'WeightSalesOutBill_TareValue','空车',0,0,'',0,1,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_TareValue',
PermissionDataName='空车',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=1,
Forbid=null
    where PermissionDataID = 24
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 25)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(25,15,'WeightSalesOutBill_TotalValue','重车',0,0,'',0,2,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_TotalValue',
PermissionDataName='重车',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=2,
Forbid=null
    where PermissionDataID = 25
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 26)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(26,15,'WeightSalesOutBill_Price','单价',0,0,'',0,0,1)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_Price',
PermissionDataName='单价',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=0,
Forbid=1
    where PermissionDataID = 26
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 27)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(27,15,'WeightSalesOutBill_PrintOutBill','补印磅单',0,0,'',0,11,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_PrintOutBill',
PermissionDataName='补印磅单',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=11,
Forbid=0
    where PermissionDataID = 27
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 28)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(28,15,'WeightSalesOutBill_PrintInBill','补打小票',0,0,'',0,10,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_PrintInBill',
PermissionDataName='补打小票',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=10,
Forbid=0
    where PermissionDataID = 28
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 29)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(29,15,'WeightSalesOutBill_ReadTare','取皮重',1,0,'',0,5,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_ReadTare',
PermissionDataName='取皮重',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=5,
Forbid=null
    where PermissionDataID = 29
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 30)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(30,20,'DBBackUp_Query','帐套备份设置',1,0,'',108,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=20,
PermissionCode='DBBackUp_Query',
PermissionDataName='帐套备份设置',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=108,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 30
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 31)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(31,20,'DBBackUp_Add','添加备份方案',0,13200,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=20,
PermissionCode='DBBackUp_Add',
PermissionDataName='添加备份方案',
PermissionType=0,
PermissionSPType=13200,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 31
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 32)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(32,20,'DBBackUp_Save','保存或修改备份方案',0,13201,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=20,
PermissionCode='DBBackUp_Save',
PermissionDataName='保存或修改备份方案',
PermissionType=0,
PermissionSPType=13201,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 32
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 33)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(33,20,'DBBackUp_Delete','删除备份方案',0,13202,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=20,
PermissionCode='DBBackUp_Delete',
PermissionDataName='删除备份方案',
PermissionType=0,
PermissionSPType=13202,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 33
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 34)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(34,21,'DbSysConfig_Query','系统设置',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=21,
PermissionCode='DbSysConfig_Query',
PermissionDataName='系统设置',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 34
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 35)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(35,21,'DbSysConfig _Save','保存系统设置',0,14300,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=21,
PermissionCode='DbSysConfig _Save',
PermissionDataName='保存系统设置',
PermissionType=0,
PermissionSPType=14300,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 35
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 36)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(36,23,'WeightDevice_Query','地磅仪表',1,0,'',119,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=23,
PermissionCode='WeightDevice_Query',
PermissionDataName='地磅仪表',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=119,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 36
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 37)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(37,23,'WeightDevice_Save','地磅仪表_保存',0,13800,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=23,
PermissionCode='WeightDevice_Save',
PermissionDataName='地磅仪表_保存',
PermissionType=0,
PermissionSPType=13800,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 37
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 38)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(38,24,'CameraDevice_Query','摄像头_查询',1,0,'',122,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=24,
PermissionCode='CameraDevice_Query',
PermissionDataName='摄像头_查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=122,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 38
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 39)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(39,24,'CameraDevice_ Save','摄像头_保存',0,13900,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=24,
PermissionCode='CameraDevice_ Save',
PermissionDataName='摄像头_保存',
PermissionType=0,
PermissionSPType=13900,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 39
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 40)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(40,26,'ItemManager_Query','物料管理_查询',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=26,
PermissionCode='ItemManager_Query',
PermissionDataName='物料管理_查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 40
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 41)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(41,26,'ItemType_Add','添加分类',0,20100,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=26,
PermissionCode='ItemType_Add',
PermissionDataName='添加分类',
PermissionType=0,
PermissionSPType=20100,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 41
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 42)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(42,26,'ItemType_Update','修改分类',0,20101,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=26,
PermissionCode='ItemType_Update',
PermissionDataName='修改分类',
PermissionType=0,
PermissionSPType=20101,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 42
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 43)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(43,26,'ItemType_Delete','删除分类',0,20102,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=26,
PermissionCode='ItemType_Delete',
PermissionDataName='删除分类',
PermissionType=0,
PermissionSPType=20102,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 43
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 44)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(44,26,'ItemBase_Update','编辑货物',0,20301,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=26,
PermissionCode='ItemBase_Update',
PermissionDataName='编辑货物',
PermissionType=0,
PermissionSPType=20301,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 44
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 45)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(45,26,'ItemBase_Add','添加货物',0,20300,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=26,
PermissionCode='ItemBase_Add',
PermissionDataName='添加货物',
PermissionType=0,
PermissionSPType=20300,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 45
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 46)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(46,26,'ItemBase_Delete','删除货物',0,20302,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=26,
PermissionCode='ItemBase_Delete',
PermissionDataName='删除货物',
PermissionType=0,
PermissionSPType=20302,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 46
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 47)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(47,27,'DBUOM_Query','计量单位管理_查询',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=27,
PermissionCode='DBUOM_Query',
PermissionDataName='计量单位管理_查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 47
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 48)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(48,27,'DBUOM_Add','添加计量单位',0,20200,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=27,
PermissionCode='DBUOM_Add',
PermissionDataName='添加计量单位',
PermissionType=0,
PermissionSPType=20200,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 48
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 49)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(49,27,'DBUOM _Update','修改计量单位',0,20201,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=27,
PermissionCode='DBUOM _Update',
PermissionDataName='修改计量单位',
PermissionType=0,
PermissionSPType=20201,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 49
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 50)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(50,27,'DBUOM _Delete','删除计量单位',0,20202,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=27,
PermissionCode='DBUOM _Delete',
PermissionDataName='删除计量单位',
PermissionType=0,
PermissionSPType=20202,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 50
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 51)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(51,28,'DBDescription_Query','备注管理_查询',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=28,
PermissionCode='DBDescription_Query',
PermissionDataName='备注管理_查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 51
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 52)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(52,28,'DBDescription_Add','添加备注',0,14000,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=28,
PermissionCode='DBDescription_Add',
PermissionDataName='添加备注',
PermissionType=0,
PermissionSPType=14000,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 52
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 53)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(53,28,'DBDescription_Update','修改备注',0,14000,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=28,
PermissionCode='DBDescription_Update',
PermissionDataName='修改备注',
PermissionType=0,
PermissionSPType=14000,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 53
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 54)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(54,28,'DBDescription_Delete','删除备注',0,14001,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=28,
PermissionCode='DBDescription_Delete',
PermissionDataName='删除备注',
PermissionType=0,
PermissionSPType=14001,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 54
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 55)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(55,29,'DBCustomer_Query','客户资料查询',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=29,
PermissionCode='DBCustomer_Query',
PermissionDataName='客户资料查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 55
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 56)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(56,29,'DBCustomer_Add','添加客户',0,13400,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=29,
PermissionCode='DBCustomer_Add',
PermissionDataName='添加客户',
PermissionType=0,
PermissionSPType=13400,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 56
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 57)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(57,29,'DBCustomer_Update','修改客户',0,13401,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=29,
PermissionCode='DBCustomer_Update',
PermissionDataName='修改客户',
PermissionType=0,
PermissionSPType=13401,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 57
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 58)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(58,29,'DBCustomer_Delete','删除客户',0,13402,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=29,
PermissionCode='DBCustomer_Delete',
PermissionDataName='删除客户',
PermissionType=0,
PermissionSPType=13402,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 58
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 59)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(59,29,'DBCustomer_Copy','复制客户',0,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=29,
PermissionCode='DBCustomer_Copy',
PermissionDataName='复制客户',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 59
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 60)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(60,30,'DBCar_Query','车辆管理查询',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=30,
PermissionCode='DBCar_Query',
PermissionDataName='车辆管理查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 60
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 61)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(61,30,'DBCar_Add','添加车辆',0,13500,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=30,
PermissionCode='DBCar_Add',
PermissionDataName='添加车辆',
PermissionType=0,
PermissionSPType=13500,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 61
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 62)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(62,30,'DBCar_Update','修改车辆',0,13501,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=30,
PermissionCode='DBCar_Update',
PermissionDataName='修改车辆',
PermissionType=0,
PermissionSPType=13501,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 62
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 63)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(63,30,'DBCar_Delete','删除车辆',0,13502,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=30,
PermissionCode='DBCar_Delete',
PermissionDataName='删除车辆',
PermissionType=0,
PermissionSPType=13502,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 63
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 64)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(64,30,'DBCarWeight_Add','新增皮重',0,20400,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=30,
PermissionCode='DBCarWeight_Add',
PermissionDataName='新增皮重',
PermissionType=0,
PermissionSPType=20400,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 64
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 65)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(65,31,'PriceManager_Add','添加调价单',0,13600,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_Add',
PermissionDataName='添加调价单',
PermissionType=0,
PermissionSPType=13600,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 65
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 66)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(66,31,'PriceManager_Update','修改调价单',0,13601,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_Update',
PermissionDataName='修改调价单',
PermissionType=0,
PermissionSPType=13601,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 66
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 67)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(67,31,'PriceManager_Delete','删除调价单',0,13602,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_Delete',
PermissionDataName='删除调价单',
PermissionType=0,
PermissionSPType=13602,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 67
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 68)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(68,31,'PriceManager_Copy','复制调价单',0,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_Copy',
PermissionDataName='复制调价单',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 68
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 69)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(69,31,'PriceManager_Approve','审核调价单',0,13603,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_Approve',
PermissionDataName='审核调价单',
PermissionType=0,
PermissionSPType=13603,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 69
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 70)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(70,31,'PriceManager_UnApprove','反审核调价单',0,13604,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_UnApprove',
PermissionDataName='反审核调价单',
PermissionType=0,
PermissionSPType=13604,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 70
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 71)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(71,31,'PriceManager_Query','调价单管理查询',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_Query',
PermissionDataName='调价单管理查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 71
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 72)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(72,31,'PriceManager_Cancel','作废调价单',0,13605,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_Cancel',
PermissionDataName='作废调价单',
PermissionType=0,
PermissionSPType=13605,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 72
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 73)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(73,31,'PriceManager_UnCancel','反作废调价单',0,13606,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=31,
PermissionCode='PriceManager_UnCancel',
PermissionDataName='反作废调价单',
PermissionType=0,
PermissionSPType=13606,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 73
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 74)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(74,33,'SalesManager_Query','销售磅单管理查询',1,0,'',0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_Query',
PermissionDataName='销售磅单管理查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 74
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 75)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(75,33,'SalesManager_Approve','销售磅单审核',0,14104,'',0,1,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_Approve',
PermissionDataName='销售磅单审核',
PermissionType=0,
PermissionSPType=14104,
LogFieldName='',
PermissionViewType=0,
DetailIndex=1,
Forbid=0
    where PermissionDataID = 75
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 76)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(76,33,'SalesManager_UnApprove','销售磅单取消审核',0,14105,'',0,2,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_UnApprove',
PermissionDataName='销售磅单取消审核',
PermissionType=0,
PermissionSPType=14105,
LogFieldName='',
PermissionViewType=0,
DetailIndex=2,
Forbid=0
    where PermissionDataID = 76
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 77)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(77,33,'SalesManager_Cancel','销售磅单作废',0,14106,'',0,4,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_Cancel',
PermissionDataName='销售磅单作废',
PermissionType=0,
PermissionSPType=14106,
LogFieldName='',
PermissionViewType=0,
DetailIndex=4,
Forbid=0
    where PermissionDataID = 77
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 78)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(78,33,'SalesManager_UnCancel','销售磅单反作废',0,14107,'',0,5,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_UnCancel',
PermissionDataName='销售磅单反作废',
PermissionType=0,
PermissionSPType=14107,
LogFieldName='',
PermissionViewType=0,
DetailIndex=5,
Forbid=0
    where PermissionDataID = 78
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 79)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(79,35,'RPReceiveList_Query','充值单管理查询',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceiveList_Query',
PermissionDataName='充值单管理查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 79
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 80)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(80,35,'RPReceive_Add','添加',0,13300,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceive_Add',
PermissionDataName='添加',
PermissionType=0,
PermissionSPType=13300,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 80
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 81)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(81,35,'RPReceive_Update','修改',0,13301,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceive_Update',
PermissionDataName='修改',
PermissionType=0,
PermissionSPType=13301,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 81
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 82)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(82,35,'RPReceive_Delete','删除',0,13302,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceive_Delete',
PermissionDataName='删除',
PermissionType=0,
PermissionSPType=13302,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 82
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 83)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(83,35,'RPReceive_Approve','审核',0,13303,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceive_Approve',
PermissionDataName='审核',
PermissionType=0,
PermissionSPType=13303,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 83
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 84)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(84,35,'RPReceive_UnApprove','反审核',0,13304,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceive_UnApprove',
PermissionDataName='反审核',
PermissionType=0,
PermissionSPType=13304,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 84
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 85)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(85,35,'RPReceive_Cancel','作废',0,13305,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceive_Cancel',
PermissionDataName='作废',
PermissionType=0,
PermissionSPType=13305,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 85
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 86)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(86,35,'RPReceive_UnCancel','反作废',0,13306,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=35,
PermissionCode='RPReceive_UnCancel',
PermissionDataName='反作废',
PermissionType=0,
PermissionSPType=13306,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 86
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 87)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(87,30,'DBCarWeight_Manager','车辆皮重库管理',1,0,'',0,null,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=30,
PermissionCode='DBCarWeight_Manager',
PermissionDataName='车辆皮重库管理',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=null,
Forbid=null
    where PermissionDataID = 87
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 88)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(88,15,'WeightSalesOutBill_ReviewInBill','预览小票',0,0,'',0,12,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_ReviewInBill',
PermissionDataName='预览小票',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=12,
Forbid=0
    where PermissionDataID = 88
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 89)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(89,15,'WeightSalesOutBill_ReviewOutBill','预览磅单',0,0,'',0,13,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_ReviewOutBill',
PermissionDataName='预览磅单',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=13,
Forbid=0
    where PermissionDataID = 89
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 90)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(90,15,'WeightSalesInBill_Cancel','作废-入场记录',0,14106,'',0,15,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesInBill_Cancel',
PermissionDataName='作废-入场记录',
PermissionType=0,
PermissionSPType=14106,
LogFieldName='',
PermissionViewType=0,
DetailIndex=15,
Forbid=0
    where PermissionDataID = 90
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 91)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(91,15,'WeightSalesInBill_UnCancel','取消作废-入场记录',0,14107,'',0,16,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesInBill_UnCancel',
PermissionDataName='取消作废-入场记录',
PermissionType=0,
PermissionSPType=14107,
LogFieldName='',
PermissionViewType=0,
DetailIndex=16,
Forbid=0
    where PermissionDataID = 91
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 92)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(92,15,'WeightSalesOutBill_SaveTare','存皮重',0,20400,'',0,4,null)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesOutBill_SaveTare',
PermissionDataName='存皮重',
PermissionType=0,
PermissionSPType=20400,
LogFieldName='',
PermissionViewType=0,
DetailIndex=4,
Forbid=null
    where PermissionDataID = 92
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 93)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(93,15,'WeightSalesInBill_Change','变更单据',0,14115,'',0,14,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesInBill_Change',
PermissionDataName='变更单据',
PermissionType=0,
PermissionSPType=14115,
LogFieldName='',
PermissionViewType=0,
DetailIndex=14,
Forbid=0
    where PermissionDataID = 93
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 94)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(94,33,'SalesManager_Change','销售磅单变更',0,14115,'',0,6,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_Change',
PermissionDataName='销售磅单变更',
PermissionType=0,
PermissionSPType=14115,
LogFieldName='',
PermissionViewType=0,
DetailIndex=6,
Forbid=0
    where PermissionDataID = 94
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 95)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(95,33,'SalesManager_ChangeDirect','直接修改原磅单数据并保存',0,14116,'',0,7,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_ChangeDirect',
PermissionDataName='直接修改原磅单数据并保存',
PermissionType=0,
PermissionSPType=14116,
LogFieldName='',
PermissionViewType=0,
DetailIndex=7,
Forbid=0
    where PermissionDataID = 95
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 96)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(96,33,'SalesManager_PrintInBill','补打入场小票',0,0,'',0,8,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_PrintInBill',
PermissionDataName='补打入场小票',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=8,
Forbid=0
    where PermissionDataID = 96
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 97)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(97,33,'SalesManager_PrintOutBill','补打出场磅单',0,0,'',0,9,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_PrintOutBill',
PermissionDataName='补打出场磅单',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=9,
Forbid=0
    where PermissionDataID = 97
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 98)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(98,33,'SalesManager_AddInBill','手工添加入场记录',0,14100,'',0,10,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_AddInBill',
PermissionDataName='手工添加入场记录',
PermissionType=0,
PermissionSPType=14100,
LogFieldName='',
PermissionViewType=0,
DetailIndex=10,
Forbid=0
    where PermissionDataID = 98
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 99)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(99,33,'SalesManager_AddOutBill','手工添加出场记录',0,14102,'',0,11,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=33,
PermissionCode='SalesManager_AddOutBill',
PermissionDataName='手工添加出场记录',
PermissionType=0,
PermissionSPType=14102,
LogFieldName='',
PermissionViewType=0,
DetailIndex=11,
Forbid=0
    where PermissionDataID = 99
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 100)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(100,36,'ReceiveBank_Query','收款银行管理查询',1,0,'',13600,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=36,
PermissionCode='ReceiveBank_Query',
PermissionDataName='收款银行管理查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=13600,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 100
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 101)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(101,36,'ReceiveBank_Add','添加收款银行',0,14600,'BankName',0,1,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=36,
PermissionCode='ReceiveBank_Add',
PermissionDataName='添加收款银行',
PermissionType=0,
PermissionSPType=14600,
LogFieldName='BankName',
PermissionViewType=0,
DetailIndex=1,
Forbid=0
    where PermissionDataID = 101
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 102)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(102,36,'ReceiveBank_Edit','编辑收款银行',0,14601,'BankName',0,2,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=36,
PermissionCode='ReceiveBank_Edit',
PermissionDataName='编辑收款银行',
PermissionType=0,
PermissionSPType=14601,
LogFieldName='BankName',
PermissionViewType=0,
DetailIndex=2,
Forbid=0
    where PermissionDataID = 102
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 103)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(103,36,'ReceiveBank_Delete','删除收款银行',0,14602,'BankName',0,3,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=36,
PermissionCode='ReceiveBank_Delete',
PermissionDataName='删除收款银行',
PermissionType=0,
PermissionSPType=14602,
LogFieldName='BankName',
PermissionViewType=0,
DetailIndex=3,
Forbid=0
    where PermissionDataID = 103
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 105)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(105,15,'WeightSalesReturnIn_Save','退货处理_入场称重',0,30000,'',0,18,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesReturnIn_Save',
PermissionDataName='退货处理_入场称重',
PermissionType=0,
PermissionSPType=30000,
LogFieldName='',
PermissionViewType=0,
DetailIndex=18,
Forbid=0
    where PermissionDataID = 105
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 106)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(106,15,'WeightSalesReturnOut_Save','退货处理_出场称重并退货',0,30001,'',0,19,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesReturnOut_Save',
PermissionDataName='退货处理_出场称重并退货',
PermissionType=0,
PermissionSPType=30001,
LogFieldName='',
PermissionViewType=0,
DetailIndex=19,
Forbid=0
    where PermissionDataID = 106
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 107)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(107,15,'WeightSalesReturn_View','退货处理',1,0,'',0,17,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightSalesReturn_View',
PermissionDataName='退货处理',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=17,
Forbid=0
    where PermissionDataID = 107
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 108)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(108,37,'ChargeType_Query','充值方式管理查询',1,0,'',138,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=37,
PermissionCode='ChargeType_Query',
PermissionDataName='充值方式管理查询',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=138,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 108
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 109)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(109,37,'ChargeType_Insert','添加充值方式',0,14700,'ChargeTypeName',0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=37,
PermissionCode='ChargeType_Insert',
PermissionDataName='添加充值方式',
PermissionType=0,
PermissionSPType=14700,
LogFieldName='ChargeTypeName',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 109
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 110)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(110,37,'ChargeType_Update','修改充值方式',0,14701,'ChargeTypeName',0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=37,
PermissionCode='ChargeType_Update',
PermissionDataName='修改充值方式',
PermissionType=0,
PermissionSPType=14701,
LogFieldName='ChargeTypeName',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 110
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 111)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(111,37,'ChargeType_Delete','删除充值方式',0,14702,'ChargeTypeName',0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=37,
PermissionCode='ChargeType_Delete',
PermissionDataName='删除充值方式',
PermissionType=0,
PermissionSPType=14702,
LogFieldName='ChargeTypeName',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 111
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 112)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(112,15,'WeightPurchase_View','汽油采购',1,0,'',0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=15,
PermissionCode='WeightPurchase_View',
PermissionDataName='汽油采购',
PermissionType=1,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 112
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 114)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(114,38,'Customer_Synchornous','客户资料同步',0,0,null,0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=38,
PermissionCode='Customer_Synchornous',
PermissionDataName='客户资料同步',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 114
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 115)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(115,38,'Car_Synchornous','车辆资料同步',0,0,null,0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=38,
PermissionCode='Car_Synchornous',
PermissionDataName='车辆资料同步',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 115
end

if not exists(select 1 from DbPermissionData where PermissionDataID = 116)
begin
    set IDENTITY_INSERT dbpermissiondata on
insert dbpermissiondata (PermissionDataID,PermissionID,PermissionCode,PermissionDataName,PermissionType,PermissionSPType,LogFieldName,PermissionViewType,DetailIndex,Forbid ) values(116,38,'SalesBill_Synchornous','单据资料同步',0,0,null,0,0,0)
set IDENTITY_INSERT dbpermissiondata off

end


else
begin
    update DbPermissionData
    set PermissionID=38,
PermissionCode='SalesBill_Synchornous',
PermissionDataName='单据资料同步',
PermissionType=0,
PermissionSPType=0,
LogFieldName='',
PermissionViewType=0,
DetailIndex=0,
Forbid=0
    where PermissionDataID = 116
end


if not exists(select 1 from DbReportType where ReportTypeID = 1)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(1,'用户管理')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='用户管理'
    where ReportTypeID = 1
end

if not exists(select 1 from DbReportType where ReportTypeID = 2)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(2,'充值管理')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='充值管理'
    where ReportTypeID = 2
end

if not exists(select 1 from DbReportType where ReportTypeID = 3)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(3,'客户管理')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='客户管理'
    where ReportTypeID = 3
end

if not exists(select 1 from DbReportType where ReportTypeID = 4)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(4,'调价单序时簿')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='调价单序时簿'
    where ReportTypeID = 4
end

if not exists(select 1 from DbReportType where ReportTypeID = 5)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(5,'调价单')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='调价单'
    where ReportTypeID = 5
end

if not exists(select 1 from DbReportType where ReportTypeID = 6)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(6,'入场磅单')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='入场磅单'
    where ReportTypeID = 6
end

if not exists(select 1 from DbReportType where ReportTypeID = 7)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(7,'出场磅单')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='出场磅单'
    where ReportTypeID = 7
end

if not exists(select 1 from DbReportType where ReportTypeID = 8)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(8,'决策分析报表')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='决策分析报表'
    where ReportTypeID = 8
end

if not exists(select 1 from DbReportType where ReportTypeID = 9)
begin
    set IDENTITY_INSERT dbreporttype on
insert dbreporttype (ReportTypeID,ReportTypeName ) values(9,'磅单查询清单')
set IDENTITY_INSERT dbreporttype off

end


else
begin
    update DbReportType
    set ReportTypeName='磅单查询清单'
    where ReportTypeID = 9
end


if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 1)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(1,'SysSaleReceiveOverdue','入场磅单有效时间(分钟)',1,'30')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='SysSaleReceiveOverdue',
SysConfigFieldText='入场磅单有效时间(分钟)',
SysConfigDataType=1,
SysConfigDefaultValue='30'
    where SysConfigFieldID = 1
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 2)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(2,'SysSaleBillType','磅单模式（0必须有入场磅单 1无需入场磅单）',2,'0')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='SysSaleBillType',
SysConfigFieldText='磅单模式（0必须有入场磅单 1无需入场磅单）',
SysConfigDataType=2,
SysConfigDefaultValue='0'
    where SysConfigFieldID = 2
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 5)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(5,'AllowPrintInReportCount','允许补打小票次数',1,'1')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='AllowPrintInReportCount',
SysConfigFieldText='允许补打小票次数',
SysConfigDataType=1,
SysConfigDefaultValue='1'
    where SysConfigFieldID = 5
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 6)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(6,'AllowPrintOutReportCount','允许补打磅单次数',1,'1')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='AllowPrintOutReportCount',
SysConfigFieldText='允许补打磅单次数',
SysConfigDataType=1,
SysConfigDefaultValue='1'
    where SysConfigFieldID = 6
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 8)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(8,'AmountNotEnough','预付客户充值余额预警值',1,'10000')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='AmountNotEnough',
SysConfigFieldText='预付客户充值余额预警值',
SysConfigDataType=1,
SysConfigDefaultValue='10000'
    where SysConfigFieldID = 8
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 9)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(9,'SaleInBillCode','入场单编码前缀',0,'JC')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='SaleInBillCode',
SysConfigFieldText='入场单编码前缀',
SysConfigDataType=0,
SysConfigDefaultValue='JC'
    where SysConfigFieldID = 9
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 11)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(11,'SaleOutBillCode','出场单编码前缀',0,'XS')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='SaleOutBillCode',
SysConfigFieldText='出场单编码前缀',
SysConfigDataType=0,
SysConfigDefaultValue='XS'
    where SysConfigFieldID = 11
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 12)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(12,'SaleReturnBillCode','退货单编码前缀',0,'TH')
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='SaleReturnBillCode',
SysConfigFieldText='退货单编码前缀',
SysConfigDataType=0,
SysConfigDefaultValue='TH'
    where SysConfigFieldID = 12
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 13)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(13,'CustomerSynchronousTime','客户资料同步时间',0,null)
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='CustomerSynchronousTime',
SysConfigFieldText='客户资料同步时间',
SysConfigDataType=0,
SysConfigDefaultValue=''
    where SysConfigFieldID = 13
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 14)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(14,'CarSynchronousTime','车辆资料同步时间',0,null)
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='CarSynchronousTime',
SysConfigFieldText='车辆资料同步时间',
SysConfigDataType=0,
SysConfigDefaultValue=''
    where SysConfigFieldID = 14
end

if not exists(select 1 from DbSysConfigField where SysConfigFieldID = 17)
begin
    set IDENTITY_INSERT dbsysconfigfield on
insert dbsysconfigfield (SysConfigFieldID,SysConfigFieldName,SysConfigFieldText,SysConfigDataType,SysConfigDefaultValue ) values(17,'ItemSynchronousTime','物料资料同步时间',0,null)
set IDENTITY_INSERT dbsysconfigfield off

end


else
begin
    update DbSysConfigField
    set SysConfigFieldName='ItemSynchronousTime',
SysConfigFieldText='物料资料同步时间',
SysConfigDataType=0,
SysConfigDefaultValue=''
    where SysConfigFieldID = 17
end


if not exists(select 1 from SysSPType where SysSPTypeID = 1)
begin
    set IDENTITY_INSERT syssptype on
insert syssptype (SysSPTypeID,SysSPType,SysSPName ) values(1,10003,'DBUser_ChangePassword')
set IDENTITY_INSERT syssptype off

end


else
begin
    update SysSPType
    set SysSPType=10003,
SysSPName='DBUser_ChangePassword'
    where SysSPTypeID = 1
end

if not exists(select 1 from SysSPType where SysSPTypeID = 2)
begin
    set IDENTITY_INSERT syssptype on
insert syssptype (SysSPTypeID,SysSPType,SysSPName ) values(2,10000,'DBUser_Insert')
set IDENTITY_INSERT syssptype off

end


else
begin
    update SysSPType
    set SysSPType=10000,
SysSPName='DBUser_Insert'
    where SysSPTypeID = 2
end


if not exists(select 1 from SysViewType where SysViewTypeID = 1)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(1,'100','Db_v_User')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='100',
SysViewName='Db_v_User'
    where SysViewTypeID = 1
end

if not exists(select 1 from SysViewType where SysViewTypeID = 2)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(2,'101','DbSystemConst')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='101',
SysViewName='DbSystemConst'
    where SysViewTypeID = 2
end

if not exists(select 1 from SysViewType where SysViewTypeID = 3)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(3,'102','Db_v_UserPermissionData')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='102',
SysViewName='Db_v_UserPermissionData'
    where SysViewTypeID = 3
end

if not exists(select 1 from SysViewType where SysViewTypeID = 11)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(11,'103','DbPermission')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='103',
SysViewName='DbPermission'
    where SysViewTypeID = 11
end

if not exists(select 1 from SysViewType where SysViewTypeID = 12)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(12,'104','DbPermissionData')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='104',
SysViewName='DbPermissionData'
    where SysViewTypeID = 12
end

if not exists(select 1 from SysViewType where SysViewTypeID = 13)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(13,'105','Db_v_ReportTemplate')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='105',
SysViewName='Db_v_ReportTemplate'
    where SysViewTypeID = 13
end

if not exists(select 1 from SysViewType where SysViewTypeID = 14)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(14,'106','SbSysLog')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='106',
SysViewName='SbSysLog'
    where SysViewTypeID = 14
end

if not exists(select 1 from SysViewType where SysViewTypeID = 15)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(15,'107','DbUserPermission')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='107',
SysViewName='DbUserPermission'
    where SysViewTypeID = 15
end

if not exists(select 1 from SysViewType where SysViewTypeID = 16)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(16,'108','DbBackUpConfig')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='108',
SysViewName='DbBackUpConfig'
    where SysViewTypeID = 16
end

if not exists(select 1 from SysViewType where SysViewTypeID = 17)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(17,'109','RPReceiveBillHeader')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='109',
SysViewName='RPReceiveBillHeader'
    where SysViewTypeID = 17
end

if not exists(select 1 from SysViewType where SysViewTypeID = 18)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(18,'110','DbCustomer')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='110',
SysViewName='DbCustomer'
    where SysViewTypeID = 18
end

if not exists(select 1 from SysViewType where SysViewTypeID = 19)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(19,'111','RP_v_ReceiveBillHeader')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='111',
SysViewName='RP_v_ReceiveBillHeader'
    where SysViewTypeID = 19
end

if not exists(select 1 from SysViewType where SysViewTypeID = 20)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(20,'112','Db_v_Customer')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='112',
SysViewName='Db_v_Customer'
    where SysViewTypeID = 20
end

if not exists(select 1 from SysViewType where SysViewTypeID = 21)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(21,'113','DbCar')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='113',
SysViewName='DbCar'
    where SysViewTypeID = 21
end

if not exists(select 1 from SysViewType where SysViewTypeID = 22)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(22,'203','Db_v_ItemBase')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='203',
SysViewName='Db_v_ItemBase'
    where SysViewTypeID = 22
end

if not exists(select 1 from SysViewType where SysViewTypeID = 23)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(23,'114','View_ModifyBillHeaderDetail')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='114',
SysViewName='View_ModifyBillHeaderDetail'
    where SysViewTypeID = 23
end

if not exists(select 1 from SysViewType where SysViewTypeID = 24)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(24,'115','View_ModifyBillHeader')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='115',
SysViewName='View_ModifyBillHeader'
    where SysViewTypeID = 24
end

if not exists(select 1 from SysViewType where SysViewTypeID = 25)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(25,'116','View_ModifyBillDetail')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='116',
SysViewName='View_ModifyBillDetail'
    where SysViewTypeID = 25
end

if not exists(select 1 from SysViewType where SysViewTypeID = 26)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(26,'117','Db_v_Car')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='117',
SysViewName='Db_v_Car'
    where SysViewTypeID = 26
end

if not exists(select 1 from SysViewType where SysViewTypeID = 27)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(27,'118','DbWeightDeviceConfig')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='118',
SysViewName='DbWeightDeviceConfig'
    where SysViewTypeID = 27
end

if not exists(select 1 from SysViewType where SysViewTypeID = 28)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(28,'119','DbWeightDeviceUserConfig')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='119',
SysViewName='DbWeightDeviceUserConfig'
    where SysViewTypeID = 28
end

if not exists(select 1 from SysViewType where SysViewTypeID = 29)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(29,'120','DbWeightDeviceUserType')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='120',
SysViewName='DbWeightDeviceUserType'
    where SysViewTypeID = 29
end

if not exists(select 1 from SysViewType where SysViewTypeID = 30)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(30,'121','DbDescription')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='121',
SysViewName='DbDescription'
    where SysViewTypeID = 30
end

if not exists(select 1 from SysViewType where SysViewTypeID = 32)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(32,'122','DbCameraConfig')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='122',
SysViewName='DbCameraConfig'
    where SysViewTypeID = 32
end

if not exists(select 1 from SysViewType where SysViewTypeID = 33)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(33,'123','SaleCarInBill')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='123',
SysViewName='SaleCarInBill'
    where SysViewTypeID = 33
end

if not exists(select 1 from SysViewType where SysViewTypeID = 35)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(35,'124','SaleCarOutBill')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='124',
SysViewName='SaleCarOutBill'
    where SysViewTypeID = 35
end

if not exists(select 1 from SysViewType where SysViewTypeID = 36)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(36,'125','View_SaleCarInOutBill')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='125',
SysViewName='View_SaleCarInOutBill'
    where SysViewTypeID = 36
end

if not exists(select 1 from SysViewType where SysViewTypeID = 37)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(37,'126','DbWeightType')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='126',
SysViewName='DbWeightType'
    where SysViewTypeID = 37
end

if not exists(select 1 from SysViewType where SysViewTypeID = 38)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(38,'127','View_DbCarSaleBillInfo')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='127',
SysViewName='View_DbCarSaleBillInfo'
    where SysViewTypeID = 38
end

if not exists(select 1 from SysViewType where SysViewTypeID = 39)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(39,'128','View_SaleCarInBill')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='128',
SysViewName='View_SaleCarInBill'
    where SysViewTypeID = 39
end

if not exists(select 1 from SysViewType where SysViewTypeID = 40)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(40,'129','Db_v_SysConfigValue')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='129',
SysViewName='Db_v_SysConfigValue'
    where SysViewTypeID = 40
end

if not exists(select 1 from SysViewType where SysViewTypeID = 41)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(41,'130','Db_v_CarWeight')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='130',
SysViewName='Db_v_CarWeight'
    where SysViewTypeID = 41
end

if not exists(select 1 from SysViewType where SysViewTypeID = 42)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(42,'131','DbCarWeight')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='131',
SysViewName='DbCarWeight'
    where SysViewTypeID = 42
end

if not exists(select 1 from SysViewType where SysViewTypeID = 43)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(43,'132','DbReportView')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='132',
SysViewName='DbReportView'
    where SysViewTypeID = 43
end

if not exists(select 1 from SysViewType where SysViewTypeID = 44)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(44,'133','DbReportViewField')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='133',
SysViewName='DbReportViewField'
    where SysViewTypeID = 44
end

if not exists(select 1 from SysViewType where SysViewTypeID = 45)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(45,'134','DbInfraredDeviceConfig')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='134',
SysViewName='DbInfraredDeviceConfig'
    where SysViewTypeID = 45
end

if not exists(select 1 from SysViewType where SysViewTypeID = 46)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(46,'135','View_SaleCarInBillNotOut')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='135',
SysViewName='View_SaleCarInBillNotOut'
    where SysViewTypeID = 46
end

if not exists(select 1 from SysViewType where SysViewTypeID = 47)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(47,'136','DbReceiveBank')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='136',
SysViewName='DbReceiveBank'
    where SysViewTypeID = 47
end

if not exists(select 1 from SysViewType where SysViewTypeID = 48)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(48,'137','View_SaleCarReturnBill')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='137',
SysViewName='View_SaleCarReturnBill'
    where SysViewTypeID = 48
end

if not exists(select 1 from SysViewType where SysViewTypeID = 49)
begin
    set IDENTITY_INSERT sysviewtype on
insert sysviewtype (SysViewTypeID,SysViewType,SysViewName ) values(49,'138','DbChargeType')
set IDENTITY_INSERT sysviewtype off

end


else
begin
    update SysViewType
    set SysViewType='138',
SysViewName='DbChargeType'
    where SysViewTypeID = 49
end

