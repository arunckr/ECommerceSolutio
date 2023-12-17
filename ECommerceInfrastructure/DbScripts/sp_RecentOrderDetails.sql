
CREATE PROC [dbo].[RecentOrderDetails]	-- RecentOrderDetails
	@User NVARCHAR(50),
	@CustomerId NVARCHAR(10)
	AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM CUSTOMERS WHERE CUSTOMERID=@CustomerId AND EMAIL=@User)
	BEGIN 
		RAISERROR(' No Customer  exist for this Email and Customer Id.',16,1)
	END
	ELSE
	BEGIN
		DECLARE @RecentOrderId INT =(SELECT TOP 1 ORDERID
									FROM ORDERS
									WHERE CUSTOMERID=@CustomerId
									ORDER BY ORDERDATE DESC)

		SELECT C.FIRSTNAME firstName, C.LASTNAME lastName, ORDERID orderNumber, ORDERDATE orderDate, DELIVERYEXPECTED deliveryExpected,
			(HOUSENO+' '+STREET+', '+TOWN+', '+POSTCODE) AS deliveryAddress
		FROM CUSTOMERS C
		LEFT JOIN ORDERS O
			ON O.ORDERID=@RecentOrderId 			
			WHERE C.CUSTOMERID=@CustomerId
		
	END
END
GO
