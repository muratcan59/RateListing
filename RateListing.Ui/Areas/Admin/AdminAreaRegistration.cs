using System.Web.Mvc;

namespace RateListing.Ui.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index",controller = "Home",AreaName = "Admin", id = UrlParameter.Optional },
                namespaces: new string[] { "RateListing.Ui.Areas.Admin.Controllers" }
            );
        }
    }
}