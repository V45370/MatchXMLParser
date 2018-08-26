using System.IO;
using System.Linq;
using MatchXMLParser.Repos;

namespace MatchXMLParser
{
    class Program
    {
        static XmlParser parser = new XmlParser();

        static void Main(string[] args)
        {
            string rootDir = @"G:\GitProjects\FootballDataCollectionFork2\footballData\footballData\matches\England\Premier League\2017";
            string[] fileNames = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
            parser.ParseAllMatches(fileNames.ToList());
        }
      

        public static void QueryGoals()
        {
            MatchRepository matchRepo = new MatchRepository();
            var matches = matchRepo.List.ToList();
            GoalRepository goalRepository = new GoalRepository();
            //Premier League
            //int matchesCount = matches.Where(x => x.League == "Spain Primera Division").Count();
            //foreach (Match match in matches.Where(x => x.League == "Spain Primera Division"))
            //{
            //    int id = int.Parse(match.ExternalId);
            //    var goals = goalRepository.List.Where(x => x.MatchId == id).OrderBy(y => y.Minute);
            //}
        }
    }
}
