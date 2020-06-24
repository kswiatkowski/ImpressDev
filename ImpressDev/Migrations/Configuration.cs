namespace ImpressDev.Migrations
{
    using ImpressDev.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ImpressDev.DAL.ImpressDevContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ImpressDev.DAL.ImpressDevContext";
        }

        protected override void Seed(ImpressDev.DAL.ImpressDevContext context)
        {
            ImpressDevInitializer.SeedImpressDevData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
