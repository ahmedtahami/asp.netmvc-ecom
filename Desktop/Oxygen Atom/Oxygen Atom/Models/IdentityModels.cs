using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Oxygen_Atom.Entities;
using System.ComponentModel.DataAnnotations;

namespace Oxygen_Atom.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
          var user= await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
          return user;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("OxygenAtomDb",throwIfV1Schema:false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SubCategory>()
                    .HasRequired(i => i.Category)
                    .WithMany()
                    .WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductSizes>()
                    .HasRequired(i => i.Product)
                    .WithMany()
                    .WillCascadeOnDelete(false);
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductSizes> ProductSizes  { get; set; }
        public DbSet<SubCategory> SubCategories  { get; set; }
    }
}