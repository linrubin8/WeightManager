ALTER VIEW [dbo].[Db_v_Customer]
AS
SELECT     CustomerID, CustomerName, CustomerCode, K3CustomerCode, Contact, Phone, Address, CarIsLimit, AmountType, LicenceNum, Description, IsForbid, ReceiveType,
                          (SELECT     ConstText
                            FROM          dbo.DbSystemConst
                            WHERE      (FieldName = 'ReceiveType') AND (ConstValue = dbo.DbCustomer.ReceiveType)) AS ReceiveTypeName,
                          (SELECT     ConstText
                            FROM          dbo.DbSystemConst AS DbSystemConst_1
                            WHERE      (FieldName = 'AmountType') AND (ConstValue = dbo.DbCustomer.AmountType)) AS AmountTypeName, CreditAmount, IsDisplayPrice, IsDisplayAmount, IsPrintAmount, IsAllowOverFul, 
                      CreateBy, CreateTime, ChangeBy, ChangeTime, ISNULL(SalesReceivedAmount, 0) AS SalesReceivedAmount, ISNULL(TotalReceivedAmount, 0) AS TotalReceivedAmount, 
                      ISNULL(TotalReceivedAmount, 0) - ISNULL(SalesReceivedAmount, 0) AS RemainReceivedAmount, ISNULL(IsAllowEmptyIn, 0) AS IsAllowEmptyIn, ISNULL(SortLevel, 0) AS SortLevel, 
                      AmountNotEnough
FROM         dbo.DbCustomer