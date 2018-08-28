using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class Possession : IEntity
    {
        public Possession() { }

        public string ExternalId { get; set; }
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Minute { get; set; }
        public string HomePossession { get; set; }
        public string AwayPossession { get; set; }
        public int TeamId { get; set; }
    }
}
