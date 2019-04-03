using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using Newtonsoft.Json;

namespace Projects
{
       public class VaultApplication
        : VaultApplicationBase
    {
        #region Identifiers
        [MFPropertyDef(Required = true)]
        private MFIdentifier hoursProp = "Prop.Hours";
        [MFPropertyDef(Required = true)]
        private MFIdentifier usedHoursProp = "Prop.HoursUsed";
        [MFPropertyDef(Required = true)]
        private MFIdentifier purchasedHoursProp = "Prop.HoursPurchased";
        [MFPropertyDef(Required = true)]
        private MFIdentifier projectProp = "Prop.Job";
        [MFPropertyDef(Required = true)]
        private MFIdentifier phaseProp = "Prop.Phase";
        [MFPropertyDef(Required = true)]
        private MFIdentifier VersionProp = "Prop.InternalVersion";
        [MFClass(Required = true)]
        private MFIdentifier invoiceClass = "Class.InvoiceDetail";
        [MFClass(Required = true)]
        private MFIdentifier phaseClass = "Class.Phase";
        [MFClass(Required = true)]
        private MFIdentifier projectClass = "Class.Job";
        [MFObjType(Required = true)]
        private MFIdentifier timesheetObj = "Obj.TimesheetLine";
        [MFObjType(Required = true)]
        private MFIdentifier projectObj = "Obj.Job";
        [MFObjType(Required = true)]
        private MFIdentifier phaseObj = "Obj.Phase";
        #endregion

        [PropertyCustomValue("Prop.HoursUsed")]
        public TypedValue UsedHours(PropertyEnvironment env)
        {
            TypedValue typedValue = new TypedValueClass();
            float num = 0;
            MFSearchBuilder mfSearchBuilder = new MFSearchBuilder(env.Vault);
            mfSearchBuilder.Deleted(false);
            mfSearchBuilder.ObjType(timesheetObj.ID);
            mfSearchBuilder.Conditions.AddPropertyCondition(projectProp, MFConditionType.MFConditionTypeEqual, MFDataType.MFDatatypeLookup, env.ObjVerEx.ID);
            var searchResults = mfSearchBuilder.FindEx();
            foreach(var searchResult in searchResults)
                num += float.Parse(searchResult.GetProperty(hoursProp).TypedValue.GetValueAsUnlocalizedText());
            typedValue.SetValue(MFDataType.MFDatatypeFloating, num);
            return typedValue;
        }

        [PropertyCustomValue("Prop.HoursPurchased")]
        public TypedValue PurchasedHours(PropertyEnvironment env)
        {
            TypedValue typedValue = new TypedValueClass();
            float num = 0;
            MFSearchBuilder mfSearchBuilder = new MFSearchBuilder(env.Vault);
            mfSearchBuilder.Deleted(false);
            mfSearchBuilder.Class(invoiceClass.ID);
            if (env.ObjVerEx.Class == projectClass.ID)
            {
                mfSearchBuilder.Conditions.AddPropertyCondition(projectProp, MFConditionType.MFConditionTypeEqual, MFDataType.MFDatatypeLookup, env.ObjVerEx.ID);
            }
                if (env.ObjVerEx.Class == phaseClass.ID)
            {
                mfSearchBuilder.Conditions.AddPropertyCondition(phaseProp, MFConditionType.MFConditionTypeEqual, MFDataType.MFDatatypeLookup, env.ObjVerEx.ID);
            }
            var searchResults = mfSearchBuilder.FindEx();
            foreach (var searchResult in searchResults)
                num += float.Parse(searchResult.GetProperty(hoursProp).TypedValue.GetValueAsUnlocalizedText());
            typedValue.SetValue(MFDataType.MFDatatypeFloating, num);
            return typedValue;
        }

        [PropertyCustomValue("Prop.HoursLeft")]
        public TypedValue RemainingHours(PropertyEnvironment env)
        {
            TypedValue typedValue = new TypedValueClass();
            float num = float.Parse(env.ObjVerEx.GetProperty(purchasedHoursProp).TypedValue.GetValueAsUnlocalizedText()) - float.Parse(env.ObjVerEx.GetProperty(usedHoursProp).TypedValue.GetValueAsUnlocalizedText());
            typedValue.SetValue(MFDataType.MFDatatypeFloating, num);
            return typedValue;
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize)]
        private void CheckinFileChanges(EventHandlerEnvironment env)
        {
            if (env.ObjVerEx.Type == projectObj)
            {
                this.syncData(env.ObjVerEx, "UpdateProject", 5);
                            }

            if (env.ObjVerEx.Type == phaseObj)
            {
                this.syncData(env.ObjVerEx, "UpdatePhase", 5);
            }

            if (!(env.ObjVerEx.Class == invoiceClass.ID | env.ObjVerEx.Type == timesheetObj.ID))
                return;
            MFSearchBuilder projectSearch = new MFSearchBuilder(env.Vault);
            projectSearch.Deleted(false);
            projectSearch.ObjType(projectObj);
            projectSearch.Object(env.ObjVerEx.GetLookupID(projectProp));
            var projectResults = projectSearch.FindEx();
            foreach (var searchResult in projectResults)
            {
                bool start = searchResult.StartRequireCheckedOut();
                searchResult.SetProperty(VersionProp, MFDataType.MFDatatypeInteger, float.Parse(searchResult.GetPropertyText(VersionProp)) + 1.0);
                searchResult.SaveProperties();
                searchResult.EndRequireCheckedOut(start);
                //this.syncData(searchResult, "UpdateProject", 5);
            }
            MFSearchBuilder phaseSearch = new MFSearchBuilder(env.Vault);
            phaseSearch.Deleted(false);
            phaseSearch.ObjType(phaseObj);
            phaseSearch.Object(env.ObjVerEx.GetLookupID(phaseProp));
            var phaseResults = phaseSearch.FindEx();
            foreach(var phaseResult in phaseResults)
            {
                bool start = phaseResult.StartRequireCheckedOut();
                phaseResult.SetProperty(VersionProp, MFDataType.MFDatatypeInteger, float.Parse(phaseResult.GetPropertyText(VersionProp)) + 1.0);
                phaseResult.SaveProperties();
                phaseResult.EndRequireCheckedOut(start);
                //this.syncData(phaseResult, "UpdatePhase", 5);
            }
            this.syncData(env.ObjVerEx, "UpdateTimesheet", 5);
        }

        public void syncData(ObjVerEx objectToMove, string actionName, int actionTypeId)
        {
            var data = new
            {
                ObjectID = objectToMove.ID,
                ObjectType = objectToMove.Type,
                Objectver = objectToMove.Version,
                ClassID = objectToMove.Class,
                ActionName = actionName,
                ActionTypeID = actionTypeId
            };
            this.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("PerformActionMethod", JsonConvert.SerializeObject((object)data));
        }
    }
}