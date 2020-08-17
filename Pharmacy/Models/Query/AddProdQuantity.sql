CREATE PROCEDURE [dbo].[sp_AddQuantity]
	@id INT,
	@quantity INT
AS
BEGIN
	UPDATE Products
	SET Quantity = @quantity
	WHERE Id = @id
END