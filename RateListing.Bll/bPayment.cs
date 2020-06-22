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
    public class bPayment
    {
        public static List<Payment> GetAll()
        {
            var list = new List<Payment>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Payments.Where(x => x.IsDelete == false).ToList();
            }
            return list;
        }

        public static List<Payment> GetAll(string userId)
        {
            var list = new List<Payment>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Payments.Where(x => x.IsDelete == false && x.userId == userId).ToList();
            }
            return list;
        }

        public static Payment GetById(int id)
        {
            var data = new Payment();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Payments.Find(id);
            }
            return data;
        }

        public static Payment GetByToken(string token)
        {
            var data = new Payment();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Payments.FirstOrDefault(w => w.iyzipay_token == token);
            }
            return data;
        }


        public static Payment Add(Payment model)
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

        public static Payment Update(Payment model)
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
                var data = db.Payments.Find(id);
                data.IsDelete = true;
                db.SaveChanges();
            }
        }
    }
}
