using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Owner :IdKey
    {
        string Name { get; set; }
        string? Surname { get; set; }
        string Nation { get; set; }
        int Age { get; set; }
        double Capital { get; set; }
        [ForeignKey("ClubId")]
        public int CLubId { get; set; }
        public Club Club { get; set; } 
    }
}
