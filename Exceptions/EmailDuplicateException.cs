using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Exceptions
{
    public class EmailDuplicateException : Exception
    {
        public EmailDuplicateException() : base("L'email existe déjà.") { }
    }
}
