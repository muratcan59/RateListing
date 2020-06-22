using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace RateListing.Ui.App_Start
{
    public class DisallowListConstraint : IRouteConstraint
    {
        private readonly string[] validOptions;
        public DisallowListConstraint(string options)
        {
            options = "rating|networking|sss|yapay-zeka-cozumleri|cozumler|platform|haber-etkinlik|haber-detay|kayit-ol|giris-yap|firmalar|firmadetay|admin|company|cikis-yap|nasil-kullanilir|kariyer|kurumsal-rating|musteri-rating|referans-programi|arabuluculuk-programi";
            options += "";
            validOptions = options.Split('|');
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return !validOptions.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}