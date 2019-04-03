/****** Object:  StoredProcedure [dbo].[spDSGetTimesheetData]    Script Date: 3/04/2019 4:56:37 PM ******/
IF EXISTS (Select * from sys.objects Where type = 'P' and name = 'spDSGetTimesheetData' )
	DROP PROCEDURE [dbo].[spDSGetTimesheetData]
GO

/****** Object:  StoredProcedure [dbo].[spDSGetTimesheetData]    Script Date: 3/04/2019 4:56:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDSGetTimesheetData]
(@ProjectID int, @Status Int, @ConsultantID int)
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
Declare @SQL varchar(4000)
    -- Insert statements for procedure here
 Set @SQL = 'Select t.Name_Or_Title, t.ObjID, t.ID, t.[Hours], t.Consultant, t.[Description], t.[Date], t.Invoiced_By_Consultant, t.Job, p.Customer'
 
 Set @SQL = @SQL + ' From MFTimesheetLine t Left Join MFProject p on Job_ID = p.objID where t.ID > 0'

 If  @ProjectID <> 0
	Set @SQL = @SQL + ' and Job_ID = ' + Cast(@ProjectID as varchar)
if @ConsultantID <> 0
	Set @SQL = @SQL + ' and Consultant_ID = ' + Cast(@ConsultantID as varchar)
If @Status <> 100
	Set @SQL = @SQL + ' and Invoiced_By_Consultant = ' + cast(@Status as varchar)
	


--print @SQL
exec(@SQL)
--Select Name_Or_Title, ObjID, ID, [Hours], Consultant, [Description], [Date], Invoiced_By_Consultant  From MFTimesheetLine

END

GO


