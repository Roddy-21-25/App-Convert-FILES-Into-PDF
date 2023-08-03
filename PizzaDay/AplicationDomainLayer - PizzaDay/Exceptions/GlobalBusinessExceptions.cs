using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomainLayer___PizzaDay.Exceptions
{
    public class GlobalBusinessExceptions : Exception
    {
        public GlobalBusinessExceptions()
        {
            
        }
        public GlobalBusinessExceptions(string ContextException) : base(ContextException) { }
    }
}
