using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.Infrastructure.Exceptions
{
    public class ArgumentException : BoxingClubException
    {
        public ArgumentException(string message, string property) : base(message, property)
        {

        }
    }
}
