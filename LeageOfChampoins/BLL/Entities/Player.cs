using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Player : IdKey
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public double Cost { get; set; }
        public int Games { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int IndividualAwards { get; set; }

        [ForeignKey("ClubId")]
        public int ClubId { get; set; }
        public Club Club { get; set; }
    }
}
