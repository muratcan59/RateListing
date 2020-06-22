using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RateListing.Model;

namespace RateListing.Dal.Entity
{
    // You can add profile data for the user by adding more properties to your User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<ProductService> ProductServices { get; set; }
        public virtual DbSet<Time> Times { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public  DbSet<City> Cities { get; set; }
        public  DbSet<District> Districts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentAnswer> CommentAnswers { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<OfferRequest> OfferRequests { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Package> Packages { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}