using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Couch : IdKey
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int AmountTrophies { get; set; }
        public int Games { get; set; }
        public double PointsPerGame { get; set; }
        public string MainSchema { get; set; }
        public double Salary { get; set; }
        public DateOnly DateExpirContract { get; set; }
        
    }
}
