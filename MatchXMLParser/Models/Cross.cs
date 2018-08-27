using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class Cross : IEntity
    {
        public Cross() { }

        public string ExternalId { get; set; }
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Minute { get; set; }
        public string Player1 { get; set; }

        public string TeamId { get; set; }
    }
}
