using RateListing.Bll;
using RateListing.Model;
using RateListing.Ui.Models;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace RateListing.Ui.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        public ActionResult List()
        {
            var list = bUser.GetAll();
            return View(list);
        }

        public ActionResult Update(string id)
        {
            ViewBag.Kategoriler = bCategory.GetAll();
            ViewBag.Sehirler = bCity.GetAll();
            ViewBag.Ilceler = bDistrict.GetAll();
            var data = bUser.GetById(id);
            return View(data);
        }

        [HttpPost]
        public JsonResult Update(User model)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var user = bUser.GetById(model.Id);
                user.TaxPlace = model.TaxPlace;
                user.TaxNo = model.TaxNo;
                user.FoundationYear = model.FoundationYear;
                user.CreditScore = model.CreditScore;
                user.RegisteredRoom = model.RegisteredRoom;
                user.RegisteredFederation = model.RegisteredFederation;
                user.NumOfEmployees = model.NumOfEmployees;
                user.CategoryId = model.CategoryId;
                user.LicenceDocument = model.LicenceDocument;
                user.LocationCount = model.LocationCount;
                user.CompanyType = model.CompanyType;
                user.ServiceDefinition = model.ServiceDefinition;
                user.BonusFeature = model.BonusFeature;
                user.isInformationConfirmed = model.isInformationConfirmed;
                user.isPaymentConfirmed = model.isPaymentConfirmed;
                if (string.IsNullOrEmpty(model.Url))
                {
                    user.Url = RateListing.Bll.Helpers.Tool.CreateUrlSlug(user.Name);
                }
                else
                {
                    user.Url = RateListing.Bll.Helpers.Tool.CreateUrlSlug(model.Url);
                }

                bUser.Update(user);
                retVal.isSuccess = true;
                retVal.message = "Güncelleme başarılı.";
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retVal.isSuccess = false;
                retVal.message = ex.Message;
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Delete(string id)
        {
            bUser.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}