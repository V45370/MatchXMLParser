using MatchXMLParser.Models;
using System.Data.Entity;

namespace MatchXMLParser.Repos
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
            : base("MatchXMLParser")
        {

        }

        public virtual IDbSet<Match> Matches { get; set; }
        public virtual IDbSet<Player> Players { get; set; }
        public virtual IDbSet<Team> Teams { get; set; }
        public virtual IDbSet<Goal> Goals { get; set; }
    }
}
