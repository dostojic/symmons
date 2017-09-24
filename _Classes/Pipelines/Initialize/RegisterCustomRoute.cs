using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace symmons.com._Classes.Pipelines.Initialize
{
    public class RegisterCustomRoute : Sitecore.Mvc.Pipelines.Loader.InitializeRoutes
    {
        public override void Process(PipelineArgs args)
        {
            Register();
        }
        public static void Register()
        {
            var routes = RouteTable.Routes;

            routes.MapRoute("productApi", "Products/GetAllProducts", new
            {
                controller = "Products",
                action = "GetAllProducts"
            });

            routes.MapRoute("productApiCsv", "Products/GetAllProductsInCsv", new
            {
                controller = "Products",
                action = "GetAllProductsInCsv"
            });

            routes.MapRoute("caseStudiesApi", "CaseStudy/GetCaseStudies", new
            {
                controller = "CaseStudy",
                action = "GetCaseStudies"
            });

            routes.MapRoute("storesJsonApi", "Stores/GetAllStoresJson", new
            {
                controller = "Stores",
                action = "GetAllStoresJson"
            });

            routes.MapRoute("storesCsvApi", "Stores/GetAllStoresCsv", new
            {
                controller = "Stores",
                action = "GetAllStoresCsv"
            });

            routes.MapRoute("productbyId", "Products/GetProductbyId/{id}", new
            {
                controller = "Products",
                action = "GetProductbyId",
                id = UrlParameter.Optional
            });

             routes.MapRoute("productssince", "Products/GetNewProductsSince/{id}", new
             {
                 controller = "Products",
                 action = "GetNewProductsSince",
                 id = UrlParameter.Optional
             });
        }

    }
}