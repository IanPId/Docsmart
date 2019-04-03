/****** Object:  StoredProcedure [dbo].[spDSGetProjectData]    Script Date: 3/04/2019 4:56:32 PM ******/
IF EXISTS (Select * from sys.objects Where type = 'P' and name = 'spDSGetProjectData' )
	DROP PROCEDURE [dbo].[spDSGetProjectData]
GO

/****** Object:  StoredProcedure [dbo].[spDSGetProjectData]    Script Date: 3/04/2019 4:56:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDSGetProjectData]
(@ProjectID int)
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 Select Name_Or_Title, ObjID, ID, Hours_Purchased, Hours_Left , Hours_Used, Customer , '' as Contact_Person, '' AS Phone_Number From MFProject
 where ObjID = @ProjectID
END

GO


