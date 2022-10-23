using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Account : IdKey
    {
        public string LogIn { get; set; }
        public string Password { get; set; }
    }
}
