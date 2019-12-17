using MatchXMLParser.Models;
using System.Data.Entity;

using System.Data.Entity.Migrations;

namespace MatchXMLParser.Repos
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
            : base("MatchXMLParser")
        {
            Database.SetInitializer<MainDbContext>(new MigrateDatabaseToLatestVersion<MainDbContext, MigrateDbConfiguration>());
        }

        public virtual IDbSet<Match> Matches { get; set; }
        public virtual IDbSet<Player> Players { get; set; }
        public virtual IDbSet<Team> Teams { get; set; }
        public virtual IDbSet<Goal> Goals { get; set; }

        public virtual IDbSet<Corner> Corners { get; set; }

        public virtual IDbSet<Possession> Possessions { get; set; }

        public virtual IDbSet<ShotOn> ShotsOn{ get; set; }

        public virtual IDbSet<ShotOff> ShotsOff { get; set; }

        public virtual IDbSet<Cross> Crosses{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dcs".ToUpper());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainDbContext, MigrateDbConfiguration>());
        }

        public static MainDbContext Create()
        {
            return new MainDbContext();
        }
    }

    public class MigrateDbConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<MainDbContext>
    {
        public MigrateDbConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }
    }
}



