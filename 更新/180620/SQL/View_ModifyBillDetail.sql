ALTER VIEW [dbo].[View_ModifyBillDetail]
AS
SELECT     d.ModifyBillHeaderID, d.ModifyBillDetailID, d.ItemID, i.ItemCode, i.ItemName, i.ItemMode, i.ItemRate, d.CarID, ca.CarNum, d.Price, d.CalculateType,
                          (SELECT     ConstText
                            FROM          dbo.DbSystemConst
                            WHERE      (FieldName = 'CalculateType') AND (ConstValue = d.CalculateType)) AS CalculateTypeName, d.UOMID, u.UOMName, d.Description, d.ChangeBy, d.ChangeTime, d.CreateBy, 
                      d.CreateTime, d.MaterialPrice, d.FarePrice, d.TaxPrice, d.BrokerPrice
FROM         dbo.ModifyBillDetail AS d INNER JOIN
                      dbo.DbItemBase AS i ON i.ItemID = d.ItemID INNER JOIN
                      dbo.DbUOM AS u ON u.UOMID = d.UOMID LEFT OUTER JOIN
                      dbo.DbCar AS ca ON ca.CarID = d.CarID