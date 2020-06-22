using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static RateListing.Model.Enums;

namespace RateListing.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public double? Score { get; set; }
        public string AuthorizedPerson { get; set; }
        public string Position { get; set; }
        public int? NumOfEmployees { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string WebUrl { get; set; }
        public int? CityNum { get; set; }
        public int? DistrictNum { get; set; }
        public UyelikTipi MembershipType { get; set; }
        public string Address { get; set; }
        [AllowHtml]
        public string Location { get; set; }
        [ForeignKey("Categories")]
        public int? CategoryId { get; set; }
        public int? Category2Id { get; set; }
        public int? Category3Id { get; set; }
        public string Url { get; set; }
        public int FoundationYear { get; set; }
        public string RegisteredRoom { get; set; }
        public string RegisteredFederation { get; set; }
        public double CreditScore { get; set; }
        public string TaxPlace { get; set; }
        public int TaxNo { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public bool BonusFeature { get; set; }
        public string LicenceDocument { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsSale { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public City Cities { get; set; }
        public District Districts { get; set; }
        public virtual Category Categories { get; set; }
        public bool isInformationConfirmed { get; set; }
        public bool isPaymentConfirmed { get; set; }
        public string AlternativePhone { get; set; }
        public int LocationCount { get; set; }
        public CompanyType CompanyType { get; set; }
        public string ServiceDefinition { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PaymentMethods { get; set; }
        public string SalientFeature1 { get; set; }
        public string SalientFeature2 { get; set; }
        public string SalientFeature3 { get; set; }

        public string Slogan { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
