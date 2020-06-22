using GoogleMaps.LocationServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RateListing.Bll;
using RateListing.Model;
using RateListing.Ui.Areas.Company.Model;
using RateListing.Ui.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace RateListing.Ui.Areas.Company.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var user = bUser.GetByName(User.Identity.Name);
            var teklif = bOfferRequest.GetAll(user.Id);
            var referas = bComplaint.GetAllReference(user.Id);
            var sikayet = bComplaint.GetAll(user.Id);

            ViewBag.TeklifCount = teklif.Count;
            ViewBag.ReferansCount = referas.Count;
            ViewBag.SikayetCount = sikayet.Count;

            return View(user);
        }

        public ActionResult ProcessTracking()
        {
            var user = bUser.GetByName(User.Identity.Name);
            return View(user);
        }

        public ActionResult UserProfile()
        {
            string userId = User.Identity.GetUserId();
            var user = bUser.GetById(userId);
            var times = bTime.GetAll(userId);
            var model = new UserProfileModel
            {
                user = user,
                times = times,
                paymentMethods = user.PaymentMethods != null ? user.PaymentMethods.Split(',') : new string[0]
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UserProfile(UserProfileModel model)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var user = bUser.GetById(model.user.Id);
                user.Name = model.user.Name;
                user.AuthorizedPerson = model.user.AuthorizedPerson;
                user.Description = model.user.Description;
                user.UserName = model.user.UserName;
                user.PhoneNumber = model.user.PhoneNumber;
                user.AlternativePhone = model.user.AlternativePhone;
                user.CityNum = model.user.CityNum;
                user.DistrictNum = model.user.DistrictNum;
                user.Address = model.user.Address;
                user.Location = model.user.Location;
                user.WebUrl = model.user.WebUrl;
                user.FacebookUrl = model.user.FacebookUrl;
                user.TwitterUrl = model.user.TwitterUrl;
                user.InstagramUrl = model.user.InstagramUrl;
                user.IsSale = model.user.IsSale;
                user.SalientFeature1 = model.user.SalientFeature1;
                user.SalientFeature2 = model.user.SalientFeature2;
                user.SalientFeature3 = model.user.SalientFeature3;
                user.PaymentMethods = model.paymentMethods != null ? string.Join(",", model.paymentMethods) : "";
                user.Slogan = model.user.Slogan;

                var locationService = new GoogleLocationService("AIzaSyA2a_AnWVfbtqTaNC6Jze_rv3-iB-r6c8E");
                var point = locationService.GetLatLongFromAddress(user.Address);
                user.Latitude = point.Latitude.ToString().Replace(",", ".");
                user.Longitude = point.Longitude.ToString().Replace(",", ".");

                bUser.Update(user);
                bTime.Update(model.times, user.Id);
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

        public ActionResult CompanyProfile()
        {
            string name = User.Identity.Name;
            var data = bUser.GetByName(name);
            return View(data);
        }

        [HttpPost]
        public JsonResult CompanyProfile(User model)
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
                user.Category2Id = model.Category2Id;
                user.Category3Id = model.Category3Id;
                user.LicenceDocument = model.LicenceDocument;
                user.ServiceDefinition = model.ServiceDefinition;
                user.LocationCount = model.LocationCount;
                user.CompanyType = model.CompanyType;

                user.isInformationConfirmed = false;
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

        public ActionResult CommentList()
        {
            var name = bUser.GetByName(User.Identity.Name);
            var list = bComment.GetAll(name.Id);
            return View(list);
        }
        public JsonResult ApproveComment(int id)
        {
            bComment.Approve(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnswerComment(CommentAnswer model)
        {
            var comModel = new CommentAnswer { Answer = model.Answer, UserId = model.UserId, CommentId = model.CommentId };
            bComment.AddComAnswer(comModel);
            return RedirectToAction(nameof(CommentList));
        }
        public JsonResult ApproveReference(int id)
        {
            bComplaint.Approve(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductServiceList()
        {
            var name = bUser.GetByName(User.Identity.Name);
            var list = bProductService.GetAll(name.Id);
            return View(list);
        }

        public ActionResult AddProductService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProductService(ProductService model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var userId = bUser.GetByName(User.Identity.Name);
                model.UserId = userId.Id;
                model.Picture = foto.FileName;
                if (foto != null && foto.ContentLength > 0)
                {
                    string dir = Server.MapPath("~/Uploads/User/" + userId.Id + "/ProductService/");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    //foto.SaveAs(dir);
                    var path = Path.Combine(dir, foto.FileName);
                    foto.SaveAs(path);

                }
                bProductService.Add(model);
                retVal.isSuccess = true;
                retVal.message = "Ekleme başarılı.";
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retVal.isSuccess = false;
                retVal.message = ex.Message;
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateProductService(int id)
        {
            var data = bProductService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public JsonResult UpdateProductService(ProductService model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var user = bUser.GetByName(User.Identity.Name);
                var productService = bProductService.GetById(model.Id);

                if (foto != null)
                {
                    string dir = Server.MapPath("~/Uploads/User/" + user.Id + "/ProductService/");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    //foto.SaveAs(dir);
                    var path = Path.Combine(dir, foto.FileName);
                    foto.SaveAs(path);
                    productService.Picture = foto.FileName;
                }

                productService.Name = model.Name;
                productService.Description = model.Description;
                bProductService.Update(productService);
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
        public JsonResult DeleteProductService(int id)
        {
            bProductService.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RequestList()
        {
            var user = bUser.GetByName(User.Identity.Name);
            var list = bOfferRequest.GetAll(user.Id);
            return View(list);
        }

        public ActionResult RequestDetail(int requestId)
        {
            var user = bUser.GetByName(User.Identity.Name);
            var request = bOfferRequest.GetById(requestId);

            if (request.UserId == user.Id)
            {
                return View(request);
            }
            else
            {
                return RedirectToAction("RequestList");
            }
        }


        public ActionResult ComplaintList()
        {
            var name = bUser.GetByName(User.Identity.Name);
            var list = bComplaint.GetAll(name.Id);
            return View(list);
        }

        public ActionResult ComplaintDetail(int complaintId)
        {
            var user = bUser.GetByName(User.Identity.Name);
            var complaint = bComplaint.GetById(complaintId);

            if (complaint.UserId == user.Id)
            {
                return View(complaint);
            }
            else
            {
                return RedirectToAction("ComplaintList");
            }
        }

        public ActionResult ReferenceList()
        {
            var name = bUser.GetByName(User.Identity.Name);
            var list = bComplaint.GetAllReference(name.Id);
            return View(list);
        }

        public ActionResult ReferenceDetail(int referenceId)
        {
            var user = bUser.GetByName(User.Identity.Name);
            var reference = bComplaint.GetById(referenceId);

            if (reference.UserId == user.Id)
            {
                return View(reference);
            }
            else
            {
                return RedirectToAction("ReferenceList");
            }
        }

        public ActionResult ReferenceUpdate(int referenceId, bool isApprove)
        {
            var user = bUser.GetByName(User.Identity.Name);
            var reference = bComplaint.GetById(referenceId);
            if (reference.UserId == user.Id)
            {
                reference.IsApprove = isApprove;
                bComplaint.Update(reference);
                return View("ReferenceDetail", reference);
            }
            else
            {
                return RedirectToAction("ReferenceList");
            }
        }

        public ActionResult AddPhoto()
        {
            return View();
        }

        public JsonResult Districts(int cityNum)
        {
            var ilceler = bCity.GetDistricts(cityNum);
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddPhoto(Photo model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var userId = bUser.GetByName(User.Identity.Name);
                model.UserId = userId.Id;
                model.Picture = foto.FileName;
                if (foto != null && foto.ContentLength > 0)
                {
                    string dir = Server.MapPath("~/Uploads/User/" + userId.Id + "/Gallery");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    //foto.SaveAs(dir);
                    var path = Path.Combine(dir, foto.FileName);
                    foto.SaveAs(path);

                }
                bPhoto.Add(model);
                retVal.isSuccess = true;
                retVal.message = "Ekleme başarılı.";
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retVal.isSuccess = false;
                retVal.message = ex.Message;
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PhotoList()
        {
            var userId = bUser.GetByName(User.Identity.Name);
            var list = bPhoto.GetAll(userId.Id);
            return View(list);
        }

        public ActionResult PhotoUpdate(int id)
        {
            var data = bPhoto.GetById(id);
            return View(data);
        }

        [HttpPost]
        public JsonResult PhotoUpdate(Photo model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var user = bUser.GetByName(User.Identity.Name);
                var photo = bPhoto.GetById(model.Id);

                if (foto != null && foto.ContentLength > 0)
                {
                    string dir = @"~/Uploads/User/" + user.Id + "/Gallery";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = Path.Combine(Server.MapPath(dir), foto.FileName);
                    foto.SaveAs(path);
                    photo.Picture = foto.FileName;

                }

                photo.OrderNo = model.OrderNo;
                bPhoto.Update(photo);
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
        public JsonResult RemoveAppComment(int id)
        {
            bComment.ApproveRemove(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveAppReference(int id)
        {
            bComplaint.ApproveRemove(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PhotoDelete(int id)
        {
            bPhoto.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLatLongFromAddress(string address)
        {
            ReturnValue retVal = new ReturnValue();

            try
            {
                var locationService = new GoogleLocationService("AIzaSyA2a_AnWVfbtqTaNC6Jze_rv3-iB-r6c8E");
                var point = locationService.GetLatLongFromAddress(address);
                var model = new LatLong
                {
                    latitude = point.Latitude,
                    longitude = point.Longitude
                };
                retVal.data = model;
                retVal.isSuccess = true;
                retVal.message = "Koordinatlar adres bilgisine göre güncellendi.";
            }
            catch (Exception Ex)
            {
                retVal.isSuccess = false;
                retVal.message = "Kordinatlar bulunamadı.";
            }

            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubCategories(int id)
        {
            var categories = bCategory.GetSubCategories(id);
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}