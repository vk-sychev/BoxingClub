using System;

namespace BoxingClub.Infrastructure.Exceptions
{
    public class BoxingClubException : Exception
    {

        public string Property { get; protected set; }
        public BoxingClubException(string message, string prop) : base(message)
        {
            Property = prop;
        }
        public BoxingClubException(string message) : base(message)
        {
        }
    }
}
