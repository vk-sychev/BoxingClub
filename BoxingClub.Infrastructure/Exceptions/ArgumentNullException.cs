using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.Infrastructure.Exceptions
{
    public class ArgumentNullException : BoxingClubException
    {
        public ArgumentNullException(string message, string prop) : base(message, prop)
        {
        }
    }
}
