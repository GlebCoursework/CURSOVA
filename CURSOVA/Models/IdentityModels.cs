using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;

namespace CURSOVA.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<BoughtList> BoughtLists { get; set; }
    }

    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public virtual List<Component> Components { get; set; }
    }

    public class BoughtPizza
    {
        public int Id { get; set; }
        public virtual Pizza Pizza { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class BoughtList
    {
        public int Id { get; set; }
        public virtual List<BoughtPizza> BoughtPizzas { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<BoughtList> BoughtList { get; set; }
        public virtual DbSet<BoughtPizza> BoughtPizza { get; set; }
        public virtual DbSet<Component> Components { get; set; }
    }
}