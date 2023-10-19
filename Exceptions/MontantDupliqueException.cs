using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class MontantDupliqueException : Exception
    {
        public MontantDupliqueException() : base("Le montant doit être différent.") { }
    }
}
