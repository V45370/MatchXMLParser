using System.Data.Entity.Migrations;

namespace MatchXMLParser.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Repos.MainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Repos.MainDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
