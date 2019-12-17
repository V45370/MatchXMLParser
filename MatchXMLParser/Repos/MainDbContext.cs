using MatchXMLParser.Migrations;
using MatchXMLParser.Models;
using System.Data.Entity;

using System.Data.Entity.Migrations;

namespace MatchXMLParser.Repos
{
    public class MainDbContext : DbContext
    {
        // Enable-Migrations -Auto
        // PM> Add-Migration -name TablesCreation -ConnectionStringName HistoricDataDbContext -ConfigurationTypeName Configuration
        public MainDbContext()
            : base("HistoricDataDbContext")
        {
            //Configuration
            Database.SetInitializer<MainDbContext>(new CreateDatabaseIfNotExists<MainDbContext>());
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
            modelBuilder.HasDefaultSchema("enetscores".ToUpper());
        }

        public static MainDbContext Create()
        {
            return new MainDbContext();
        }
    }

}



