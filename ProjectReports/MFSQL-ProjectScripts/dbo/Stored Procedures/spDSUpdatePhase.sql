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