using RateListing.Dal.Entity;
using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateListing.Bll
{
    public static class bBlog
    {
        public static List<Blog> GetAll()
        {
            var list = new List<Blog>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Blogs.Where(x => x.IsDelete == false).ToList();
            }
            return list;
        }
        public static List<Blog> GetLast(int count)
        {
            var list = new List<Blog>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Blogs.Where(x => x.IsDelete == false).OrderByDescending(w => w.CreateDate).Take(count).ToList();
            }
            return list;
        }

        public static Blog GetByUrl(string blogUrl)
        {
            var data = new Blog();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Blogs.FirstOrDefault(x => x.Url == blogUrl);
            }
            return data;
        }

        public static Blog GetById(int id)
        {
            var data = new Blog();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Blogs.FirstOrDefault(x => x.Id == id);
            }
            return data;
        }

        public static Blog Add(Blog model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            CheckIt(model);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Blogs.Add(model);
                db.SaveChanges();
            }
            return model;
        }

        public static Blog Update(Blog model)
        {
            model.UpdateDate = DateTime.Now;
            CheckIt(model);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return model;
        }

        public static void Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Blogs.Find(id);
                data.IsDelete = true;
                data.DeleteDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static Blog CheckIt(Blog model)
        {
            try
            {
                if (model.Name == null)
                {
                    throw new Exception("Ad kısmı boş geçilemez");
                }
                else if (model.Description == null)
                {
                    throw new Exception("Açıklama kısmı boş geçilemez");
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
