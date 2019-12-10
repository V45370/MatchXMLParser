using MatchXMLParser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser
{
    public class Match : IEntity
    {
        public Match()
        {
            HomePlayers = new List<Player>();
            AwayPlayers = new List<Player>();
        }

        //public int Id { get; set; }
        //public int ExternalId { get; set; }
        [Key]
        public int ExternalId { get; set; }
        public DateTime Date { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
        public string Season { get; set; }
        //public int Stage { get; set; }
        public string Stage { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual List<Player> HomePlayers { get; set; }
        public virtual List<Player> AwayPlayers { get; set; }
    }
}
