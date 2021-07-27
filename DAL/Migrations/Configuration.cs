namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.InventoryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.InventoryContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Users.AddOrUpdate(new DomainModels.User { Id = 1, Name = "Staff", Secret = "Staff" });
            context.Users.AddOrUpdate(new DomainModels.User { Id = 2, Name = "Admin", Secret = "Admin" });

            context.UserRoles.AddOrUpdate(new DomainModels.UserRole { Id = 1, UserId = 1, Role = "Staff" });
            context.UserRoles.AddOrUpdate(new DomainModels.UserRole { Id = 2, UserId = 2, Role = "Admin" });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            base.Seed(context);

        }
    }
}
