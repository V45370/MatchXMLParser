using MatchXMLParser.Models;
using System.Data.Entity;

namespace MatchXMLParser.Repos
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
            : base("MatchXMLParser")
        {
            Database.SetInitializer<MainDbContext>(new CreateDatabaseIfNotExists<MainDbContext>());
        }

        public virtual IDbSet<Match> Matches { get; set; }
        public virtual IDbSet<Player> Players { get; set; }
        public virtual IDbSet<Team> Teams { get; set; }
        public virtual IDbSet<Goal> Goals { get; set; }

        public virtual IDbSet<Corner> Corners { get; set; }

        public virtual IDbSet<Possession> Possessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MainDbContext, MigrateDbConfiguration>());
        }
    }

    public class MigrateDbConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<MainDbContext>
    {
        public MigrateDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }
    }
}



