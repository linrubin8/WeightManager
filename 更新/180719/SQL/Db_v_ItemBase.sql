ALTER VIEW [dbo].[Db_v_ItemBase]
AS
SELECT     i.ItemID, i.ItemTypeID, t.ItemTypeName, i.ItemCode, i.ItemName, i.ItemMode, i.ItemRate, i.UOMID, u.UOMName, u.UOMType, i.Description, i.IsForbid, i.ChangeBy, 
                      i.ChangeTime, ISNULL(i.ItemPrice, 0) AS ItemPrice,K3ItemCode
FROM         dbo.DbItemBase AS i LEFT OUTER JOIN
                      dbo.DbItemType AS t ON t.ItemTypeID = i.ItemTypeID LEFT OUTER JOIN
                      dbo.DbUOM AS u ON u.UOMID = i.UOMID