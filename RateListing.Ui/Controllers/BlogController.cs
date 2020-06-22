using RateListing.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateListing.Ui.Controllers
{
    public class BlogController : Controller
    {
        [Route("haberler-ve-etkinlikler")]
        public ActionResult List()
        {
            var list = bBlog.GetAll();
            return View(list);
        }

        [Route("haberler-ve-etkinlikler/{blogUrl}")]
        public ActionResult Detail(string blogUrl)
        {
            var data = bBlog.GetByUrl(blogUrl);
            return View(data);
        }
    }
}