using System.Data.Entity;
using System.Reflection;
using SQLite.CodeFirst;

namespace LunaSkypeBot.Database
{
    public class DerpyDbInitializer : SqliteCreateDatabaseIfNotExists<DerpyDbContext>
    {
        public DerpyDbInitializer(string connectionString, DbModelBuilder modelBuilder) : base(connectionString, modelBuilder)
        {
            //modelBuilder.Configurations.Add(new Migrations.Configuration());
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof (Migrations.Configuration)));
        }
    }
}