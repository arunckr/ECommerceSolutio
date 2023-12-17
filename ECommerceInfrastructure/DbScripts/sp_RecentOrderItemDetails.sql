CREATE PROC [dbo].[spRecentOrderItemDetails]	--RecentOrderItemDetails
	@LastOrderId INT
	AS
BEGIN
	
	DECLARE @IsContianGift BIT =(SELECT TOP 1 CONTAINSGIFT
		FROM ORDERS
		WHERE ORDERID=@LastOrderId)

	    SELECT
        CASE WHEN @IsContianGift = 1 THEN 'Gift' ELSE P.PRODUCTNAME END AS product,
        OD.QUANTITY AS quantity,
        OD.PRICE AS priceEach
		FROM ORDERITEMS OD
		LEFT JOIN PRODUCTS P ON P.PRODUCTID = OD.PRODUCTID
		WHERE OD.ORDERID = @LastOrderId;

END