using System.Web.Mvc;

namespace symmons.com.Areas.Symmons
{
    public class SymmonsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Symmons";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
           
        }
    }
}