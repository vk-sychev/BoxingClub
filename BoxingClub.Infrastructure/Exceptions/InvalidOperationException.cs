using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.Infrastructure.Exceptions
{
    public class InvalidOperationException : BoxingClubException
    {
        public InvalidOperationException(string message) : base(message)
        {
        }
    }
}
