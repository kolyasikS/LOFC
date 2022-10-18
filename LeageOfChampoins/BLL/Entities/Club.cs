using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Club : IdKey
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public DateOnly DateFoundation { get; set; }
        public double Cost { get; set; }
        public int PosUEFARatingClubs { get; set; }
        public int AmountTrophies { get; set; }
        public string Schema { get; set; }
        public int posInLeague { get; set; }
        public char? Group { get; set; }
        [ForeignKey("LeagueId")]
        public int LeagueId { get; set; }
        public League League { get; set; }
        [ForeignKey("CouchId")]
        public int? CouchId { get; set; }
        public Couch? Couch { get; set; }
    }
}
