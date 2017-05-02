-- =============================================
-- Author:		Juanjo Pérez
-- Create date: 30-04-2017
-- Description: Pull data to fund tables
-- =============================================
CREATE PROCEDURE PopulateData
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @i as int 
	declare @FundId as int
	set @i = 1 

	begin transaction 
	while(@i < 1000001) 
	begin    
		INSERT INTO Fund (Name, Description) VALUES ('Fund Name ' + CAST(@i AS VARCHAR), 'Fund Description ' + CAST(@i AS VARCHAR)) 
		SET @FundId = (SELECT SCOPE_IDENTITY())
		INSERT INTO [dbo].[FundValues] ([FundId],[Date],[Value]) VALUES (@FundId, GETDATE(), ROUND(RAND(CHECKSUM(NEWID())) * (3357-272),0) + 272)
		set @i += 1 
	end
	commit transaction
END