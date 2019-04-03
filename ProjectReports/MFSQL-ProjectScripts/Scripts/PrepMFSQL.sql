/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
exec spMFSynchronizeMetadata
Go
exec spMFCreateTable @ClassName = 'Consultant'
exec spMFCreateTable @ClassName = 'Invoice Detail'
exec spMFCreateTable @ClassName = 'Phase'
exec spMFCreateTable @ClassName = 'Project'
exec spMFCreateTable @ClassName = 'Timesheet Line'
go
exec spMFUpdateTable @MFTableName = 'MFConsultant', @UpdateMethod = 1
exec spMFUpdateTable @MFTableName = 'MFInvoiceDetail', @UpdateMethod = 1
exec spMFUpdateTable @MFTableName = 'Phase', @UpdateMethod = 1
exec spMFUpdateTable @MFTableName = 'Project', @UpdateMethod = 1
exec spMFUpdateTable @MFTableName = 'TimesheetLine', @UpdateMethod = 1
go

INSERT INTO [dbo].[MFContextMenu]
           ([ActionName]
           ,[Action]
           ,[ActionType]
           ,[Message]
           ,[SortOrder]
           ,[ParentID]
           ,[IsProcessRunning]
           ,[ISAsync]
           ,[UserGroupID])
     VALUES
           ('UpdateTimesheet','spDSUpdateTimesheet',5,NULL,NULL,0,0,0,1),
           ('UpdateProject','spDSUpdateProject',5,NULL,NULL,0,0,0,1),
		   ('UpdatePhase','spDSUpdatePhase',5,NULL,NULL,0,0,0,1)
go

