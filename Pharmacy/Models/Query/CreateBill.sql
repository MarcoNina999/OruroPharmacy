CREATE PROCEDURE [dbo].[sp_CreateBill]
	@id INT,
	@userId NVARCHAR(128),
	@saleId INT,
	@sellerId NVARCHAR(128),
	@create_at DATE,
	@subTotal REAL,
	@discount REAL,
	@bonus REAL,
	@totalIva INT,
	@total REAL
AS
BEGIN
	SET IDENTITY_INSERT Bills ON
	INSERT Bills(Id,UserId,SaleId,SellerId,Create_at,SubTotal,Discount,Bonus,TotalIVA,Total) VALUES  (@id,@userId,@saleId,@sellerId,@create_at,@subTotal,@discount,@bonus,@totalIva,@total)
	SET IDENTITY_INSERT Bills OFF
END