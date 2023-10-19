using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Exceptions
{
    public class WrongsCredentialsException: Exception
    {
        public WrongsCredentialsException() : base("L'identifiant ou le mot de passe est incorrect.") { }
    }
}
