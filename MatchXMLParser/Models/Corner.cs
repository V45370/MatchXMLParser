using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class Corner : IEntity
    {
        public Corner()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExternalId { get; set; }
    
        public int MatchId { get; set; }
        public string Minute { get; set; }
        public string Player1 { get; set; }

        public string TeamId { get; set; }
    }
}
