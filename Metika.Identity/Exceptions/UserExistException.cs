using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metika.Identity.Exceptions
{
    public class UserExistException : Exception
    {
        public UserExistException() : base("User exists!")
        {
            
        }
    }
}
