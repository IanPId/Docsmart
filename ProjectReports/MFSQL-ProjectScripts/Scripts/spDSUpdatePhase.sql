/****** Object:  StoredProcedure [dbo].[spDSUpdatePhase]    Script Date: 3/04/2019 4:56:44 PM ******/
IF EXISTS (Select * from sys.objects Where type = 'P' and name = 'spDSUpdatePhase' )
	DROP PROCEDURE [dbo].[spDSUpdatePhase]
GO

/****** Object:  StoredProcedure [dbo].[spDSUpdatePhase]    Script Date: 3/04/2019 4:56:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ian Piddington
-- Create date: 13/3/2019
-- Description:	Syncs specific Project object and assoicated timesheet and invoice objects
-- =============================================
CREATE PROCEDURE [dbo].[spDSUpdatePhase]
	@ObjectID INT
	, @ObjectType INT
	, @ObjectVer INT
	, @OutPut VARCHAR(1000) OUTPUT
	, @ClassID int
	, @ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Sync Project Object
	exec spMFUpdateTable @MFTableName = "MFPhase", @UpdateMethod = 1, @ObjIDs =@ObjectID
	
END
GO


