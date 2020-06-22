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
    public static class bComment
    {
        public static List<Comment> GetAll(string userId)
        {
            var list = new List<Comment>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Comments.Where(x => x.IsDelete == false && x.UserId == userId).ToList();
            }
            return list;
        }

        public static List<Comment> GetApproveComments(string userId)
        {
            var list = new List<Comment>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Comments.Where(x => x.IsDelete == false && x.UserId == userId && x.IsApprove == true).ToList();
            }
            return list;
        }

        public static CommentAnswer AddComAnswer(CommentAnswer model)
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

        public static void DeleteComAnswer(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Comments.Find(id);
                data.IsDelete = true;
                db.SaveChanges();
            }
        }

        public static Comment GetById(int id)
        {
            var data = new Comment();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Comments.Find(id);
            }
            return data;
        }



        public static double ScoreAvg(string UserId)
        {
            double data = 0;
            var list = new List<Complaint>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Complaints.Where(x => x.IsDelete == false && x.UserId == UserId && x.IsApprove).ToList();
                if (list.Count != 0)
                {
                    data = list.Average(x => x.Score);
                }
            }
            return data;
        }

        public static Comment Add(Comment model)
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

        public static Comment Update(Comment model)
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
                var data = db.Comments.Find(id);
                data.IsApprove = true;
                db.SaveChanges();
            }
        }

        public static void ApproveRemove(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Comments.Find(id);
                data.IsApprove = false;
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Comments.Find(id);
                data.IsDelete = true;
                db.SaveChanges();
            }
        }
    }
}
