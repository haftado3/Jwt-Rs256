using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metika.Identity.Exceptions
{
    public class RegisterUserException : Exception
    {
        public RegisterUserException():base("User Register Exception!")
        {
            
        }
    }
}
