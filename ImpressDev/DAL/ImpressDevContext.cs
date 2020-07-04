using ImpressDev.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ImpressDev.DAL
{
    public class ImpressDevContext : IdentityDbContext<ApplicationUser>
    {
        public ImpressDevContext() : base("ImpressDevContext")
        { }

        static ImpressDevContext()
        {
            Database.SetInitializer<ImpressDevContext>(new ImpressDevInitializer());
        }

        public static ImpressDevContext Create()
        {
            return new ImpressDevContext();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //Disable the "s" addition conventions at the end of the table name (database)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}