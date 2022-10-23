using BLL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Owner : IdKey
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string Nation { get; set; }
        public int Age { get; set; }
        public double Capital { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [ForeignKey("ClubId")]
        public int? ClubId { get; set; }
        public Club? Club { get; set; }
    }
}
