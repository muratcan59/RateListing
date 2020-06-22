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
    public static class bTime
    {
        public static List<Time> GetAll(string userId)
        {
            var list = new List<Time>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Times.Where(x => x.IsDelete == false && x.UserId == userId).ToList();
            }
            return list;
        }

        //public static Time GetByUserId(string userId)
        //{
        //    var data = new Time();
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        data = db.Times.Find(userId);
        //    }
        //    return data;
        //}

        public static Time Add(Time model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            //CheckIt(model);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Times.Add(model);
                db.SaveChanges();
            }
            return model;
        }

        public static void Update(List<Time> model, string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var times = db.Times.Where(w => w.UserId == userId);
                db.Times.RemoveRange(times);
                db.Times.AddRange(model);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Times.Find(id);
                data.IsDelete = true;
                data.DeleteDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        //public static Time CheckIt(Time model)
        //{
        //    try
        //    {
        //        if (model.Name == null)
        //        {
        //            throw new Exception("Ad kısmı boş geçilemez");
        //        }
        //        return model;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
