using MatchXMLParser.Models;
using MatchXMLParser.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace MatchXMLParser
{
    public class XmlParser
    {

        public void ParseAllMatches(List<string> fileNames)
        {
            fileNames.SelectAsync((fileName) => ProcessXML(fileName)).Wait();
        }
        private Task<string> ProcessXML(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (ext == ".xml")
            {
                Console.WriteLine(fileName);
                try
                {
                    this.ParseMatch(fileName);
                }
                catch (XmlException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return Task.FromResult("");
        }

        public void ParseMatch(string filePath)
        {
            XElement xml = null;
            try
            {
                var xDoc = XDocument.Load(filePath);
                if (xDoc != null)
                {
                     xml = XElement.Parse(xDoc.ToString());
                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldnt parse match" + ex.Message);
            }
            try
            {
                if(xml != null)
                   Extract(xml);
            }
            catch
            {

            }
        }

        private void Extract(XElement xml)
        {
            string country = xml.Element("country") != null ? xml.Element("country").Value : null;
            string league = xml.Element("league") != null ? xml.Element("league").Value : null;
            string season = xml.Element("season") != null ? xml.Element("season").Value : null;
            //int stage = int.Parse(xml.Element("stage").Value);
            string stage = xml.Element("stage") != null ? xml.Element("stage").Value : null;
            //int matchId = int.Parse(xml.Element("matchId").Value);
            string matchId = xml.Element("matchId") != null ? xml.Element("matchId").Value : null;
            DateTime date = DateTime.Parse(xml.Element("date") != null ? xml.Element("date").Value : null);
            //int homeTeamId = int.Parse(xml.Element("homeTeamId").Value);
            string homeTeamId = xml.Element("homeTeamId") != null ? xml.Element("homeTeamId").Value : null;
            //int awayTeamId = int.Parse(xml.Element("awayTeamId").Value);
            string awayTeamId = xml.Element("awayTeamId") != null ? xml.Element("awayTeamId").Value : null;
            string homeTeamFullName = xml.Element("homeTeamFullName") != null ? xml.Element("homeTeamFullName").Value : null;
            string awayTeamFullName = xml.Element("awayTeamFullName") != null ? xml.Element("awayTeamFullName").Value : null;
            string homeTeamAcronym = xml.Element("homeTeamAcronym") != null ? xml.Element("homeTeamAcronym").Value : null;
            string awayTeamAcronym = xml.Element("awayTeamAcronym") != null ? xml.Element("awayTeamAcronym").Value : null;
            List<string> homePlayersName = xml.Element("homePlayers").Nodes().Select(el => (el as XElement).Value).ToList();
            List<string> awayPlayersName = xml.Elements("awayPlayers").Nodes().Select(el => (el as XElement).Value).ToList();
            //List<int> homePlayersId = xml.Elements("homePlayersId").Nodes().Select(el => (el as XElement).Value).ToList().ConvertAll(s => Int32.Parse(s));
            //List<int> awayPlayersId = xml.Elements("awayPlayersId").Nodes().Select(el => (el as XElement).Value).ToList().ConvertAll(s => Int32.Parse(s));
            List<string> homePlayersId = xml.Elements("homePlayersId").Nodes().Select(el => (el as XElement).Value).ToList();
            List<string> awayPlayersId = xml.Elements("awayPlayersId").Nodes().Select(el => (el as XElement).Value).ToList();

            //Teams
            Team homeTeam = new Team()
            {
                ExternalId = homeTeamId,
                FullName = homeTeamFullName,
                Acronym = homeTeamAcronym
            };
            CreateTeam(homeTeam);

            Team awayTeam = new Team()
            {
                ExternalId = awayTeamId,
                FullName = awayTeamFullName,
                Acronym = awayTeamAcronym
            };
            CreateTeam(awayTeam);


            //Players
            List<Player> homePlayers = new List<Player>();
            List<Player> awayPlayers = new List<Player>();
            for (int i = 0; i < homePlayers.Count; i++)
            {
                string awayPlayerId = awayPlayersId.ElementAt(i);
                //string awayPlayerName = awayPlayersName.ElementAt(i);
                Player awayPlayer = new Player()
                {
                    ExternalId = awayPlayerId,
                    //Name = awayPlayerName
                };
                CreatePlayer(awayPlayer);
                awayPlayers.Add(awayPlayer);
            }

            for (int i = 0; i < homePlayers.Count; i++)
            {
                string homePlayerId = homePlayersId.ElementAt(i);
                //string homePlayerName = homePlayersName.ElementAt(i);
                Player homePlayer = new Player()
                {
                    ExternalId = homePlayerId,
                    //Name = homePlayerName
                };
                CreatePlayer(homePlayer);
                homePlayers.Add(homePlayer);
            }
            //Goals
            List<Goal> goals = new List<Goal>();
            var goalNodes = xml.Elements("goal").Nodes();
            foreach (XElement goalNode in goalNodes)
            {
                string goalId = goalNode.Element("id")!= null ? goalNode.Element("id").Value : null; ; ;
                string minute = goalNode.Element("elapsed")!= null ? goalNode.Element("elapsed").Value : null;
                string scorerId = goalNode.Element("player1") != null? goalNode.Element("player1").Value : null;
                string assistId = goalNode.Element("player2") != null ? goalNode.Element("player2").Value : null;
                string type = goalNode.Element("subtype") != null ? goalNode.Element("subtype").Value : null;
                string teamId = goalNode.Element("team")?.Value;

                int matchIdInt = int.Parse(matchId);

                Goal goal = new Goal()
                {
                    MatchId = matchIdInt,
                    ExternalId = goalId,
                    Minute = minute,
                    ScorerId = scorerId,
                    AssistId = assistId,
                    Type = type,
                    TeamId = teamId
                };
                CreateGoal(goal);
                goals.Add(goal);
            }

            List<Corner> corners = new List<Corner>();
            var cornerNodes = xml.Elements("corner").Nodes();
            foreach (XElement corner in cornerNodes)
            {
                string goalId = corner.Element("id").Value;
                string minute = corner.Element("elapsed").Value;
                string scorerId = corner.Element("player1") != null ? corner.Element("player1").Value : null;
                string teamId = corner.Element("team") != null ? corner.Element("team").Value : null;

                int matchIdInt = int.Parse(matchId);

                Corner cornerObject = new Corner()
                {
                    MatchId = matchIdInt,
                    ExternalId = goalId,
                    Minute = minute,
                    TeamId = teamId
                };
                CreateGoal(cornerObject);
                corners.Add(cornerObject);
            }

            //Match
            Match match = new Match()
            {
                ExternalId = matchId,
                Date = date,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                Country = country,
                League = league,
                Season = season,
                Stage = stage,
                AwayPlayers = awayPlayers,
                HomePlayers = homePlayers
            };
            //match.Goals = goals;
            CreateMatch(match);
        }
        private void CreateMatch(Match match)
        {
            var matchRepo = new MatchRepository();
            matchRepo.Add(match);
        }
        private void CreateTeam(Team team)
        {
            var teamRepo = new TeamRepository();

            Team existing = teamRepo.FindByExternalId(team.ExternalId);
            if (existing == null)
            {
                teamRepo.Add(team);
            }
        }
        private void CreateGoal(Goal goal)
        {
            var goalRepo = new GoalRepository();
            goalRepo.Add(goal);
        }

        private void CreateGoal(Corner corner)
        {
            var cornerRepo = new CornerRepository();
            cornerRepo.Add(corner);
        }

        private void CreatePlayer(Player player)
        {
            var playerRepo = new PlayerRepository();

            Player existing = playerRepo.FindByExternalId(player.ExternalId);
            if (existing == null)
            {
                playerRepo.Add(player);
            }
        }
    }
}
