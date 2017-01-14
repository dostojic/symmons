using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using symmons.com._Classes.Shared.Global;
using Verndale.SharedSource.Helpers;

namespace symmons.com._Classes.Symmons.Custom.Migration
{
    public static class ProductMigrationHelper
    {
        // ************************************************************************************************************************
        const string ProductBranchItemGuid = "{DE00C99C-BFE6-4D6E-B36A-9F1A731CAA2C}";


        // ********************** MigrateProducts : To Migrate produts from old system to new system... ***************************

        public static void TestFeatures()
        {
            var dalProductMigration = new ProductMigrationData();
            var legacyDatabase = Factory.GetDatabase("LegacyProductsProd");
            if (legacyDatabase != null)
            {
                var legacyBathroomProductsLocationItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.LegacyBathroomProductLocation,
                        legacyDatabase);


                // Get the list of all "Bathroom product Items" from old system...
                var bathroomProductItems =
                    legacyBathroomProductsLocationItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == "{2C832C8F-5C83-4562-B0A1-2147D5C9EC63}")
                        .ToList();

                var bathroomProductItems123 =
                    bathroomProductItems
                        .Where(x => x.Fields["SKU"].Value == "").ToList();



                  var legacyKitchenProductsLocationItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID("{EC0E90B9-ABD0-46B9-A492-1D137E24822D}", legacyDatabase);

                var kitchenProductItems =
                    legacyKitchenProductsLocationItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == "{D9BE9283-8628-4E74-93EF-5AAA42DA3FAC}")
                        .ToList();
                var kitchenProductItems123 =
                    kitchenProductItems
                        .Where(x => x.Fields["SKU"].Value == "").ToList();



                  var legacyCommercialProductsLocationItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID("{315B67C1-CF52-47E7-A03F-F620E13B1EC7}", legacyDatabase);

                var commercialProductItems =
                    legacyCommercialProductsLocationItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == "{E34E184F-2654-43DD-A9A2-47C160EDCF54}")
                        .ToList();
                 var commercialProductItems123 =
                    commercialProductItems
                        .Where(x => x.Fields["SKU"].Value == "").ToList();

                var oldProductItem =
                    bathroomProductItems.FirstOrDefault(x => x.ID.ToString() == "{020D4A91-4A22-4E64-AD0B-0820B59BCD31}");

                var masterDB = Factory.GetDatabase("master");
                var existingProductItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID("{5C7BAFCB-5F70-4F55-A4C6-C3A066E27A4E}",
                        masterDB);

                var productRepositoryItem = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.FolderIds.ProductsRepository);
                var productRepositoryItems =
                    productRepositoryItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == Constants.TemplateIds.ProductDetailsTemplateId)
                        .ToList();
                var tempItem =
                    productRepositoryItems.Where(
                        x =>
                            x.Fields[Constants.FieldNames.ProductModelNumber].Value ==
                            oldProductItem.Fields["SKU"].Value).ToList().FirstOrDefault();

                //var existingProductItem = bathroomProductItems.FirstOrDefault(x => x.Fields[Constants.FieldNames.ProductModelNumber].Value == oldProductItem.Fields["SKU"].Value);

                var newProductItem = MigrateProduct(oldProductItem, false, true, existingProductItem);
                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "1",
                                "Product Updated");

                string abc = "Success";
            }
        }

        public static bool MigrateProducts()
        {
            var symmonsMasterDB = Factory.GetDatabase("master");
            var productRepositoryItem = SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.FolderIds.ProductsRepository, symmonsMasterDB);
            var productRepositoryItems =
                productRepositoryItem.Axes.GetDescendants()
                    .Where(x => x.TemplateID.ToString() == Constants.TemplateIds.ProductDetailsTemplateId)
                    .ToList();
            
            var dalProductMigration = new ProductMigrationData();
            var legacyDatabase = Factory.GetDatabase("LegacyProductsProd");
            if (legacyDatabase != null)
            {
                var legacyBathroomProductsLocationItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID(Constants.PageIds.LegacyBathroomProductLocation,
                        legacyDatabase);

                #region Migrate Symmons Bathroom Products

                // Get the list of all "Bathroom product Items" from old system...
                var bathroomProductItems =
                    legacyBathroomProductsLocationItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == "{2C832C8F-5C83-4562-B0A1-2147D5C9EC63}")
                        .ToList();

                // ************************************************************************************************************************

                // Product Types :
                // 1 : Bathroom Product
                // 2 : Kitchen Product
                // 3 : Commercial Product
                // 4 : Approved Product

                // ************************************************************************************************************************

                if (bathroomProductItems.Any())
                {
                    var filteredBathroomProductItems = bathroomProductItems.Where(x => x.DisplayName != "*").ToList();
                    foreach (var oldProductItem in filteredBathroomProductItems)
                    {
                        try
                        {
                            Item newProductItem = null;
                            var existingProductItem = productRepositoryItems.FirstOrDefault(x => x.Fields[Constants.FieldNames.ProductModelNumber].Value == oldProductItem.Fields["SKU"].Value);
                            if (existingProductItem != null)
                            {
                                newProductItem = MigrateProduct(oldProductItem, false, true, existingProductItem);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "1",
                                "Product/Finish Updated");
                            }
                            else
                            {
                                newProductItem = MigrateProduct(oldProductItem, false,false);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "1",
                                "Product/Finish Migrated");
                            }
                        }
                        catch (Exception ex)
                        {
                            dalProductMigration.LogUpdate(oldProductItem.ID.ToString(), oldProductItem.DisplayName,
                                oldProductItem.Paths.Path, "NO", ex.Message);
                        }
                    }
                }

                #endregion

                #region Migrate Symmons Kitchen Products

                var legacyKitchenProductsLocationItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID("{EC0E90B9-ABD0-46B9-A492-1D137E24822D}", legacyDatabase);

                var kitchenProductItems =
                    legacyKitchenProductsLocationItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == "{D9BE9283-8628-4E74-93EF-5AAA42DA3FAC}")
                        .ToList();
                if (kitchenProductItems.Any())
                {
                    var filteredKitchenProductItems = kitchenProductItems.Where(x => x.DisplayName != "*").ToList();
                    foreach (var oldProductItem in filteredKitchenProductItems)
                    {
                        try
                        {
                            Item newProductItem = null;
                            var existingProductItem = productRepositoryItems.FirstOrDefault(x => x.Fields[Constants.FieldNames.ProductModelNumber].Value == oldProductItem.Fields["SKU"].Value);
                            if (existingProductItem != null)
                            {
                                newProductItem = MigrateProduct(oldProductItem, false, true, existingProductItem);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "2",
                                "Product/Finish Updated");
                            }
                            else
                            {
                                newProductItem = MigrateProduct(oldProductItem, false, false);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "2",
                                "Product/Finish Migrated");
                            }
                        }
                        catch (Exception ex)
                        {
                            dalProductMigration.LogUpdate(oldProductItem.ID.ToString(), oldProductItem.DisplayName,
                                oldProductItem.Paths.Path, "NO", ex.Message);
                        }
                    }
                }

                #endregion

                #region Migrate Symmons Commericial Products

                var legacyCommercialProductsLocationItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID("{315B67C1-CF52-47E7-A03F-F620E13B1EC7}", legacyDatabase);

                var commercialProductItems =
                    legacyCommercialProductsLocationItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == "{E34E184F-2654-43DD-A9A2-47C160EDCF54}")
                        .ToList();

                if (commercialProductItems.Any())
                {
                    var filteredCommercialProductItems = commercialProductItems.Where(x => x.DisplayName != "*").ToList();
                    foreach (var oldProductItem in filteredCommercialProductItems)
                    {
                        try
                        {
                            Item newProductItem = null;
                            var existingProductItem = productRepositoryItems.FirstOrDefault(x => x.Fields[Constants.FieldNames.ProductModelNumber].Value == oldProductItem.Fields["SKU"].Value);
                            if (existingProductItem != null)
                            {
                                newProductItem = MigrateProduct(oldProductItem, false, true, existingProductItem);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "3",
                                "Product/Finish Updated");
                            }
                            else
                            {
                                newProductItem = MigrateProduct(oldProductItem, false, false);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "3",
                                "Product/Finish Migrated");
                            }
                        }
                        catch (Exception ex)
                        {
                            dalProductMigration.LogUpdate(oldProductItem.ID.ToString(), oldProductItem.DisplayName,
                                oldProductItem.Paths.Path, "NO", ex.Message);
                        }
                    }
                }

                #endregion

                #region Migrate Symmons Approved Products

                var legacyApprovedProductsLocationItem =
                    SitecoreHelper.ItemMethods.GetItemFromGUID("{B9FBD5D3-158B-43CA-A0B2-37920B63BF59}", legacyDatabase);

                var approvedlProductItems =
                    legacyApprovedProductsLocationItem.Axes.GetDescendants()
                        .Where(x => x.TemplateID.ToString() == "{2C832C8F-5C83-4562-B0A1-2147D5C9EC63}")
                        .ToList();
                if (approvedlProductItems.Any())
                {
                    var filteredApprovedProductItems = approvedlProductItems.Where(x => x.DisplayName != "*").ToList();
                    foreach (var oldProductItem in filteredApprovedProductItems)
                    {
                        try
                        {
                            Item newProductItem = null;
                            var existingProductItem = productRepositoryItems.FirstOrDefault(x => x.Fields[Constants.FieldNames.ProductModelNumber].Value == oldProductItem.Fields["SKU"].Value);
                            if (existingProductItem != null)
                            {
                                newProductItem = MigrateProduct(oldProductItem, false, true, existingProductItem);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "4",
                                "Product/Finish Updated");
                            }
                            else
                            {
                                newProductItem = MigrateProduct(oldProductItem, false, false);
                                dalProductMigration.LogProductForRollBack(newProductItem.ID.ToString(),
                                oldProductItem.DisplayName, oldProductItem.Paths.Path, newProductItem.Paths.Path, "4",
                                "Product/Finish Migrated");
                            }
                        }
                        catch (Exception ex)
                        {
                            dalProductMigration.LogUpdate(oldProductItem.ID.ToString(), oldProductItem.DisplayName,
                                oldProductItem.Paths.Path, "NO", ex.Message);
                        }
                    }
                }
                return true;

                #endregion
            }
            else
            {
                dalProductMigration.LogUpdate("Source DB Not found", "Source DB Not found", "Source DB Not found", "NO", "Source DB Not found");
                return false;
            }
        }

        // ******************************************************************************************************************************

        private static Item MigrateProduct(Item oldProductItem, bool isCommercial, bool isUpdate, Item existingProductItem=null)
        {
            Item newProductItem = null;
            var dalProductMigration = new ProductMigrationData();
            if (isUpdate)
            {
                // Update existing product...
                newProductItem = existingProductItem;
            }
            else
            {
                // Create new product
                newProductItem = AddUpdateHelper.CreateSitecoreItemUsingBranchTemplate("/sitecore/content/Global/Product Items", ProductBranchItemGuid,
                            oldProductItem.DisplayName);
            }

            if (newProductItem != null)
            {
                // get fields

                var productValues = new Dictionary<string, string>();
                var message = String.Empty;

                #region SKU Update

                var oldSku = oldProductItem.Fields["SKU"].Value;
                if (!String.IsNullOrEmpty(oldSku))
                {
                    productValues.Add(Constants.FieldNames.ProductModelNumber, oldSku);
                }

                #endregion

                #region US Price Update

                var oldUsPrice = oldProductItem.Fields[Constants.FieldNames.LegacyProductUsPrice].Value;
                if (!String.IsNullOrEmpty(oldUsPrice))
                {
                    productValues.Add(Constants.FieldNames.PriceMin, oldUsPrice);
                }

                #endregion


                #region CAN Price Update

                var oldCanPrice = oldProductItem.Fields["CAN price"].Value;
                if (!String.IsNullOrEmpty(oldCanPrice))
                {
                    productValues.Add(Constants.FieldNames.CanPrice, oldCanPrice);
                }

                #endregion

                #region Content Update

                var content = oldProductItem.Fields["Content"].Value;
                if (!String.IsNullOrEmpty(content))
                {
                    productValues.Add("Feature Description", content);
                }

                #endregion

                #region Legacy Product Style

                var oldStyle = oldProductItem.Fields[Constants.FieldNames.LegacyProductStyle].Value;
                if (!String.IsNullOrEmpty(oldStyle))
                {
                    switch (oldStyle)
                    {
                        case "{DA7118EB-98AB-40E7-85BC-C67BABEF467F}":// for transitional
                            productValues.Add(Constants.FieldNames.ProductStyle, "{35993E0C-A267-44CF-A8F6-16AE500E9037}");
                            break;
                        case "{D929FBAD-4AB8-44BB-A3F1-409C57752D21}": // for contemporary
                            productValues.Add(Constants.FieldNames.ProductStyle, "{5E1D18CA-1BFA-4882-9E68-3990C6238A71}");
                            break;
                        case "{A1916F13-868C-4590-A672-C5719713200C}": // for traditional
                            productValues.Add(Constants.FieldNames.ProductStyle, "{985EC1C7-DB56-40CF-A7DC-1C4059086CB3}");
                            break;
                    }
                }

                #endregion

                #region Title Update

                var oldTitle = oldProductItem.Fields["Title"].Value;
                if (!String.IsNullOrEmpty(oldTitle))
                {
                    productValues.Add(Constants.FieldNames.ProductName, oldTitle);
                    productValues.Add(Constants.FieldNames.PageTitle, oldTitle);
                }

                #endregion

                #region Features Update

                var featuresGuid = String.Empty;

                if (oldProductItem.Fields["Water Sense"].Value == "1")
                {
                    featuresGuid = "{7A355888-C4CE-4EE1-BF37-885BC6EF9932}";
                }
                if (oldProductItem.Fields["Water Efficient"].Value == "1")
                {
                    featuresGuid = !String.IsNullOrEmpty(featuresGuid) ? featuresGuid+ "|{AB5ADA38-6812-4AD5-870C-584B12B2A1C0}" : "{AB5ADA38-6812-4AD5-870C-584B12B2A1C0}";
                }
                if (oldProductItem.Fields["ADA Compliant"].Value == "1")
                {
                    featuresGuid = !String.IsNullOrEmpty(featuresGuid) ? featuresGuid+"|{064A70EF-D717-4588-94AD-ABBD3C5C5203}" : "{064A70EF-D717-4588-94AD-ABBD3C5C5203}";
                }
                if (oldProductItem.Fields["Lead Free"].Value == "1")
                {
                    featuresGuid = !String.IsNullOrEmpty(featuresGuid) ? featuresGuid+"|{750FA906-B365-4A5B-944D-9CD49CCF082B}" : "{750FA906-B365-4A5B-944D-9CD49CCF082B}";
                }
                if (oldProductItem.Fields["Buy American"].Value == "1")
                {
                    featuresGuid = !String.IsNullOrEmpty(featuresGuid) ? featuresGuid+"|{7E4F2ACF-290E-48D0-A9C1-BDD76178C565}" : "{7E4F2ACF-290E-48D0-A9C1-BDD76178C565}";
                }

                if (!String.IsNullOrEmpty(featuresGuid))
                {
                    productValues.Add(Constants.FieldNames.ProductAttributes, featuresGuid);
                }

                #endregion


                #region Is Commercial Product
                //Show in Commercial Product


                // Add all commercial segments to this product, which includes : "Hospitality","Healthcare","Housing" and "Facilities"...
                // {8199BFFF-158F-451D-9C48-AC402D5F3A61} : facilities
                // healthcare : {0FF57890-83E2-4E0D-828C-B50369C3FE31}
                // hospitality : {36A0FE67-DDDC-4E2C-8D1E-7AA11805C498}
                // housing : {8A2A73DD-CDD1-45C8-B6C1-58D1CE19E366}
                if (isCommercial)
                {
                    const string segmentGuids = "{8199BFFF-158F-451D-9C48-AC402D5F3A61}|{0FF57890-83E2-4E0D-828C-B50369C3FE31}|{36A0FE67-DDDC-4E2C-8D1E-7AA11805C498}|{8A2A73DD-CDD1-45C8-B6C1-58D1CE19E366}";
                    productValues.Add(Constants.FieldNames.ProductSegment, segmentGuids);
                }

                #endregion

                //#region Adding Product Finishes

                // Delete existing finish item, and then migrate the new finishes...
                var defaultFinishItem =
                    newProductItem.GetChildren()
                        .Where(x => x.Template.ID.ToString() == "{D387AA7C-28BB-4BBA-A5A2-66E9E4279425}")
                        .ToList()
                        .FirstOrDefault();
                if (defaultFinishItem != null)
                {
                    defaultFinishItem.Delete();
                }

                // {03019ED8-8CC5-4FC8-997F-31E5464218B6} : Old Product Finish template Id...
                var oldProductFinishItems =
                    oldProductItem.GetChildren()
                        .Where(x => x.Template.ID.ToString() == "{03019ED8-8CC5-4FC8-997F-31E5464218B6}")
                        .ToList();

                var allFinishes = String.Empty;
                if (oldProductFinishItems.Any())
                {
                    foreach (var productFinish in oldProductFinishItems)
                    {
                        Item newProductFinishItem = null;
                        var tempFinishGuid = String.Empty;
                        var tempProductFinishItem =
                            newProductItem.GetChildren()
                                .Where(x => x.DisplayName == productFinish.DisplayName)
                                .ToList()
                                .FirstOrDefault();

                        if (tempProductFinishItem != null)
                        {
                            newProductFinishItem = tempProductFinishItem;
                        }
                        else
                        {
                            // {D387AA7C-28BB-4BBA-A5A2-66E9E4279425} : Template Id of new finish template...
                            newProductFinishItem =
                                AddUpdateHelper.CreateSitecoreItemUsingTemplate(newProductItem.Paths.Path,
                                    "{D387AA7C-28BB-4BBA-A5A2-66E9E4279425}",
                                    productFinish.DisplayName);
                        }

                        var productFinishValues = new Dictionary<string, string>();

                        //Brushed Bronze finish
                        if (productFinish.Fields["Finish"].Value == "{D09D6FD8-E9E4-4E04-9D9C-671F2F507B6F}")
                        {
                            productFinishValues.Add("Finish", "{E01DA140-6315-41CF-BFB4-D68C85DBE3E0}");
                            tempFinishGuid = "{E01DA140-6315-41CF-BFB4-D68C85DBE3E0}";
                        }
                        //Chrome finish
                        else if (productFinish.Fields["Finish"].Value == "{2B6E99C3-4140-46A7-8512-DB36B754E8EB}")
                        {
                            productFinishValues.Add("Finish", "{1249EE33-EFD8-4845-81D3-502DB7875394}");
                            tempFinishGuid = "{1249EE33-EFD8-4845-81D3-502DB7875394}";
                        }
                        //Oil Rubbed Bronze finish
                        else if (productFinish.Fields["Finish"].Value == "{A8426495-7284-40F7-96D0-35EE2ABF81A1}")
                        {
                            productFinishValues.Add("Finish", "{38CE056A-5CF9-4C08-A631-9280A28258FE}");
                            tempFinishGuid = "{38CE056A-5CF9-4C08-A631-9280A28258FE}";
                        }
                        //Polished Graphite finish
                        else if (productFinish.Fields["Finish"].Value == "{8C072106-7EFC-4191-9224-329676159B8B}")
                        {
                            productFinishValues.Add("Finish", "{AC52D0BF-A5D8-4AF1-9EED-86736D3A5C15}");
                            tempFinishGuid = "{AC52D0BF-A5D8-4AF1-9EED-86736D3A5C15}";
                        }
                        //Polished Nickel finish
                        else if (productFinish.Fields["Finish"].Value == "{6ABC0954-0070-4BA0-966C-748DC026B4BD}")
                        {
                            productFinishValues.Add("Finish", "{19BC264E-9541-410A-AC40-EF16EEE2BA1A}");
                            tempFinishGuid = "{19BC264E-9541-410A-AC40-EF16EEE2BA1A}";
                        }
                        //Satin Nickel finish
                        else if (productFinish.Fields["Finish"].Value == "{CB8F718E-B56F-4CA4-AF4B-AF671F4FD9C8}")
                        {
                            productFinishValues.Add("Finish", "{1A81E30E-59F0-416A-826C-352B49441D1C}");
                            tempFinishGuid = "{1A81E30E-59F0-416A-826C-352B49441D1C}";
                        }
                        //Seasoned Bronze finish
                        else if (productFinish.Fields["Finish"].Value == "{0D05D80A-DEBC-4EA9-88D2-46BE79B2D368}")
                        {
                            productFinishValues.Add("Finish", "{F43002E6-7B0A-450C-A854-21BE7B3CEA43}");
                            tempFinishGuid = "{F43002E6-7B0A-450C-A854-21BE7B3CEA43}";
                        }
                        //Stainless Steel finish
                        else if (productFinish.Fields["Finish"].Value == "{614DA0F0-E05E-4956-9AF0-E77A5E2BBCCF}")
                        {
                            productFinishValues.Add("Finish", "{4874F694-D305-4ABE-B5C8-89EF58FB0A51}");
                            tempFinishGuid = "{4874F694-D305-4ABE-B5C8-89EF58FB0A51}";
                        }


                        if (String.IsNullOrEmpty(allFinishes))
                        {
                            allFinishes = tempFinishGuid;
                        }
                        else
                        {
                            allFinishes += "|" + tempFinishGuid;
                        }


                        var oldFinishCanPrice = productFinish.Fields["CAN Price"].Value;
                        if (!String.IsNullOrEmpty(oldFinishCanPrice))
                        {
                            productFinishValues.Add("Finish CAN Price", oldFinishCanPrice);
                        }

                        var oldFinishUsPrice = productFinish.Fields["US Price"].Value;
                        if (!String.IsNullOrEmpty(oldFinishUsPrice))
                        {
                            productFinishValues.Add("Finish Price", oldFinishUsPrice);
                        }

                        var oldFinishSKU = productFinish.Fields["SKU"].Value;
                        if (!String.IsNullOrEmpty(oldFinishSKU))
                        {
                            productFinishValues.Add("Finish SKU", oldFinishSKU);
                        }


                        

                        AddUpdateHelper.UpdateSitecoreItem(newProductFinishItem, productFinishValues, out message);
                        dalProductMigration.LogProductForRollBack(newProductFinishItem.ID.ToString(),
                            newProductFinishItem.DisplayName, productFinish.Paths.Path, newProductFinishItem.Paths.Path,
                            "10",
                            "Product Finish Added");

                    }
                }

                //#endregion


                #region Product Finishes Multilist in Product detail

                if (!String.IsNullOrEmpty(allFinishes))
                {
                    productValues.Add(Constants.FieldNames.ProductFinishes, allFinishes);
                }
                
                #endregion

                #region Update Sitecore Item

                AddUpdateHelper.UpdateSitecoreItem(newProductItem, productValues, out message);

                const int milliseconds = 2600;
                Thread.Sleep(milliseconds);

                #endregion
            }
            return newProductItem;
        }

        // This is to Rollback product data which is migrated to new system...
        // ************************************************************************************************************************

        public static void MigrationRollBack()
        {
            // ToDo : Product rollback...
        }

        // ************************************************************************************************************************

        // This is to show the status of migration...
        public static DataTable ShowMigrationStatusSuccess()
        {
            var dalProductMigration = new ProductMigrationData();
            return dalProductMigration.GetProducts();
        }

        // ************************************************************************************************************************

        // This is to show the status of migration...
        public static DataTable ShowMigrationStatusFail()
        {
            var dalProductMigration = new ProductMigrationData();
            return dalProductMigration.GetProductsFailed();
        }

        // ************************************************************************************************************************
        // ************************************************************************************************************************
    }
}