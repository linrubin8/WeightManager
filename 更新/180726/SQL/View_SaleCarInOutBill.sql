ALTER VIEW [dbo].[View_SaleCarInOutBill]
AS
SELECT     i.SaleCarInBillID, i.BillTypeID, o.SaleCarOutBillCode, i.SaleCarInBillCode, i.CarID, c.CarNum, i.ItemID, b.ItemName, b.ItemRate, b.ItemMode, i.BillDate AS BillDateIn, i.ReceiveType, 
                      ISNULL(i.BillStatus, 0) AS BillStatus, i.CustomerID, s.CustomerName, i.CalculateType, i.CarTare, i.PrintCount, i.IsCancel, i.ApproveBy, i.ApproveTime, i.CreateBy AS CreateByIn, 
                      i.CreateTime AS CreateTimeIn, i.CancelBy, i.CancelTime, o.SaleCarOutBillID, o.BillDate AS BillDateOut, o.TotalWeight, o.SuttleWeight, o.SuttleWeight / 1000.0 AS SuttleWeightT, o.Price, 
                      o.Price * 1000 AS PriceT, o.Amount, o.Description, o.CreateBy AS CreateByOut, CAST(CONVERT(char, o.CreateTime, 120) AS datetime) AS CreateTimeOut, CAST((CASE WHEN isnull(i.IsCancel, 0) 
                      = 1 THEN 2 WHEN isnull(i.BillStatus, 0) = 2 THEN 1 ELSE 0 END) AS tinyint) AS BillType, i.CancelDesc, o.OutPrintCount, y.ItemTypeName, ISNULL(i.SaleBillType, 0) AS SaleBillType, 
                      ISNULL(i.IsSynchronousToServer, 0) AS IsSynchronousToServer, i.SynchronousToServerTime, s.AmountType, o.CreateTime,o.MaterialPrice, o.FarePrice, o.TaxPrice, o.BrokerPrice, o.ChangeDetail, 
                      i.IsSynchronousToK3OutBill, i.SynchronousK3ByOutBill, i.SynchronousToTimeOutBill,i.SynchronousK3OutBillResult,
                      i.IsSynchronousToK3Receive, i.SynchronousK3ByReceive, i.SynchronousToTimeReceive,i.SynchronousK3ReceiveResult,
                      b.K3ItemCode,s.K3CustomerCode
FROM         dbo.SaleCarInBill AS i INNER JOIN
                      dbo.DbCar AS c ON c.CarID = i.CarID LEFT OUTER JOIN
                      dbo.DbCustomer AS s ON s.CustomerID = i.CustomerID INNER JOIN
                      dbo.DbItemBase AS b ON b.ItemID = i.ItemID LEFT OUTER JOIN
                      dbo.SaleCarOutBill AS o ON i.SaleCarInBillID = o.SaleCarInBillID LEFT OUTER JOIN
                      dbo.DbItemType AS y ON y.ItemTypeID = b.ItemTypeID
