using RateListing.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateListing.Ui.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var users = bUser.GetAll();
            var sikayetler = bComplaint.GetAll();
            var kategoriler = bCategory.GetAll();
            var blogs = bBlog.GetAll();

            ViewBag.UserCount = users.Count;
            ViewBag.SikayetCount = sikayetler.Count;
            ViewBag.KategoriCount = kategoriler.Count;
            ViewBag.BlogCount = blogs.Count;
            return View();
        }

        public ActionResult Complaints()
        {
            var list = bComplaint.GetAll();
            return View(list);
        }

        public ActionResult Complaint(int complaintId)
        {
            var model = bComplaint.GetById(complaintId);
            return View(model);
        }

        public ActionResult ComplaintUpdate(int complaintId, bool? isSolve, bool? isApprove)
        {
            var model = bComplaint.GetById(complaintId);
            if (isSolve != null)
            {
                model.IsSolve = isSolve.Value;
            }
            if (isApprove != null)
            {
                model.IsApprove = isApprove.Value;
            }
            bComplaint.Update(model);
            return View("Complaint", model);
        }

        public JsonResult DeleteComplaint(int id)
        {
            bComplaint.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Districts(int cityNum)
        {
            var ilceler = bCity.GetDistricts(cityNum);
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }

    }
}