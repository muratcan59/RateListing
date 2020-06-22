using RateListing.Bll;
using RateListing.Model;
using RateListing.Ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RateListing.Ui.Controllers
{
    public class CompaniesController : Controller
    {
        [Route("firmalar")]
        public ActionResult List(string name, int? city, int? country, FilterModel filterModel)
        {
            var list = bUser.GetComCateCitDis(name, city, country);

            if (Request.HttpMethod == "GET")
            {
                filterModel = new FilterModel
                {
                    KurumsalRating = 1,
                    Mesafe = 0,
                    MusteriRatingi = 1,
                    Categories = bCategory.GetAll().Select(w => new CategoryFilter
                    {
                        Value = false,
                        Id = w.Id,
                        Name = w.Name
                    }).ToList()
                };
            }
            else
            {
                var selectedCategories = filterModel.Categories.Where(w => w.Value).Select(w => w.Id).ToList();
                var selectedStart = new List<int>();
                var selectedRating = new List<int>();

                if (selectedCategories.Any())
                {
                    list = list.Where(w => selectedCategories.Any(a => a == w.CategoryId)).ToList();
                }

                if (filterModel.yildiz5) selectedStart.Add(5);
                if (filterModel.yildiz4) selectedStart.Add(4);
                if (filterModel.yildiz3) selectedStart.Add(3);
                if (filterModel.yildiz2) selectedStart.Add(2);
                if (filterModel.yildiz1) selectedStart.AddRange(new List<int> { 1, 0 });
                if (selectedStart.Any())
                {
                    list = list.Where(w => selectedStart.Any(a => a == int.Parse(Math.Floor(bComment.ScoreAvg(w.Id)).ToString()))).ToList();
                }

                if (filterModel.mukemmel) selectedRating.AddRange(new List<int> { 9, 10 });
                if (filterModel.cokiyi) selectedRating.AddRange(new List<int> { 8, 9, 10 });
                if (filterModel.iyi) selectedRating.AddRange(new List<int> { 7, 8, 9, 10 });
                if (filterModel.orta) selectedRating.AddRange(new List<int> { 6, 7, 8, 9, 10 });
                if (selectedRating.Any())
                {
                    list = list.Where(w => selectedRating.Any(a => a == int.Parse(bUser.GetCorporateScore(w.Id).ToString()))).ToList();
                }

                if (filterModel.Mesafe != 0)
                {
                    var cLatitude = filterModel.currentLocation.latitude;
                    var cLongitude = filterModel.currentLocation.longitude;

                    list = list.Where(w => RateListing.Bll.Helpers.Tool.Uzaklik_Hesapla(Double.Parse(cLatitude.Replace(".", ",")), Double.Parse(cLongitude.Replace(".", ",")), Double.Parse(w.Latitude.Replace(".", ",")), Double.Parse(w.Longitude.Replace(".", ","))) <= filterModel.Mesafe).ToList();
                }
            }

            ViewBag.FilterModel = filterModel;
            ViewBag.SearchName = name;
            ViewBag.SearchCity = city;
            ViewBag.SearchCountry = country;
            return View(list);
        }

        [Route("firma/{firmaUrl}")]
        public ActionResult Details(string firmaUrl)
        {
            var detay = bUser.GetUrl(firmaUrl);
            if (detay.isInformationConfirmed && detay.isPaymentConfirmed && !detay.IsDelete)
            {
                return View(detay);
            }
            else
            {
                return Redirect("/");
            }

        }

        [HttpPost]
        public JsonResult YorumYap(Comment model)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                bComment.Add(model);
                retVal.isSuccess = true;
                retVal.message = "Yorumunuz başarılı bir şekilde gönderildi.";
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retVal.isSuccess = false;
                retVal.message = ex.Message;
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SikayetEt(Complaint model)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                model.IsComplaint = true;
                bComplaint.Add(model);
                retVal.isSuccess = true;
                retVal.message = "Şikayetiniz başarılı bir şekilde iletildi.";
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retVal.isSuccess = false;
                retVal.message = ex.Message;
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ReferansVer(Complaint model)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                model.IsComplaint = false;
                bComplaint.Add(model);
                retVal.isSuccess = true;
                retVal.message = "Referans başarılı bir şekilde verildi.";
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retVal.isSuccess = false;
                retVal.message = ex.Message;
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult OneriTeklif(OfferRequest model)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var user = bUser.GetById(model.UserId);

                if (!string.IsNullOrEmpty(user.Email))
                {
                    EmailService emailService = new EmailService();
                    var body = "Ad Soyad: " + model.NameSurname
                        + "<br />Email: " + model.Email
                        + "<br />Telefon: " + model.Phone
                        + "<br />Mesaj: " + model.Description;

                    emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                    {
                        Body = body,
                        Subject = "Teklif Formu Yeni Kayıt",
                        Destination = user.Email
                    });
                }
                bOfferRequest.Add(model);
                retVal.isSuccess = true;
                retVal.message = "Başarıyla Gönderildi.";
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retVal.isSuccess = false;
                retVal.message = ex.Message;
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Districts(int cityNum)
        {
            var ilceler = bCity.GetDistricts(cityNum);
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }
    }
}