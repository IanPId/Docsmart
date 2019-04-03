/****** Object:  StoredProcedure [dbo].[spDSGetAllProjects]    Script Date: 3/04/2019 4:56:23 PM ******/
IF EXISTS (Select * from sys.objects Where type = 'P' and name = 'spDSGetAllProjects' )
	DROP PROCEDURE [dbo].[spDSGetAllProjects]
GO

/****** Object:  StoredProcedure [dbo].[spDSGetAllProjects]    Script Date: 3/04/2019 4:56:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDSGetAllProjects]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 Select Name_Or_Title, ObjID, ID From MFProject
  union
 Select 'All',0,0

 order by ObjID
END
GO


