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
    public static class bDistrict
    {
        public static List<District> GetAll()
        {
            var list = new List<District>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Districts.OrderBy(x => x.Name).ToList();
            }
            return list;
        }

        public static District GetById(int id)
        {
            var data = new District();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Districts.Find(id);
            }
            return data;
        }

     
    }
}
