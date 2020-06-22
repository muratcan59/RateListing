using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateListing.Ui.Models.Helper
{
    public static class LangHelper
    {
        static LangHelper()
        {
            // Cookie.Create("googtrans", "/tr/tr");
            Cookie.Create("lang", "tr");
        }

        public static LangType GetLang()
        {
            var lang = LangType.tr;
            if (Cookie.Get("lang") == null)
            {
                lang = LangType.tr;
                Cookie.Delete("googtrans");
            }
            else if (Cookie.Get("lang") == "en")
            {
                lang = LangType.en;
                Cookie.Create("googtrans", "/tr/en");
            }
            return lang;
        }

        public static void SetLang(LangType lang)
        {
            switch (lang)
            {
                case LangType.tr:
                    Cookie.Delete("googtrans");
                    Cookie.Create("lang", "tr");
                    break;
                case LangType.en:
                    Cookie.Create("googtrans", "/tr/en");
                    Cookie.Create("lang", "en");
                    break;
            }
        }

        public static string GetLangString(this LangType lang)
        {
            switch (lang)
            {
                case LangType.tr: return "tr";
                case LangType.en: return "en";
                default: return " Unknown";
            }
        }

        public static string GetLangDisplayString(this LangType lang)
        {
            switch (lang)
            {
                case LangType.tr: return "Türkçe";
                case LangType.en: return "English";
                default: return " Unknown";
            }
        }
    }

    public enum LangType
    {
        tr,
        en
    }
}