-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDSGetInvoiceData]
(@ProjectID int)
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 Select Name_Or_Title, ObjID, ID, [Hours], Paid, Amount_Ex_Gst, Amount_Inc_Gst, Date, Invoice_Number, Description From MFInvoiceDetail


 where Job_ID = @ProjectID
END