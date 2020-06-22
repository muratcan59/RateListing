using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RateListing.Model.Enums;

namespace RateListing.Bll.Helpers
{
    public static class EnumHelper
    {
        public static string GetCompanyTypeName(CompanyType type)
        {
            switch (type)
            {
                case CompanyType.Sahis:
                    return "Şahıs Şirketi";
                case CompanyType.Limited:
                    return "Limited Şirketi";
                case CompanyType.Anonim:
                    return "Anonim Şirketi";
                default:
                    return "";
            }
        }
    }
}
