using RateListing.Bll;
using RateListing.Bll.Helpers;
using RateListing.Model;
using RateListing.Ui.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateListing.Ui.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        public ActionResult List()
        {
            var list = bBlog.GetAll();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Add(Blog model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                if (model.Url == null)
                {
                    model.Url = Tool.CreateUrlSlug(model.Name);
                }
                model.Picture = foto.FileName;

                bBlog.Add(model);

                if (foto != null && foto.ContentLength > 0)
                {
                    string dir = Server.MapPath("~/Uploads/Blog/" + model.Id);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = Path.Combine(dir, foto.FileName);
                    foto.SaveAs(path);
                }
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
            var data = bBlog.GetById(id);
            return View(data);
        }


        [HttpPost]
        public JsonResult Update(Blog model, HttpPostedFileBase foto)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                if (model.Url == null)
                {
                    model.Url = Tool.CreateUrlSlug(model.Name);
                }

                if (foto != null && foto.ContentLength > 0)
                {
                    string dir = Server.MapPath("~/Uploads/Blog/" + model.Id);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var path = Path.Combine(dir, foto.FileName);
                    foto.SaveAs(path);
                    model.Picture = foto.FileName;
                }
                bBlog.Update(model);
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
            bBlog.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
