using RateListing.Dal.Entity;
using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RateListing.Bll
{
    public static class bPhoto
    {
        public static List<Photo> GetAll(string userId)
        {
            var list = new List<Photo>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Photos.Where(x => x.IsDelete == false && x.UserId == userId).ToList();
            }
            return list;
        }

        public static Photo GetById(int id)
        {
            var data = new Photo();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Photos.Find(id);
            }
            return data;
        }

        public static Photo Add(Photo model)
        {

            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
            }
            return model;
        }

        public static Photo Update(Photo model)
        {
            model.UpdateDate = DateTime.Now;
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
                var data = db.Photos.Find(id);
                data.IsDelete = true;
                db.SaveChanges();
            }
        }
    }
}
