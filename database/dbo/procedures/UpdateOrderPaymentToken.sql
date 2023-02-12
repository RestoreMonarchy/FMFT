CREATE PROCEDURE dbo.UpdateOrderPaymentToken
	@OrderId INT,
	@PaymentToken VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ret INT = 0;
    DECLARE @id CHAR(8) = '--------';

	IF NOT EXISTS (SELECT * FROM dbo.Orders WHERE Id = @OrderId)
        SET @ret = 1;

	IF @ret = 0
    BEGIN
		SET @id = @OrderId;

        UPDATE dbo.Orders SET 
		PaymentToken = @PaymentToken,
		UpdateDate = SYSDATETIME()
		WHERE Id = @OrderId;
	END;

	EXEC dbo.GetOrders @OrderId = @id;    
    RETURN @ret;
END
