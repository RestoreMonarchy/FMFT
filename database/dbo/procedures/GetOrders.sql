CREATE PROCEDURE dbo.GetOrders
    @OrderId INT = NULL,
    @UserId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @UserId IS NOT NULL
    BEGIN
        SELECT o.*, u.*, i.*,sp.*,s.*
        FROM dbo.Orders o
        LEFT JOIN dbo.Users u ON u.Id = o.UserId
        LEFT JOIN dbo.OrderItems i ON i.OrderId = o.Id
        LEFT JOIN dbo.ShowProducts sp ON sp.Id = i.ShowProductId
        LEFT JOIN dbo.Shows s ON s.Id = sp.ShowId
        WHERE o.UserId = @UserId
        ORDER BY o.Id, i.Id;
    END
    ELSE IF @OrderId IS NOT NULL
    BEGIN
        SELECT o.*, u.*, i.*,sp.*,s.*
        FROM dbo.Orders o
        LEFT JOIN dbo.Users u ON u.Id = o.UserId
        LEFT JOIN dbo.OrderItems i ON i.OrderId = o.Id
        LEFT JOIN dbo.ShowProducts sp ON sp.Id = i.ShowProductId
        LEFT JOIN dbo.Shows s ON s.Id = sp.ShowId
        WHERE o.Id = @OrderId
        ORDER BY o.Id, i.Id;
    END
    ELSE
    BEGIN
        SELECT o.*, u.*, i.*,sp.*,s.*
        FROM dbo.Orders o
        LEFT JOIN dbo.Users u ON u.Id = o.UserId
        LEFT JOIN dbo.OrderItems i ON i.OrderId = o.Id
        LEFT JOIN dbo.ShowProducts sp ON sp.Id = i.ShowProductId
        LEFT JOIN dbo.Shows s ON s.Id = sp.ShowId
        ORDER BY o.Id, i.Id;
    END;
    RETURN 0;
END;
