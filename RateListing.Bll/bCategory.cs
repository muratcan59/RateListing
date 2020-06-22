using RateListing.Bll.Helpers;
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
    public static class bCategory
    {
        public static List<Category> GetAll()
        {
            var list = new List<Category>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Categories.Where(x => x.IsDelete == false && x.Level == 1).ToList();
            }
            return list;
        }

        public static List<Category> GetSubCategories(int id)
        {
            var list = new List<Category>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Categories.Where(x => x.IsDelete == false && x.UpperCategoryId.Value == id).ToList();
            }
            return list;
        }

        public static Category GetById(int id)
        {
            var data = new Category();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Categories.FirstOrDefault(x => x.Id == id);
            }
            return data;
        }

        public static Category Add(Category model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            CheckIt(model);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Categories.Add(model);
                db.SaveChanges();
            }
            return model;
        }

        public static Category Update(Category model)
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
                var data = db.Categories.Find(id);
                data.IsDelete = true;
                data.DeleteDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static Category CheckIt(Category model)
        {
            try
            {
                if (model.Name == null)
                {
                    throw new Exception("Ad kısmı boş geçilemez");
                }
                if (model.Url == null)
                {
                    model.Url = Tool.CreateUrlSlug(model.Name);
                }
                if (model.Level == 0)
                {
                    model.Level = 1;
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
