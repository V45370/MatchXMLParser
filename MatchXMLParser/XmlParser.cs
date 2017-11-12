using MatchXMLParser.Models;
using MatchXMLParser.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace MatchXMLParser
{
    public class XmlParser
    {
        public void ParseMatch(string filePath)
        {
            var xDoc = XDocument.Load(filePath);
            if(xDoc != null)
            {
                XElement xml = XElement.Parse(xDoc.ToString());
                Extract(xml);
            }
        }

        private void Extract(XElement xml)
        {
            string country = xml.Element("country").Value;
            string league = xml.Element("league").Value;
            string season = xml.Element("season").Value;
            //int stage = int.Parse(xml.Element("stage").Value);
            string stage = xml.Element("stage").Value;
            //int matchId = int.Parse(xml.Element("matchId").Value);
            string matchId = xml.Element("matchId").Value;
            DateTime date = DateTime.Parse(xml.Element("date").Value);
            //int homeTeamId = int.Parse(xml.Element("homeTeamId").Value);
            string homeTeamId = xml.Element("homeTeamId").Value;
            //int awayTeamId = int.Parse(xml.Element("awayTeamId").Value);
            string awayTeamId = xml.Element("awayTeamId").Value;
            string homeTeamFullName = xml.Element("homeTeamFullName").Value;
            string awayTeamFullName = xml.Element("awayTeamFullName").Value;
            string homeTeamAcronym = xml.Element("homeTeamAcronym").Value;
            string awayTeamAcronym = xml.Element("awayTeamAcronym").Value;
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
            for (int i = 0; i < 11; i++)
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

            //Goals
            var goalNodes = xml.Elements("goal").Nodes();
            List<Goal> goals = new List<Goal>();
            foreach (XElement goalNode in goalNodes)
            {
                string goalId = goalNode.Element("id").Value;
                string minute = goalNode.Element("elapsed").Value;
                string scorerId = goalNode.Element("player1").Value;
                string assistId = goalNode.Element("player2") != null ? goalNode.Element("player2").Value : null;
                string type = goalNode.Element("subtype") != null ? goalNode.Element("subtype").Value : null;
                string teamId = goalNode.Element("team").Value;

                Goal goal = new Goal()
                {
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
