using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class Possession : IEntity
    {
        public Possession() { }
        [Key]
        public int ExternalId { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        public int MatchId { get; set; }
        public string Minute { get; set; }
        public string HomePossession { get; set; }
        public string AwayPossession { get; set; }
        public int TeamId { get; set; }
    }
}
