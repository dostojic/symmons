using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FileHelpers;
using Sitecore.Data.Items;
using symmons.com._Classes.Shared.Global;
using Verndale.SharedSource.Helpers;
using WebGrease.Css.Extensions;

namespace symmons.com._Classes.Symmons.Custom.Migration
{
    public class WhereToBuyImport
    {
        public bool ImportWheretoBuyStores(string fileName, out int totalRecordCount, out int importedRecordCount, out string statusMsg)
        {
            importedRecordCount = 0;
            totalRecordCount = 0;
            var sbStatusMessage = new StringBuilder();
            try
            {
                var engine = new FileHelperEngine(typeof(StoreData));
                var storeDataArray = (StoreData[])engine.ReadFile(fileName);
                totalRecordCount = storeDataArray.Length - 1;
                var rowCount = 1;

                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    var master = Sitecore.Data.Database.GetDatabase("master");

                    var mainStoreItem = master.GetItem(Constants.FolderIds.StoresRepository);
                    var mainCanadaStoreItem = master.GetItem(Constants.PageIds.CanadaStoresRepository);

                    storeDataArray.Where(x => String.Equals(x.Country, Constants.ConstantValues.CountryCodeCanada,
                            StringComparison.CurrentCultureIgnoreCase)).ForEach(x => x.State = x.Province);

                    foreach (var storeData in storeDataArray.Where(x => x != storeDataArray.FirstOrDefault()))
                    {
                        rowCount++;

                        #region CheckCount

                        var validateMsg = ValidateStore(storeData, rowCount);

                        if (!string.IsNullOrEmpty(validateMsg))
                        {
                            sbStatusMessage.Append(validateMsg);
                            continue;
                        }


                        Item parentItem = null;

                        if (String.Equals(storeData.Country, Constants.ConstantValues.CountryCodeUSA,
                            StringComparison.CurrentCultureIgnoreCase))
                        {
                            parentItem = mainStoreItem;
                        }
                        else if (String.Equals(storeData.Country, Constants.ConstantValues.CountryCodeCanada,
                            StringComparison.CurrentCultureIgnoreCase))
                        {
                            parentItem = mainCanadaStoreItem;
                        }

                        if (parentItem == null)
                        {
                            sbStatusMessage.AppendLine("Line No: " + rowCount + "- Specify Valid Country.<br/>");
                            continue;
                        }

                        #region CreateState

                        Item stateItem;
                        if (
                            parentItem.GetChildren()
                                .ToList()
                                .Any(
                                    x =>
                                        String.Equals(x.Name, storeData.State,
                                            StringComparison.CurrentCultureIgnoreCase)))
                        {
                            stateItem =
                                parentItem.GetChildren()
                                    .ToList()
                                    .SingleOrDefault(
                                        x =>
                                            String.Equals(x.Name, storeData.State,
                                                StringComparison.CurrentCultureIgnoreCase));
                        }
                        else
                        {
                            stateItem = CreateItem(storeData.State, Constants.TemplateIds.State, parentItem);
                            try
                            {
                                stateItem.Editing.BeginEdit();

                                stateItem.Fields[Constants.FieldNames.StateAbbreviation].Value = storeData.State;
                                stateItem.Fields[Constants.FieldNames.StateName].Value = storeData.State;

                                stateItem.Editing.EndEdit();

                            }
                            catch (Exception ex)
                            {
                                sbStatusMessage.AppendLine("Line No: " + rowCount + "- Unable to Save State details.<br/>");
                                stateItem.Editing.CancelEdit();
                            }
                        }

                        #endregion

                        if (stateItem == null)
                        {
                            sbStatusMessage.AppendLine("Line No: " + rowCount + "- Specify Valid Country.<br/>");
                            continue;
                        }

                        #region CreateStoreFolder

                        Item storeFolder;

                        if (
                            stateItem.GetChildren()
                                .ToList()
                                .Any(
                                    x =>
                                        String.Equals(x.Name, storeData.StoreType,
                                            StringComparison.CurrentCultureIgnoreCase)))
                        {
                            storeFolder =
                                stateItem.GetChildren()
                                    .ToList()
                                    .SingleOrDefault(
                                        x =>
                                            String.Equals(x.Name, storeData.StoreType,
                                                StringComparison.CurrentCultureIgnoreCase));
                        }
                        else
                        {
                            if (String.Equals(storeData.StoreType, Constants.ConstantValues.RepresentativesFolderName,
                                StringComparison.CurrentCultureIgnoreCase))
                            {
                                storeFolder = CreateItem(storeData.StoreType, Constants.TemplateIds.CommonFolder, stateItem);
                            }
                            else
                            {
                                storeFolder = CreateItem(storeData.StoreType, Constants.TemplateIds.StoreFolder, stateItem);
                            }
                        }

                        #endregion

                        if (storeFolder == null)
                        {
                            sbStatusMessage.AppendLine("Line No: " + rowCount + "- Specify Valid Store Type.<br/>");
                            continue;
                        }

                        var storeTemplateId = GetStoreTemplateId(storeFolder.Name);
                        if (string.IsNullOrEmpty(storeTemplateId))
                        {
                            sbStatusMessage.AppendLine("Line No: " + rowCount + "- Specify Valid Store Type.<br/>");
                            continue;
                        }

                        #region CreateStore
                        var storeName = storeData.StoreName + (string.IsNullOrEmpty(storeData.City) ? "" : " " + storeData.City);

                        var store = CreateItem(storeName, storeTemplateId, storeFolder);

                        if (store != null)
                        {
                            try
                            {
                                store.Editing.BeginEdit();

                                store.Fields[Constants.FieldNames.StoreName].Value = storeData.StoreName;
                                store.Fields[Constants.FieldNames.StoreAddressLine1].Value = storeData.Address1;
                                store.Fields[Constants.FieldNames.StoreAddressLine2].Value = storeData.Address2;
                                store.Fields[Constants.FieldNames.StoreCity].Value = storeData.City;
                                store.Fields[Constants.FieldNames.StoreZip].Value = storeData.ZipCode;
                                store.Fields[Constants.FieldNames.StorePhone].Value = storeData.MainPhoneNumber;
                                store.Fields[Constants.FieldNames.StoreFax].Value = storeData.MainFaxNumber;
                                store.Fields[Constants.FieldNames.StoreEmail].Value = storeData.CompanyEMail;
                                store.Fields[Constants.FieldNames.StoreLatitude].Value = storeData.Latitude;
                                store.Fields[Constants.FieldNames.StoreLongitude].Value = storeData.Longitude;

                                if (storeData.Preferred == "Y")
                                {
                                    store.Fields[Constants.FieldNames.IsSymmonsPreferred].Value = "1";
                                }
                                store.Editing.EndEdit();
                                importedRecordCount++;

                            }
                            catch (Exception ex)
                            {
                                sbStatusMessage.AppendLine("Line No: " + rowCount +
                                                           "- Unable to Save Store details.<br/>");
                                store.Editing.CancelEdit();
                            }
                        }

                        #endregion

                        #endregion

                        if (importedRecordCount + 1 != rowCount)
                        {
                            var test = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Source.Contains("FileHelper"))
                {
                    sbStatusMessage.AppendLine(string.Format("Upload status: Error Uploading the file. Please check and upload valid file.<br/>"));
                }
                else
                {
                    sbStatusMessage.AppendLine(string.Format("Upload status: The file could not be uploaded. The following error occured: {0} <br />", ex.Message));
                }
                return false;
            }
            finally
            {
                statusMsg = sbStatusMessage.ToString();
                if ((File.Exists(fileName)))
                {
                    File.Delete(fileName);
                }
            }
            return true;
        }

        private string ValidateStore(StoreData storeData, int rowCount)
        {
            var lstStoreTypes = new List<string>
                {
                    Constants.ConstantValues.RetailStoreFolderName.ToUpper(),
                    Constants.ConstantValues.WholesaleStoreFolderName.ToUpper(),
                    Constants.ConstantValues.PartDistributorsFolderName.ToUpper(),
                    Constants.ConstantValues.ShowroomFolderName.ToUpper(),
                    Constants.ConstantValues.RepresentativesFolderName.ToUpper()
                };

            var sbStoreMsg = new StringBuilder();
            if (string.IsNullOrEmpty(storeData.Country))
            {
                sbStoreMsg.Append("Line No.: " + rowCount + " Please Specify Country.<br/>");
            }
            if (string.IsNullOrEmpty(storeData.State))
            {
                sbStoreMsg.Append("Line No.: " + rowCount + " Please Specify State.<br/>");
            }
            if (string.IsNullOrEmpty(storeData.StoreName))
            {
                sbStoreMsg.Append("Line No.: " + rowCount + " Please Specify Store Name.<br/>");
            }
            if (string.IsNullOrEmpty(storeData.StoreType))
            {
                sbStoreMsg.Append("Line No.: " + rowCount + " Please Specify Store Type.<br/>");
            }
            if (!lstStoreTypes.Contains(storeData.StoreType.ToUpper()))
            {
                sbStoreMsg.Append("Line No.: " + rowCount + " Please Specify Valid Store Type.<br/>");
            }
            return sbStoreMsg.ToString();
        }

        private static string GetStoreTemplateId(string storeFolderName)
        {
            var storeTemplateId = string.Empty;

            if (String.Equals(storeFolderName, Constants.ConstantValues.WholesaleStoreFolderName, StringComparison.CurrentCultureIgnoreCase))
            {
                storeTemplateId = Constants.TemplateIds.WholesaleStores;
            }
            else if (String.Equals(storeFolderName, Constants.ConstantValues.ShowroomFolderName, StringComparison.CurrentCultureIgnoreCase))
            {
                storeTemplateId = Constants.TemplateIds.ShowroomStores;
            }
            else if (String.Equals(storeFolderName, Constants.ConstantValues.RetailStoreFolderName, StringComparison.CurrentCultureIgnoreCase))
            {
                storeTemplateId = Constants.TemplateIds.RetailStores;
            }
            else if (String.Equals(storeFolderName, Constants.ConstantValues.PartDistributorsFolderName, StringComparison.CurrentCultureIgnoreCase))
            {
                storeTemplateId = Constants.TemplateIds.PartsStores;
            }
            else if (String.Equals(storeFolderName, Constants.ConstantValues.RepresentativesFolderName, StringComparison.CurrentCultureIgnoreCase))
            {
                storeTemplateId = Constants.TemplateIds.SalesRepresntative;
            }
            return storeTemplateId;
        }

        private Item CreateItem(string itemName, string templateId, Item parentItem)
        {
            try
            {
                var stateTemplate = SitecoreHelper.ItemMethods.GetItemFromGUID(templateId);
                if (stateTemplate != null)
                {
                    itemName = Regex.Replace(itemName, @"[^0-9a-zA-Z]+", " ");
                    return parentItem.Add(itemName.Trim(), (TemplateItem)stateTemplate);
                }
            }
            catch (Exception)
            {
                Console.Write("Item Creation Failed");
            }
            return null;
        }
    }


    public class StoreData1
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string StoreType { get; set; }
        public string StoreName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Manager { get; set; }
        public string Location { get; set; }
        public string LongitudeLatitude { get; set; }
    }

    [DelimitedRecord("`")]
    public class StoreData
    {
        public string CompanyID { get; set; }
        public string ExternalUniqueID { get; set; }
        public string Blank { get; set; }
        public string StoreName { get; set; }
        public string Preferred { get; set; }
        public string ExhibitAPartsProgram { get; set; }
        public string RecordOwner1 { get; set; }
        public string ShowroomName { get; set; }
        public string MainPhoneNumber { get; set; }
        public string MainFaxNumber { get; set; }
        public string TerritoryName { get; set; }
        public string AccountNumber { get; set; }
        public string RecordOwner2 { get; set; }
        public string CommercialMultiplier { get; set; }
        public string PremiumMultiplier { get; set; }
        public string CatalogueCustomer { get; set; }
        public string VIPMultiplier { get; set; }
        public string StoreType { get; set; }
        public string TMVStockingDistributor { get; set; }
        public string CompanyEMail { get; set; }
        public string DealerType { get; set; }
        public string ShowroomStatus { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }



}