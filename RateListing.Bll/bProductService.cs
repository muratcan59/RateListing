using RateListing.Dal.Entity;
using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RateListing.Bll
{
    public static class bProductService
    {
        public static List<ProductService> GetAll(string userId)
        {
            var list = new List<ProductService>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.ProductServices.Where(x => x.IsDelete == false && x.UserId == userId).ToList();
            }
            return list;
        }

        public static ProductService GetById(int id)
        {
            var data = new ProductService();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.ProductServices.FirstOrDefault(x => x.Id == id);
            }
            return data;
        }

        public static ProductService Add(ProductService model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            CheckIt(model);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.ProductServices.Add(model);
                db.SaveChanges();
            }
            return model;
        }

        public static ProductService Update(ProductService model)
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
                var data = db.ProductServices.Find(id);
                data.IsDelete = true;
                data.DeleteDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static ProductService CheckIt(ProductService model)
        {
            try
            {
                if (model.Name == null)
                {
                    throw new Exception("Ad kısmı boş geçilemez");
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
