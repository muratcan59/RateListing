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
    public static class bOfferRequest
    {
        public static List<OfferRequest> GetAll(string userId)
        {
            var list = new List<OfferRequest>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.OfferRequests.Where(x => x.IsDelete == false && x.UserId == userId).ToList();
            }
            return list;
        }

        public static OfferRequest GetById(int id)
        {
            var data = new OfferRequest();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.OfferRequests.Find(id);
            }
            return data;
        }

        public static OfferRequest Add(OfferRequest model)
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

        public static OfferRequest Update(OfferRequest model)
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
                var data = db.OfferRequests.Find(id);
                data.IsDelete = true;
                db.SaveChanges();
            }
        }
    }
}
