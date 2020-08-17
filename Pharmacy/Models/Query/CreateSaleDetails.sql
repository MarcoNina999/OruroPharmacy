CREATE PROCEDURE [dbo].[sp_CreateSaleDetails]
	@id INT,
	@billId INT,
	@productId INT,
	@quantity INT,
	@unitPrice REAL,
	@discount REAL,
	@subTotal REAL,
	@iva REAL,
	@netTotal INT
AS
Begin
	SET IDENTITY_INSERT SaleDetails ON
	INSERT SaleDetails(Id,BillId,ProductId,Quantity,UnitPrice,Discount,SubTotal,IVA,NetTotal) VALUES  (@id,@billId,@productId,@quantity,@unitPrice,@discount,@subTotal,@iva,@netTotal)
	SET IDENTITY_INSERT SaleDetails OFF
END