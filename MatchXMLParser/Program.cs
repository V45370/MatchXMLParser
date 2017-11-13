using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MatchXMLParser.Models;
using MatchXMLParser.Repos;

namespace MatchXMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryGoals();

            string rootDir = @"H:\OneDrive\Projects\BettingSerivce\FootballDataCollectionFork2\footballData\footballData\";
            string[] fileNames = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
            XmlParser parser = new XmlParser();

            foreach (var fileName in fileNames)
            {
                string ext = Path.GetExtension(fileName);
                if (ext == ".xml")
                {
                    Console.WriteLine(fileName);
                    try
                    {
                        parser.ParseMatch(fileName);
                    }
                    catch (XmlException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    catch(NullReferenceException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
           
        }

        public static void QueryGoals()
        {
            MatchRepository matchRepo = new MatchRepository();
            var matches = matchRepo.List.ToList();
            GoalRepository goalsGoalRepository = new GoalRepository();

            List<int> firstGoals = new List<int>();
            List<int> secondGoals = new List<int>();
            List<int> thirdGoals = new List<int>();


            //Premier League
            int goalsCount = 0, goalsFirstCount = 0, goalsSecondCound = 0, goalsThirdsCound = 0;
            int matchesCount = matches.Where(x => x.League == "Spain Primera Division").Count();
            foreach (Match match in matches.Where(x => x.League == "Spain Primera Division"))
            {
                int id = int.Parse(match.ExternalId);
                var goals = goalsGoalRepository.List.Where(x => x.MatchId == id).OrderBy(y => y.Minute);

                int count = 0;
                foreach (Goal goal in goals)
                {
                    int goalMinute = int.Parse(goal.Minute);
                    count++;
                    if (count == 1)
                    {
                        firstGoals.Add(goalMinute);
                        goalsFirstCount++;
                    }
                        
                    else if (count == 2)
                    {
                        secondGoals.Add(goalMinute);
                        goalsSecondCound++;
                    }
                    else if (count == 3)
                    {
                        thirdGoals.Add(goalMinute);
                        goalsThirdsCound++;
                    }
                        
                    goalsCount++;
                }
            }
        }
    }
}
