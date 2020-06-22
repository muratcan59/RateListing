using RateListing.Bll;
using RateListing.Bll.Helpers;
using RateListing.Dal.Entity;
using RateListing.Ui.Models;
using RateListing.Ui.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RateListing.Ui.Controllers
{
    public class MainController : Controller
    {
        [Route("")]
        public ActionResult Index(string platform)
        {
            if (Cookie.Get("isMobileApp") == null)
            {
                if (platform == "mobileApp")
                {
                    Cookie.Create("isMobileApp", "true");
                }
                else
                {
                    Cookie.Create("isMobileApp", "false");
                }
            }

            return View();
        }

        [Route("rating")]
        public ActionResult Rating()
        {
            return View();
        }

        [Route("networking")]
        public ActionResult Networking()
        {
            return View();
        }

        [Route("cozumler")]
        public ActionResult Solutions()
        {
            return View();
        }

        [Route("yapay-zeka-cozumleri")]
        public ActionResult ArtIntelSolutions()
        {
            return View();
        }

        [Route("platform")]
        public ActionResult Platform()
        {
            return View();
        }

        [Route("sss")]
        public ActionResult FAQ()
        {
            return View();
        }

        [Route("nasil-kullanilir")]
        public ActionResult HowToUse()
        {
            return View();
        }

        [Route("kariyer")]
        public ActionResult Career()
        {
            return View();
        }

        [Route("network-agimiz")]
        public ActionResult Network()
        {
            return View();
        }

        [Route("kurumsal-rating")]
        public ActionResult CorporateRating()
        {
            return View();
        }

        [Route("musteri-rating")]
        public ActionResult CustomerRating()
        {
            return View();
        }

        [Route("referans-programi")]
        public ActionResult ReferenceProgram()
        {
            return View();
        }

        [Route("sikayet-cozumleri")]
        public ActionResult ComplaintSolution()
        {
            return View();
        }

        [Route("arabuluculuk-programi")]
        public ActionResult Mediation()
        {
            return View();
        }


        [Route("kvkk")]
        public ActionResult KVKK()
        {
            return View();
        }

        [Route("yardim")]
        public ActionResult Support()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Support(string name_contact, string lastname_contact, string email_contact, string phone_contact, string message_contact)
        {
            try
            {
                EmailService emailService = new EmailService();

                var Body = "Ad: " + name_contact +
                    "</br>Soyad: " + lastname_contact +
                    "</br>Email: " + email_contact +
                    "</br>Telefon: " + phone_contact +
                    "</br>Mesaj: " + message_contact;

                emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                {
                    Subject = "Yardım Formu Yeni Kayıt",
                    Body = Body,
                    Destination = "unal.cakir@setup34.com.tr"
                });

                return Json("Başarıyla Gönderildi", JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json("İsteğinizi şuan gerçekleştiremiyoruz. Lütfen daha sonra tekrar deneyin.", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Districts(int cityNum)
        {
            var ilceler = bCity.GetDistricts(cityNum);
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }

        [Route("changeLang")]
        public ActionResult ChangeLang(LangType lang, string returnUrl)
        {
            LangHelper.SetLang(lang);
            return Redirect(returnUrl);
        }
    }
}