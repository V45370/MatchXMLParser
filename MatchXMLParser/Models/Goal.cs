using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class Goal : IEntity
    {
        public Goal()
        {

        }
       
        public int Id { get; set; }

        public int MatchId { get; set; }
        //public int ExternalId { get; set; }
        public string ExternalId { get; set; }
        //public int Minute { get; set; }
        public string Minute { get; set; }
        //public int ScorerId { get; set; }
        public string ScorerId { get; set; }
        //public int AssistId { get; set; }
        public string AssistId { get; set; }
        public string Type { get; set; }
        //public int TeamId { get; set; }
        public string TeamId { get; set; }
    }
}
