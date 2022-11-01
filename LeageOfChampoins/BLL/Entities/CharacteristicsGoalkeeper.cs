using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class CharacteristicsGoalkeeper : IdKey
    {
        public int Speed { get; set; }
        public int Diving { get; set; }
        public int Handling { get; set; }
        public int Positioning { get; set; }
        public int Kicking { get; set; }
        public int Reflexes { get; set; }
        [ForeignKey("Player")]
        public int playerId { get; set; }
        public Player Player { get; set; }
    }
}
