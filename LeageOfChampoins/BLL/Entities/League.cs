using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class League : IdKey
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public int AmountClubs { get; set; }
        public string CurrentWinner { get; set; }
        public int posUEFARatingLeagues { get; set; }
    }
}
