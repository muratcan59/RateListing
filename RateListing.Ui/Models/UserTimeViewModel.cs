using RateListing.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using static RateListing.Model.Enums;

namespace RateListing.Ui.Models
{
    public class UserTimeViewModel
    {
        public string Id { get; set; }
        public int CategoryId { get; set; }
        public int TaxNo { get; set; }
        public string TaxPlace { get; set; }
        public int FoundationYear { get; set; }
        public DateTime CreateDate { get; set; }
        public int NumOfEmployees { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        public string RegisteredFederation { get; set; }
        public string RegisteredRoom { get; set; }
        public string SecurityStamp { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string AuthorizedPerson { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string WebUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int? CityNum { get; set; }
        public int? DistrictNum { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public bool IsPersonCompany { get; set; }
        public bool BonusFeature { get; set; }
        public string LicenceDocument { get; set; }
        public List<Gunler> Day { get; set; }
        public List<int> TimeId { get; set; }
        public List<DateTime> OpenTime { get; set; }
        public List<DateTime> CloseTime { get; set; }
    }
}