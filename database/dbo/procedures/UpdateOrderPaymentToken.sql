CREATE PROCEDURE dbo.UpdateOrderPaymentToken
	@OrderId INT,
	@PaymentToken VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	UPDATE dbo.Orders SET 
	PaymentToken = @PaymentToken,
	UpdateDate = SYSDATETIME()
	WHERE Id = @OrderId;

	EXEC dbo.GetOrders @OrderId = @OrderId;
	RETURN 0;
END
