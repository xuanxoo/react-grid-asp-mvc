-- =============================================
-- Author:		Juanjo Pérez
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FundsPaginated]
	-- Add the parameters for the stored procedure here
	@PageNumber as int,
	@RowsPage as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM (
				 SELECT ROW_NUMBER() OVER(ORDER BY Id) AS NUMBER,
						Id, Name, Description, Value, Date FROM V_FundWithLastValue
				   ) AS TBL
	WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RowsPage + 1) AND (@PageNumber * @RowsPage)
	ORDER BY Id
END