using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Globalization;
using symmons.com.Areas.Symmons.Controllers.Global;
using symmons.com.Areas.Symmons.Models.Pages.Products;
using symmons.com._Classes.Symmons.Helpers;
using Verndale.SharedSource.Helpers;
using Constants = symmons.com._Classes.Shared.Global.Constants;

namespace symmons.com.Areas.Symmons.Controllers.Pages.Products
{
    public class SuperCategoryController : SymmonsController
    {
        // *****************************************************************************************************************
        
        public ActionResult GetSuperCategoryDetails()
        {
            var superCategoryModel = SitecoreCurrentContext.GetCurrentItem<SuperCategory>();

            if (superCategoryModel == null)
                return null;
            var familyId = string.Empty;

            // System will search the products based on segment (this can be "Commercial" or "Home")...
            var segmentId = superCategoryModel.ProductSegment == null ? string.Empty : superCategoryModel.ProductSegment.Id.ToString("B");
            
            if (superCategoryModel.ProductSection.ToUpper().Equals(Translate.Text(Constants.Dictionary.Home)))
            {
                // Check if "Product family" is configured or not...
                if (superCategoryModel.ProductFamily != null)
                {
                    familyId = superCategoryModel.ProductFamily.Id.ToString("B");
                }
            }
            superCategoryModel.NewProducts = GetNewProducts(segmentId, familyId);
            return View(Constants.ViewPaths.SuperCategoryDetails, superCategoryModel);
        }

        // *****************************************************************************************************************

        public static List<ProductDetails> GetNewProducts(string prodSegment, string prodFamily)
        {
            var refinements = new List<Refinement>
            {
                new Refinement
                {
                    FieldName = Constants.FieldNames.ProductFamily,
                    IsOr = true,
                    IsFacetRefinement = true,
                    Value = prodFamily
                },
                new Refinement
                {
                    FieldName = Constants.FieldNames.ProductSegment,
                    IsOr = true,
                    IsFacetRefinement = true,
                    Value = prodSegment
                },
                new Refinement
                {
                    FieldName = Constants.FieldNames.IsNew,
                    IsOr = true,
                    Value = "1"
                }
            };

            var productItems = ProductsHelper.GetProductItems(Constants.SearchIndex.Products, Sitecore.Context.Language.ToString(), Constants.TemplateIds.ProductDetailsTemplateId,
                Constants.FolderIds.ProductsRepository, String.Empty, refinements);
            return productItems != null ? productItems.ToList() : null;
        }

        // *****************************************************************************************************************

        // Redirects to "Design Studio" page...
        public ActionResult GetDesignStudio()
        {
            return View(Constants.ViewPaths.DesignStudio);
        }

        public ActionResult GetDesignStudioCarousel()
        {
            return View(Constants.ViewPaths.DesignStudioCarousel);
        }

        public ActionResult GetDesignStudioProcesses()
        {
            return View(Constants.ViewPaths.DesignStudioProcesses);
        }

        // *****************************************************************************************************************
        // *****************************************************************************************************************
    }
}