using System;
using System.Linq;
using System.Runtime.Serialization;
using MFiles.VAF;
using MFiles.VAF.AdminConfigurations;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;

namespace ConvertMultiFile
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication
    : VaultApplicationBase, IUsesAdminConfigurations
    {

        /// <summary>
        /// The properties to remove from the cloned property values collection.
        /// </summary>
        public readonly int[] PropertiesToRemove =
        {
(int)MFBuiltInPropertyDef.MFBuiltInPropertyDefWorkflow,
(int)MFBuiltInPropertyDef.MFBuiltInPropertyDefState,
(int)MFBuiltInPropertyDef.MFBuiltInPropertyDefSingleFileObject
};

        public ConfigurationNode<Configuration> Configuration { get; set; }

        /// <summary>
        /// Executed when an object is moved into a workflow state
        /// with alias "MFiles.WorkflowStates.SplitMFD.Split".
        /// </summary>
        /// <param name="env">The vault/object environment.</param>
        //[StateAction("MFiles.WorkflowStates.SplitMFD.Split")]
        //public void SplitMFD(StateEnvironment env)
        //{
        //    // Sanity.
        //    if (null == env?.ObjVer)
        //        return;

        //    // If it is not an MFD then except.
        //    if (env.ObjVerEx.Info.SingleFile)
        //        throw new InvalidOperationException("Single file documents cannot be moved into this state.");

        //    // If it has zero files then except.
        //    if (env.ObjVerEx.Info.FilesCount == 0)
        //    {
        //        throw new InvalidOperationException("Objects with no files cannot be moved into this state.");
        //    }

        protected override void StartApplication()
        {
            this.BackgroundOperations.StartRecurringBackgroundOperation("Convert Multifiles to Document Collections",
                //TimeSpan.FromHours(1), () =>
                TimeSpan.FromMinutes(5), () =>
                {
                    var env = this.PermanentVault;
                    FindMultifile(env);
                });
        }


        public string FindMultifile(Vault env)
        {
            var searchMultiFile = new MFSearchBuilder(env);
            searchMultiFile.Deleted(false);
            searchMultiFile.ObjType(MFBuiltInObjectType.MFBuiltInObjectTypeDocument);
            searchMultiFile.Conditions.AddPropertyCondition(22, MFConditionType.MFConditionTypeEqual, MFDataType.MFDatatypeBoolean, false);
            searchMultiFile.Class(this.Configuration.CurrentConfiguration.ProposalClass);

            var searchMFResults = searchMultiFile.FindEx();
            foreach (var searchResult in searchMFResults)
            {
                // If it is an MFD with one file then convert back.
                if (searchResult.Info.FilesCount == 1)
                {
                    // Set to SFD.
                    searchResult.SaveProperty(
                (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefSingleFileObject,
                MFDataType.MFDatatypeBoolean,
                true);

                    // Audit trail.


                    // Done.
                    return ("");

                }

                try
                {
                    //Get multifile name
                    var MFTitle = searchResult.Title;


                    // Get a collection of documents for the document collection.
                    var createdItems = new Lookups();

                    // Get a copy of the current object's properties.
                    var propertiesCopy = this.GetNewObjectPropertyValues(searchResult.Properties);
                    
                    // For each file, create a new object.
                    foreach (var file in searchResult.Info.Files.Cast<ObjectFile>())
                    {
                        // Add Component Type based on Title
                        if (file.Title.Contains("NPA"))
                        {
                            propertiesCopy.SetProperty(this.Configuration.CurrentConfiguration.ComponentTypeProperty, MFDataType.MFDatatypeLookup, 2);
                        }
                        else if (file.Title.Contains("EOI"))
                        {
                            propertiesCopy.SetProperty(this.Configuration.CurrentConfiguration.ComponentTypeProperty, MFDataType.MFDatatypeLookup, 3);
                        }
                        else if (file.Title.Contains("Price"))
                        {
                            propertiesCopy.SetProperty(this.Configuration.CurrentConfiguration.ComponentTypeProperty, MFDataType.MFDatatypeLookup, 4);
                        }
                        else if (file.Title.Contains("RFT"))
                        {
                            propertiesCopy.SetProperty(this.Configuration.CurrentConfiguration.ComponentTypeProperty, MFDataType.MFDatatypeLookup, 1);
                        }
                        else if (file.Title.Contains("RFP"))
                        {
                            propertiesCopy.SetProperty(this.Configuration.CurrentConfiguration.ComponentTypeProperty, MFDataType.MFDatatypeLookup, 1);
                        }

                        // Download the file.
                        var sourceObjectFiles = this.GetNewObjectSourceFiles(env, file);
                        propertiesCopy.SetProperty(0,MFDataType.MFDatatypeText, file.Title);
                        // Create the new object.
                        var createdObjectId = env.ObjectOperations
                        .CreateNewObjectExQuick(
                        (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument,
                        propertiesCopy.Clone(),
                        sourceObjectFiles,
                        SFD: true);

                        createdItems.Add(-1, new Lookup
                        {
                            ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument,
                            Item = createdObjectId,
                            // TODO: Version???
                        });

                    }

                    // Add the created documents to a property for the collection.
                    {
                        var collectionMembersPropertyValue = new PropertyValue
                        {
                            PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefCollectionMemberDocuments
                        };
                        collectionMembersPropertyValue.Value.SetValueToMultiSelectLookup(createdItems);
                        propertiesCopy.Add(-1, collectionMembersPropertyValue);
                    }

                    propertiesCopy.SetProperty(0, MFDataType.MFDatatypeText, MFTitle);
                    propertiesCopy.RemoveProperty(this.Configuration.CurrentConfiguration.ComponentTypeProperty);
                    propertiesCopy.SetProperty(100, MFDataType.MFDatatypeLookup, this.Configuration.CurrentConfiguration.ProposalCollectionClass);
                    // Create the document collection.
                    var documentCollectionObjVer = new ObjVer
                    {
                        Type = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocumentCollection,
                        ID = env.ObjectOperations.CreateNewObjectExQuick(
                    (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocumentCollection,
                    propertiesCopy)
                    };

                    // Can we reference the collection?
                    if (this.Configuration.CurrentConfiguration.DocumentCollectionReference.IsResolved)
                    {
                        // Find items which referenced this (old) document and instead reference the collection.
                        foreach (var objVerEx in env
                        .ObjectOperations
                        .GetRelationships(searchResult.ObjVer, MFRelationshipsMode.MFRelationshipsModeToThisObject)
                        .Cast<ObjectVersion>()
                        .Select(ov => new ObjVerEx(env, ov)))
                        {
                            var checkout = objVerEx.StartRequireCheckedOut();
                            objVerEx.AddLookup(
                            this.Configuration.CurrentConfiguration.DocumentCollectionReference.ID,
                            documentCollectionObjVer,
                            exactVersion: false);
                            objVerEx.SaveProperties();
                            objVerEx.EndRequireCheckedOut(checkout);
                        }
                    }

                    //Remove orignal File
                    {
                        searchResult.Delete();
                    }

                }
                catch (Exception e)
                {
                    SysUtils.ReportErrorToEventLog(e);

                    // Throw.
                    throw;
                }
            }
            return ("");
        }

        /// <summary>
        /// Downloads the <see cref="objectFile"/>
        /// and creates a <see cref="SourceObjectFiles"/> to be used in new object creation.
        /// </summary>
        /// <param name="vault">The vault connection used to download the files.</param>
        /// <param name="objectFile">The file to copy.</param>
        /// <returns>A copy of the current files, as a <see cref="SourceObjectFiles"/>.</returns>
        private SourceObjectFiles GetNewObjectSourceFiles(Vault vault, ObjectFile objectFile)
        {
            // Sanity.
            if (null == vault)
                throw new ArgumentNullException(nameof(vault));
            if (null == objectFile)
                throw new ArgumentNullException(nameof(objectFile));

            // Create the collection to return.
            var sourceObjectFiles = new SourceObjectFiles();

            // Download and add to the collection

            // Where can we download it?
            var temporaryFilePath = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(), // The temporary file folder.
            System.IO.Path.GetTempFileName() + "." + objectFile.Extension); // The name including extension.

            // Download the file to a temporary location.
            vault.ObjectFileOperations.DownloadFile(objectFile.ID, objectFile.Version, temporaryFilePath);

            // Create an object source file for this temporary file
            // and add it to the collection.
            sourceObjectFiles.Add(-1, new SourceObjectFile()
            {
                Extension = objectFile.Extension,
                SourceFilePath = temporaryFilePath,
                Title = objectFile.Title
            });

            // Return the collection.
            return sourceObjectFiles;
        }

        /// <summary>
        /// Clears up any temporary files used with the creation of an object.
        /// </summary>
        /// <param name="sourceObjectFiles">The files to clear up.</param>
        private void ClearTemporaryFiles(SourceObjectFiles sourceObjectFiles)
        {
            // Sanity.
            if (null == sourceObjectFiles)
                return; // No point throwing; nothing to clear up.

            // Iterate over the files and clear them up.
            foreach (var sourceObjectFile in sourceObjectFiles.Cast<SourceObjectFile>())
            {
                try
                {
                    System.IO.File.Delete(sourceObjectFile.SourceFilePath);
                }
                catch (Exception e)
                {
                    // TODO: Swallowing exceptions isn't nice.
                }
            }
        }

        /// <summary>
        /// Copies property values from <see cref="cloneFrom"/>, removing items that exist in
        /// <see cref="PropertiesToRemove"/>
        /// </summary>
        /// <param name="cloneFrom">The collection of properties to clone.</param>
        /// <returns>The cloned set of properties, with the requested properties removed.</returns>
        private PropertyValues GetNewObjectPropertyValues(PropertyValues cloneFrom)
        {
            // Sanity.
            if (null == cloneFrom)
                throw new ArgumentNullException(nameof(cloneFrom));

            // Get a basic copy.
            var propertyValues = cloneFrom.Clone();

            // Remove the properties we don't want.
            foreach (var propertyId in this.PropertiesToRemove)
            {
                // If the property is not in the collection then skip.
                int index = propertyValues.IndexOf(propertyId);
                if (-1 == index)
                    continue;

                // Remove it.
                propertyValues.Remove(index);
            }

            // Return.
            return propertyValues;
        }

        #region Implementation of IUsesAdminConfigurations

        /// <inheritdoc />
        public void InitializeAdminConfigurations(IAdminConfigurations adminConfigurations)
        {
            this.Configuration = adminConfigurations
            .AddSimpleConfigurationNode<Configuration>("Split MFD Configuration");
        }

        #endregion

    }

    [DataContract]
    public class Configuration
    {
        [MFPropertyDef]
        [DataMember]
        [JsonConfEditor(DefaultValue = "Prop.Documentcollection")]
        public MFIdentifier DocumentCollectionReference = "Prop.Documentcollection";
        [MFPropertyDef]
        [DataMember]
        [JsonConfEditor(DefaultValue = "Prop.ComponentType")]
        public MFIdentifier ComponentTypeProperty = "Prop.ComponentType";
        [MFClass]
        [DataMember]
        [JsonConfEditor(DefaultValue = "Class.Proposal")]
        public MFIdentifier ProposalClass = "Class.Proposal";
        [MFClass]
        [DataMember]
        [JsonConfEditor(DefaultValue = "Class.ProposalCollection")]
        public MFIdentifier ProposalCollectionClass = "Class.ProposalCollection";
    }

}