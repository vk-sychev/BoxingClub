using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClud.Exceptions.Exceptions
{
    public class NotFoundException : BoxingClubException
    {
        public NotFoundException(string message, string prop) : base(message, prop)
        {
        }
    }
}
