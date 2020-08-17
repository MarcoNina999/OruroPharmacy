CREATE PROCEDURE [dbo].[sp_BillReport]
	@billId INT	
AS
BEGIN
	SELECT s.*, p.Name, u.First_Name+' '+u.Last_Name AS Nombre, u.Ci, u.Email FROM SaleDetails s, Bills b, Products p, AspNetUsers u WHERE p.Id LIKE s.ProductId AND u.Id LIKE b.UserId AND s.BillId LIKE b.Id AND b.Id LIKE @billId
END