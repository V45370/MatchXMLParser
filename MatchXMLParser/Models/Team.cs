using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchXMLParser.Models
{
    public class Team : IEntity
    {
        public Team()
        {

        }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        //public int ExternalId { get; set; }
        [Key]
        public int ExternalId { get; set; }
        public string FullName { get; set; }
        public string Acronym { get; set; }
    }
}
