-- =============================================
-- Author:		Ian Piddington
-- Create date: 13/3/2019
-- Description:	Syncs specific Project object and assoicated timesheet and invoice objects
-- =============================================
CREATE PROCEDURE [dbo].[spDSUpdateTimesheet]
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

	Declare @TimeSheetObjType int
	select @TimeSheetObjType = (select MFID from MFObjectType where Alias = 'Obj.TimesheetLine')
	
	If @ObjectType = @TimeSheetObjType
		-- Find and sync Timesheet objects
		exec spMFUpdateTable @MFTableName = "MFTimesheetLine", @UpdateMethod = 1, @ObjIDs = @ObjectID
	else	
		-- Sync Invocie Lines
		exec spMFUpdateTable @MFTableName = "MFInvoiceDetail", @UpdateMethod = 1, @ObjIDs = @ObjectID
	
END