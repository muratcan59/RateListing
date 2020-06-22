using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RateListing.Bll.Helpers
{
    public static class Tool
    {
        public static string toSearchString(this string text)
        {
            if (text != null)
            {
                return text.ToUpper().Replace("İ", "I").Replace("Ü", "U").Replace("Ğ", "G").Replace("Ş", "S").Replace("Ö", "O").Replace("Ç", "C").Replace(" ", "");
            }
            else
            {
                return "";
            }
        }



        public static string CreateUrlSlug(string text)
        {
            try
            {
                text = text.ToUpper().Replace("İ", "I").Replace("Ü", "U").Replace("Ğ", "G").Replace("Ş", "S").Replace("Ö", "O").Replace("Ç", "C");
                text = Translit(text);
                var str = RemoveAccent(text).ToLower();
                str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
                str = Regex.Replace(str, @"\s+", " ").Trim();
                str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
                str = Regex.Replace(str, @"\s", "-");
                return str;
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        private static string RemoveAccent(string txt)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }

        private static string Translit(string str)
        {
            string[] latUp = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya" };
            string[] latLow = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya" };
            string[] rusUp = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] rusLow = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (var i = 0; i <= 32; i++)
            {
                if (str.Contains("I"))
                {
                    str = str.Replace("I", "i");
                }
                else
                {
                    str = str.Replace(rusUp[i], latUp[i]);
                    str = str.Replace(rusLow[i], latLow[i]);
                }
            }
            return str;
        }

        public static bool IsValidEmail(this string emailaddress)
        {
            try
            {
                var m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidPhone(this string tel)
        {
            try
            {
                var a = Regex.Match(tel, @"^(\+[0-9]{10})$").Success;
                return a;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string AppSetting(string key)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[key];
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static List<SelectListItem> getStaticTimeList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{Text="Kapalı",Value="Kapalı" },
                new SelectListItem{Text="00:00",Value="0" },
                new SelectListItem{Text="01:00",Value="1" },
                new SelectListItem{Text="02:00",Value="2" },
                new SelectListItem{Text="03:00",Value="3" },
                new SelectListItem{Text="04:00",Value="4" },
                new SelectListItem{Text="05:00",Value="5" },
                new SelectListItem{Text="06:00",Value="6" },
                new SelectListItem{Text="07:00",Value="7" },
                new SelectListItem{Text="08:00",Value="8" },
                new SelectListItem{Text="09:00",Value="9" },
                new SelectListItem{Text="10:00",Value="10" },
                new SelectListItem{Text="11:00",Value="11" },
                new SelectListItem{Text="12:00",Value="12" },
                new SelectListItem{Text="13:00",Value="13" },
                new SelectListItem{Text="14:00",Value="14" },
                new SelectListItem{Text="15:00",Value="15" },
                new SelectListItem{Text="16:00",Value="16" },
                new SelectListItem{Text="17:00",Value="17" },
                new SelectListItem{Text="18:00",Value="18" },
                new SelectListItem{Text="19:00",Value="19" },
                new SelectListItem{Text="20:00",Value="20" },
                new SelectListItem{Text="21:00",Value="21" },
                new SelectListItem{Text="22:00",Value="22" },
                new SelectListItem{Text="23:00",Value="23" }
            };

            return list;
        }

        public static List<SelectListItem> getBooleanSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{Text="Evet",Value="true" },
                new SelectListItem{Text="Hayır",Value="false" },
            };

            return list;
        }

        public static Double Uzaklik_Hesapla(Double latitude1, Double longitude1, Double latitude2, Double longitude2)
        {

            Double teta_degeri = longitude1 - longitude2;
            Double mil = Math.Sin(deg2rad(latitude1)) * Math.Sin(deg2rad(latitude2)) +
            Math.Cos(deg2rad(latitude1)) * Math.Cos(deg2rad(latitude2)) * Math.Cos(deg2rad(teta_degeri));

            mil = Math.Acos(mil);
            mil = rad2deg(mil);
            mil = mil * 60 * 1.1515;

            Double kilometre = mil * 1.609344;

            return kilometre;

        }

        private static Double rad2deg(Double rad)
        {

            return (Double)(rad / Math.PI * 180.0);

        }

        private static Double deg2rad(Double deg)
        {

            return (Double)(deg * Math.PI / 180.0);

        }

        public static string stringToTimeFormat(string time)
        {
            if (time == "Kapalı")
            {
                return time;
            }
            else
            {
                return time.Length == 1 ? "0" + time + ":00" : time + ":00";
            }
        }
    }
}