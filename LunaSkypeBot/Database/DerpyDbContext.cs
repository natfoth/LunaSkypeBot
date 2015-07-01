using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using LunaSkypeBot.Database.Entity;

namespace LunaSkypeBot.Database
{
    public class DerpyDbContext : DbContext
    {
        public DerpyDbContext()
            : base("derpyDb")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            //System.Data.Entity.Database.SetInitializer<DerpyDbContext>(null);

        }

        public DbSet<DerpyImage> DerpyImages { get; set; }
        public DbSet<DerpyRepresentation> DerpyRepresentations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<DerpyDbContext>(
            //Database.Connection.ConnectionString, modelBuilder);
            //System.Data.Entity.Database.SetInitializer(sqliteConnectionInitializer);
            //var initializer = new DerpyDbInitializer(Database.Connection.ConnectionString, modelBuilder);
            //System.Data.Entity.Database.SetInitializer<DerpyDbContext>(null);
        }
    }


    public class DerpyDbHistoryContext : HistoryContext
    {
        public DerpyDbHistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable(tableName: "MigrationHistory", schemaName: "admin");
            modelBuilder.Entity<HistoryRow>().Property(p => p.MigrationId).HasColumnName("Migration_ID");
        }
    }
    //public class DerpyDbInitializer : SqliteDropCreateDatabaseAlways<DerpyDbContext>
    //{
    //    public DerpyDbInitializer(string connectionString, DbModelBuilder modelBuilder)
    //        : base(connectionString, modelBuilder)
    //    { }

    //    protected override void Seed(DerpyDbContext context)
    //    {
    //        context.Set<Team>().Add(new Team
    //        {
    //            Name = "YB",
    //            Players = new List<Player>
    //            {
    //                new Player
    //                {
    //                    City = "Bern",
    //                    FirstName = "Marco",
    //                    LastName = "Bürki",
    //                    Street = "Wunderstrasse 43"
    //                },
    //                new Player
    //                {
    //                    City = "Berlin",
    //                    FirstName = "Alain",
    //                    LastName = "Rochat",
    //                    Street = "Wonderstreet 13"
    //                }
    //            },
    //            Stadion = new Stadion
    //            {
    //                Name = "Stade de Suisse",
    //                City = "Bern",
    //                Street = "Papiermühlestrasse 71"
    //            }
    //        });
    //    }
    //}
}
