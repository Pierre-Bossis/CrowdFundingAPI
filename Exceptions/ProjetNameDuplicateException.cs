using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Exceptions
{
    public class ProjetNameDuplicateException : Exception
    {
        public ProjetNameDuplicateException() : base("Le nom du projet est déjà pris.") { }
    }
}
