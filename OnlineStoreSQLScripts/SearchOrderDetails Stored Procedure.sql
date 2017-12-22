SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns all order details that contain
-- =============================================
CREATE PROCEDURE [dbo].[SearchOrderDetails]
	@Email nvarchar(max),
	@SearchInputText nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT p.Name as ProductName, o.*, od.*
	FROM dbo.OrderDetails od
	JOIN dbo.Orders o ON od.OrderID = o.OrderID
	JOIN dbo.Products p ON od.ProductID = p.ProductID
	WHERE o.Email = @Email and p.Name LIKE '%' + @SearchInputText + '%'  
END