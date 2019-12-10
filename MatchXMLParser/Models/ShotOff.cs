using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class ShotOff : IEntity
    {
        public ShotOff() { }
        [Key]
        public int ExternalId { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        public int MatchId { get; set; }
        public string Minute { get; set; }
        public string Player1 { get; set; }

        public string TeamId { get; set; }
    }
}
