using System;
using System.Collections.Generic;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        //public int ExternalId { get; set; }
        public int ExternalId { get; set; }
        public string FullName { get; set; }
        public string Acronym { get; set; }
    }
}
