using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class Player : IEntity
    {
        public Player()
        {
            Matches = new List<Match>();
        }
       
        public int Id { get; set; }
        //public int ExternalId { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public virtual List<Match> Matches { get; set; }
    }
}
