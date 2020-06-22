using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateListing.Ui.Models.Helper
{
    public static class Cookie
    {
        public static void Create(string name, string value)
        {

            //var encodeValue = HttpContext.Current.Server.UrlEncode(value);
            HttpCookie cookieVisitor = new HttpCookie(name, value);
            cookieVisitor.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookieVisitor);
        }
        public static string Get(string name)
        {
            //Böyle bir cookie mevcut mu kontrol ediyoruz
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(name))
            {
                //böyle bir cookie varsa bize geri değeri döndürsün
                var encodeValue = HttpContext.Current.Request.Cookies[name].Value;
                var decodeValue = HttpContext.Current.Server.UrlDecode(encodeValue);
                return decodeValue;
            }
            return null;
        }
        public static void Delete(string name)
        {
            //Böyle bir cookie var mı kontrol ediyoruz
            if (Get(name) != null)
            {
                //Varsa cookiemizi temizliyoruz
                HttpContext.Current.Response.Cookies.Remove(name);
                //ya da 
                HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}