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
    public static class bComplaint
    {
        public static List<Complaint> GetAll(string userId)
        {
            var list = new List<Complaint>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Complaints.Where(x => x.IsDelete == false && x.IsComplaint == true && x.UserId == userId).ToList();
            }
            return list;
        }

        public static List<Complaint> GetAll()
        {
            var list = new List<Complaint>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Complaints.Where(x => x.IsDelete == false && x.IsComplaint == true).ToList();
            }
            return list;
        }

        public static List<Complaint> GetAllReference(string userId)
        {
            var list = new List<Complaint>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Complaints.Where(x => x.IsDelete == false && x.IsComplaint == false && x.UserId == userId).ToList();
            }
            return list;
        }

        public static Complaint GetById(int id)
        {
            var data = new Complaint();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Complaints.Find(id);
            }
            return data;
        }

        public static Complaint Add(Complaint model)
        {
            model.CreateDate = DateTime.Now;
            model.IsDelete = false;
            model.IsApprove = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
            }
            return model;
        }

        public static Complaint Update(Complaint model)
        {
            model.UpdateDate = DateTime.Now;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            return model;
        }

        public static void Approve(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Complaints.Find(id);
                data.IsApprove = true;
                db.SaveChanges();
            }
        }

        public static void ApproveRemove(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Complaints.Find(id);
                data.IsApprove = false;
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Complaints.Find(id);
                data.IsDelete = true;
                db.SaveChanges();
            }
        }
    }
}
