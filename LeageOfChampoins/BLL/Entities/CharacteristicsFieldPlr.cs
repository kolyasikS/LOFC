using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class CharacteristicsFieldPlr : IdKey
    {
        public int Speed { get; set; }
        public int Dribbling { get; set; }
        public int Shooting { get; set; }
        public int Defending { get; set; }
        public int Pass { get; set; }
        public int Physics { get; set; }
        [ForeignKey("Player")]
        public int playerId { get; set; }
        public Player Player { get; set; }
    }
}
