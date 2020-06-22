using System.Web.Mvc;

namespace RateListing.Ui.Areas.Company
{
    public class CompanyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Company";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Company_default",
                "Company/{controller}/{action}/{id}",
                new { action = "Index", controller = "Dashboard", AreaName = "Company", id = UrlParameter.Optional },
                namespaces: new string[] { "RateListing.Ui.Areas.Company.Controllers" }
            );
        }
    }
}