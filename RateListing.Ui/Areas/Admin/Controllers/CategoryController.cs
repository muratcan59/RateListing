using RateListing.Bll;
using RateListing.Bll.Helpers;
using RateListing.Model;
using RateListing.Ui.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace RateListing.Ui.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        public ActionResult List()
        {
            var list = bCategory.GetAll();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Add(Category model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                bCategory.Add(model);

                if (foto != null)
                {
                    string dir = Server.MapPath("~/Uploads/Category/" + model.Id);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = Path.Combine(dir, foto.FileName);
                    foto.SaveAs(path);
                    model.Picture = foto.FileName;
                }
                bCategory.Update(model);
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

        [HttpGet]
        public ActionResult Update(int id)
        {
            var data = bCategory.GetById(id);
            return View(data);
        }


        [HttpPost]
        public JsonResult Update(Category model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                var category = bCategory.GetById(model.Id);
                if (foto != null)
                {
                    string dir = Server.MapPath("~/Uploads/Category/" + model.Id);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = Path.Combine(dir, foto.FileName);
                    foto.SaveAs(path);
                    category.Picture = foto.FileName;
                }
                if (string.IsNullOrEmpty(model.Url))
                {
                    category.Url = Tool.CreateUrlSlug(model.Name);
                }
                else
                {
                    category.Url = model.Url;
                }
                category.Name = model.Name;
                bCategory.Update(category);
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

        public JsonResult Delete(int id)
        {
            bCategory.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}