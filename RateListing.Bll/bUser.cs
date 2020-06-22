using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RateListing.Bll.Helpers;
using RateListing.Dal.Entity;
using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RateListing.Bll
{
    public static class bUser
    {
        public static List<User> GetAll()
        {
            var list = new List<User>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Users.Where(x => x.IsDelete == false).ToList();
            }
            return list;
        }

        public static User GetById(string id)
        {
            var data = new User();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Users.Find(id);
            }
            return data;
        }

        public static double GetCorporateScore(string id)
        {
            var data = new User();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Users.Find(id);
            }
            double score = 1;
            if (!string.IsNullOrEmpty(data.AuthorizedPerson))
            {
                score += 1;
            }
            if (!string.IsNullOrEmpty(data.Address))
            {
                score += 1;
            }
            if (!string.IsNullOrEmpty(data.PhoneNumber))
            {
                score += 1;
            }
            if (!string.IsNullOrEmpty(data.WebUrl) && !string.IsNullOrEmpty(data.Email))
            {
                score += 1;
            }
            if (!string.IsNullOrEmpty(data.FacebookUrl) && !string.IsNullOrEmpty(data.TwitterUrl) && !string.IsNullOrEmpty(data.InstagramUrl))
            {
                score += 1;
            }
            if (data.TaxNo != 0)
            {
                score += 1;
            }
            if (DateTime.Now.Year - data.FoundationYear >= 3)
            {
                score += 1;
            }
            if (data.CompanyType != Enums.CompanyType.Sahis)
            {
                score += 1;
            }
            if (!string.IsNullOrEmpty(data.LicenceDocument))
            {
                score += 1;
            }
            if (!string.IsNullOrEmpty(data.RegisteredRoom) && !string.IsNullOrEmpty(data.RegisteredFederation))
            {
                score += 1;
            }
            if (data.CreditScore >= 1250)
            {
                score += 1;
            }

            if (score >= 10)
            {
                return 10;
            }
            else
            {
                return score;
            }
        }

        public static bool isOpenNow(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var times = db.Times.Where(w => w.UserId == id).ToList();

                if (!times.Any())
                {
                    return false;
                }

                var pazartesi = times.FirstOrDefault(w => w.Day == Enums.Gunler.Pazartesi);
                var salı = times.FirstOrDefault(w => w.Day == Enums.Gunler.Salı);
                var çarşamba = times.FirstOrDefault(w => w.Day == Enums.Gunler.Çarşamba);
                var perşembe = times.FirstOrDefault(w => w.Day == Enums.Gunler.Perşembe);
                var cuma = times.FirstOrDefault(w => w.Day == Enums.Gunler.Cuma);
                var cumartesi = times.FirstOrDefault(w => w.Day == Enums.Gunler.Cumartesi);
                var pazar = times.FirstOrDefault(w => w.Day == Enums.Gunler.Pazar);

                var selectTime = new Time();

                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Sunday: selectTime = pazar; break;
                    case DayOfWeek.Monday: selectTime = pazartesi; break;
                    case DayOfWeek.Tuesday: selectTime = salı; break;
                    case DayOfWeek.Wednesday: selectTime = çarşamba; break;
                    case DayOfWeek.Thursday: selectTime = perşembe; break;
                    case DayOfWeek.Friday: selectTime = cuma; break;
                    case DayOfWeek.Saturday: selectTime = cumartesi; break;
                    default: return false;
                }

                if (string.IsNullOrEmpty(selectTime.OpenTime) || string.IsNullOrEmpty(selectTime.CloseTime))
                {
                    return false;
                }
                else if (selectTime.OpenTime == "Kapalı" || selectTime.CloseTime == "Kapalı")
                {
                    return false;
                }
                else
                {
                    var hourNow = DateTime.Now.Hour;
                    var openHourToday = int.Parse(selectTime.OpenTime.Split(':')[0].ToString());
                    var closeHourToday = int.Parse(selectTime.CloseTime.Split(':')[0].ToString());

                    if (hourNow > openHourToday && hourNow < closeHourToday)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static User GetUrl(string companyUrl)
        {
            var data = new User();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Users.FirstOrDefault(x => x.Url == companyUrl && x.IsDelete == false);
            }
            return data;
        }

        public static User GetByName(string name)
        {
            var data = new User();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Users.FirstOrDefault(x => x.UserName == name);
            }
            return data;
        }


        public static List<User> GetComCate(string Name)
        {
            var data = new List<User>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                data = db.Users.Where(x => x.Categories.Name == Name && x.IsDelete == false).ToList();
                if (data.Count == 0)
                {
                    data = db.Users.Where(x => x.Name == Name && x.IsDelete == false).ToList();
                }
            }
            return data;
        }

        public static List<User> GetComCateCit(string catName, int cityNum)
        {
            var list = new List<User>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Users.Where(x => x.Categories.Name == catName && x.IsDelete == false && x.Cities.CityNum == cityNum).ToList();
            }
            return list;
        }

        public static List<User> GetComCateCitDis(string Name, int? cityNum, int? districtNum)
        {
            var list = new List<User>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                list = db.Users.Where(x => (x.Categories.Name.Contains(Name) || x.Name.Contains(Name)) && x.IsDelete == false && x.isInformationConfirmed && x.isPaymentConfirmed).ToList();
                if (cityNum != null)
                {
                    list = list.Where(w => w.CityNum == cityNum).ToList();
                    if (districtNum != null)
                    {
                        list = list.Where(w => w.DistrictNum == districtNum).ToList();
                    }
                }
            }
            return list;
        }

        public static User CreateUser(string UserName, string Email, string Password, string Address,
            string AuthorizedPerson, string Position, string WebUrl, string Url, int CityNum, int DistrictNum,
            string Description, int CategoryId, int NumOfEmployees, string PhoneNumber,
            string Role = null)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                if (!context.Users.Any(u => u.UserName == UserName))
                {
                    var store = new UserStore<User>(context);
                    var manager = new UserManager<User>(store);
                    var user = new User { UserName = UserName, Email = Email, CreateDate = DateTime.Now, IsDelete = false, Address = Address, AuthorizedPerson = AuthorizedPerson, Position = Position, RegisterDate = DateTime.Now, Url = Url, WebUrl = WebUrl, IsSale = false, CityNum = CityNum, DistrictNum = DistrictNum, Description = Description, CategoryId = CategoryId, NumOfEmployees = NumOfEmployees, PhoneNumber = PhoneNumber };

                    CheckIt(user);
                    if (string.IsNullOrEmpty(Role))
                    {
                        manager.AddToRole(user.Id, Role);
                    }
                    context.Users.Add(user);
                    context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new Exception("Firma sistemde kayıtlı.");
                }
            }
        }

        public static User Update(User model)
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

        public static void Delete(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var data = db.Users.Find(id);
                data.IsDelete = true;
                data.DeleteDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        public static void CheckIt(User model)
        {
            try
            {
                if (model.UserName == null)
                {
                    throw new Exception("Firma adı boş geçilemez");
                }
                else if (model.Email == null)
                {
                    throw new Exception("Email boş geçilemez");
                }
                else if (model.Url == null)
                {
                    model.Url = Tool.CreateUrlSlug(model.UserName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PackageInfo GetPacgageInfo(string id)
        {
            var model = new PackageInfo();
            model.discountTexts = new List<string>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = bUser.GetById(id);
                model.package = db.Packages.FirstOrDefault(w => user.NumOfEmployees >= w.MinEmployees && user.NumOfEmployees <= w.MaxEmployees);

                if (user.LocationCount > 4 && user.LocationCount <= 14)
                {
                    model.discountTexts.Add("%10 Lokasyon İndirimi");
                    model.totalDiscountPercent += 10;
                }
                else if (user.LocationCount >= 15 && user.LocationCount <= 49)
                {
                    model.discountTexts.Add("%15 Lokasyon İndirimi");
                    model.totalDiscountPercent += 15;
                }
                else if (user.LocationCount >= 50)
                {
                    model.discountTexts.Add("%20 Lokasyon İndirimi");
                    model.totalDiscountPercent += 20;
                }

                var corporateScore = GetCorporateScore(user.Id);
                if (corporateScore >= 10)
                {
                    model.discountTexts.Add("%10 Kurumsal Puan İndirimi");
                    model.totalDiscountPercent += 10;
                }

                model.totalDiscountPrice = (model.package.Price / 100) * model.totalDiscountPercent;
                model.GrandTotal = model.package.Price - model.totalDiscountPrice;

            }


            return model;
        }


    }
}
