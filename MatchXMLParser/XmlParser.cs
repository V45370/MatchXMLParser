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
            try
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
                ExternalId = int.Parse(homeTeamId),
                FullName = homeTeamFullName,
                Acronym = homeTeamAcronym
            };
            CreateTeam(homeTeam);

            Team awayTeam = new Team()
            {
                ExternalId = int.Parse(awayTeamId),
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
                    ExternalId = int.Parse(awayPlayerId),
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
                    ExternalId = int.Parse(homePlayerId),
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
                string goalId = goalNode.Element("id")!= null ? goalNode.Element("id").Value : null;
                string minute = goalNode.Element("elapsed")!= null ? goalNode.Element("elapsed").Value : null;
                string scorerId = goalNode.Element("player1") != null? goalNode.Element("player1").Value : null;
                string assistId = goalNode.Element("player2") != null ? goalNode.Element("player2").Value : null;
                string type = goalNode.Element("subtype") != null ? goalNode.Element("subtype").Value : null;
                string teamId = goalNode.Element("team")?.Value;

                int matchIdInt = int.Parse(matchId);

                Goal goal = new Goal()
                {
                    MatchId = matchIdInt,
                    ExternalId = int.Parse(goalId),
                    Minute = minute,
                    ScorerId = scorerId,
                    AssistId = assistId,
                    Type = type,
                    TeamId = teamId
                };
                CreateGoal(goal);
                goals.Add(goal);
            }

            var cornerNodes = xml.Elements("corner").Nodes();
            foreach (XElement corner in cornerNodes)
            {
                string cornerId = corner.Element("id").Value;
                string minute = corner.Element("elapsed").Value;
                string player1 = corner.Element("player1") != null ? corner.Element("player1").Value : null;
                string teamId = corner.Element("team") != null ? corner.Element("team").Value : null;

                int matchIdInt = int.Parse(matchId);

                Corner cornerObject = new Corner()
                {
                    MatchId = matchIdInt,
                    ExternalId = int.Parse(cornerId),
                    Minute = minute,
                    TeamId = teamId,
                    Player1 = player1
                };
                CreateCorner(cornerObject);
            }

            var shotOnNodes = xml.Elements("shoton").Nodes();
            foreach (XElement shotOn in shotOnNodes)
            {
                string cornerId = shotOn.Element("id").Value;
                string minute = shotOn.Element("elapsed").Value;
                string player1 = shotOn.Element("player1") != null ? shotOn.Element("player1").Value : null;
                string teamId = shotOn.Element("team") != null ? shotOn.Element("team").Value : null;

                int matchIdInt = int.Parse(matchId);

                ShotOn shotOnObject = new ShotOn()
                {
                    MatchId = matchIdInt,
                    ExternalId = int.Parse(cornerId),
                    Minute = minute,
                    TeamId = teamId,
                    Player1 = player1
                };
                CreateShotOn(shotOnObject);
            }

            var shotOffNodes = xml.Elements("shotoff").Nodes();
            foreach (XElement shotOff in shotOffNodes)
            {
                string cornerId = shotOff.Element("id").Value;
                string minute = shotOff.Element("elapsed").Value;
                string player1 = shotOff.Element("player1") != null ? shotOff.Element("player1").Value : null;
                string teamId = shotOff.Element("team") != null ? shotOff.Element("team").Value : null;

                int matchIdInt = int.Parse(matchId);

                ShotOff shotOffObject = new ShotOff()
                {
                    MatchId = matchIdInt,
                    ExternalId = int.Parse(cornerId),
                    Minute = minute,
                    TeamId = teamId,
                    Player1 = player1
                };
                CreateShotOff(shotOffObject);
            }

            var crossesNodes = xml.Elements("cross").Nodes();
            foreach (XElement cross in crossesNodes)
            {
                string cornerId = cross.Element("id").Value;
                string minute = cross.Element("elapsed").Value;
                string player1 = cross.Element("player1") != null ? cross.Element("player1").Value : null;
                string teamId = cross.Element("team") != null ? cross.Element("team").Value : null;

                int matchIdInt = int.Parse(matchId);

                Cross crossObject = new Cross()
                {
                    MatchId = matchIdInt,
                    ExternalId = int.Parse(cornerId),
                    Minute = minute,
                    TeamId = teamId,
                    Player1 = player1,
                };
                CreateCross(crossObject);
            }

            List<Possession> possessions = new List<Possession>();
            var possessionNodes = xml.Elements("possession").Nodes();
            foreach (XElement corner in possessionNodes)
            {
                string minute = corner.Element("elapsed").Value;
                string homePos = corner.Element("homepos") != null ? corner.Element("homepos").Value : string.Empty;
                string awayPos = corner.Element("awaypos") != null ? corner.Element("awaypos").Value : string.Empty;
                string possessionId = corner.Element("id").Value;
                string teamId = corner.Element("team") != null ? corner.Element("team").Value : null ;

                int matchIdInt = int.Parse(matchId);

                Possession posessionObject = new Possession()
                {
                    MatchId = matchIdInt,
                    Minute = minute,
                    HomePossession = homePos,
                    AwayPossession = awayPos,
                    ExternalId = int.Parse(possessionId),
                };

                if(teamId != null)
                    posessionObject.TeamId = int.Parse(teamId);
                CreatePossession(posessionObject);
            }

            //Match
            Match match = new Match()
            {
                ExternalId = int.Parse(matchId),
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
            catch (Exception ex)
            {

            }
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

        private void CreatePossession(Possession poss)
        {
            var possRepo = new PossessionRepository();
            possRepo.Add(poss);
        }
        private void CreateGoal(Goal goal)
        {
            var goalRepo = new GoalRepository();
            goalRepo.Add(goal);
        }

        private void CreateCorner(Corner corner)
        {
            var cornerRepo = new CornerRepository();
            cornerRepo.Add(corner);
        }

        private void CreateShotOn(ShotOn shotOn)
        {
            var shotOnRepostirotry = new ShotOnRepostirotry();
            shotOnRepostirotry.Add(shotOn);
        }

        private void CreateShotOff(ShotOff shotOff)
        {
            var shotOffRepostirotry = new ShotOffRepostirotry();
            shotOffRepostirotry.Add(shotOff);
        }

        private void CreateCross(Cross cross)
        {
            var crossRepostirotry = new CrossesRepository();
            crossRepostirotry.Add(cross);
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
