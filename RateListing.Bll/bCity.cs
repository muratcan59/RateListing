using RateListing.Dal.Entity;
using RateListing.Model;
using System.Collections.Generic;
using System.Linq;

namespace RateListing.Bll
{
    public static class bCity
    {
        public static List<City> GetAll()
        {
            var list = new List<City>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Cities.OrderBy(x => x.Name).ToList().OrderBy(w => w.DisplayOrder).ToList();
            }
            return list;
        }

        public static City GetById(int id)
        {
            var data = new City();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Cities.Find(id);
            }
            return data;
        }


        public static List<District> GetDistricts(int id)
        {
            var list = new List<District>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Districts.Where(a => a.CityNum == id).OrderBy(w => w.Name).ToList();
            }
            return list;
        }
    }
}
