using System.Data.Entity.Migrations;
using LunaSkypeBot.Database;

namespace LunaSkypeBot.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DerpyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("System.Data.SQLite", new MigrationSqLiteGenerator());

            //SetHistoryContextFactory("System.Data.SQLite",
            //    (connection, defaultSchema) => new DerpyDbHistoryContext(connection, defaultSchema));
        }
        protected override void Seed(DerpyDbContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
