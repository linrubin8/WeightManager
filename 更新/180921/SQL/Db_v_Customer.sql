ALTER VIEW [dbo].[Db_v_Customer]
AS
SELECT     c.CustomerID, c.CustomerName, c.CustomerCode, c.K3CustomerCode, c.Contact, c.Phone, c.Address, c.CarIsLimit, c.AmountType, c.LicenceNum, c.Description, 
                      c.IsForbid, c.ReceiveType,
                          (SELECT     ConstText
                            FROM          dbo.DbSystemConst
                            WHERE      (FieldName = 'ReceiveType') AND (ConstValue = c.ReceiveType)) AS ReceiveTypeName,
                          (SELECT     ConstText
                            FROM          dbo.DbSystemConst AS DbSystemConst_1
                            WHERE      (FieldName = 'AmountType') AND (ConstValue = c.AmountType)) AS AmountTypeName, c.CreditAmount, c.IsDisplayPrice, c.IsDisplayAmount, 
                      c.IsPrintAmount, c.IsAllowOverFul, c.CreateBy, c.CreateTime, c.ChangeBy, c.ChangeTime, ISNULL(c.SalesReceivedAmount, 0) AS SalesReceivedAmount, 
                      ISNULL(c.TotalReceivedAmount, 0) AS TotalReceivedAmount, ISNULL(c.TotalReceivedAmount, 0) - ISNULL(c.SalesReceivedAmount, 0) AS RemainReceivedAmount, 
                      ISNULL(c.IsAllowEmptyIn, 0) AS IsAllowEmptyIn, ISNULL(c.SortLevel, 0) AS SortLevel, c.AmountNotEnough, c.ForbidBy, c.ForbidTime, t.CustomerTypeID, 
                      t.CustomerTypeCode, t.CustomerTypeName
FROM         dbo.DbCustomer AS c LEFT OUTER JOIN
                      dbo.DbCustomerType AS t ON t.CustomerTypeID = c.CustomerTypeID