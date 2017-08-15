using symmons.com._Classes.Custom.API;
using symmons.com._Classes.Symmons.Helpers;
using symmons.com.Areas.Symmons.Controllers.Global;
using System.Net;
using System.Web.Mvc;

namespace symmons.com.Areas.Symmons.Controllers.Modules.Api
{
    public class CaseStudyController : SymmonsController
    {
        // GET: /casestudy/getcasestudies
        [HttpGet]
        public JsonResult GetCaseStudies()
        {
            var caseStudies = CaseStudyHelper.GetCaseStudies();

            if (caseStudies.Count == 0)
            {
                var noData = new ApiResult
                {
                    statusCode = HttpStatusCode.OK,
                    description = "No Products",
                    result = new[] { new object() }
                };

                return Json(noData, JsonRequestBehavior.AllowGet);
            }

            var data = new ApiResult
            {
                statusCode = HttpStatusCode.OK,
                result = caseStudies
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}