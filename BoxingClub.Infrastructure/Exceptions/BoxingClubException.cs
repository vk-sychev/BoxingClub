using System;

namespace BoxingClub.Infrastructure.Exceptions
{
    public class BoxingClubException : Exception
    {
        public string Property { get; protected set; }

        public BoxingClubException(string message, string property) : base(message)
        {
            Property = property;
        }

        public BoxingClubException(string message) : base(message)
        {
        }
    }
}
