using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Exceptions
{
    public class MinimalContrepartieException : Exception
    {
        public MinimalContrepartieException() : base("Le projet doit contenir au minimum 3 contreparties.") { }
    }
}
