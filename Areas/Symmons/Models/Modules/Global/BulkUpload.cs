using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace symmons.com.Areas.Symmons.Models.Modules.Global
{
    public class BulkUpload
    {
        [Required(ErrorMessage = "Please Upload File")]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase file { get; set; }
    }
}