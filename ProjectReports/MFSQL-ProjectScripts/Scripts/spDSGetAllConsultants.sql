/****** Object:  StoredProcedure [dbo].[spDSGetAllConsultants]    Script Date: 3/04/2019 4:56:18 PM ******/
IF EXISTS (Select * from sys.objects Where type = 'P' and name = 'spDSGetAllConsultants' )
	DROP PROCEDURE [dbo].[spDSGetAllConsultants]
GO

/****** Object:  StoredProcedure [dbo].[spDSGetAllConsultants]    Script Date: 3/04/2019 4:56:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDSGetAllConsultants]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 Select Name_Or_Title, ObjID, ID From MFConsultant
 union
 Select 'All',0,0
 
 order by ObjID
 End
GO


