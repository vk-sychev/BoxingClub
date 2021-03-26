using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClud.Exceptions.Exceptions
{
    public class BoxingClubException : Exception
    {
        public string Property { get; protected set; }
        public BoxingClubException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
