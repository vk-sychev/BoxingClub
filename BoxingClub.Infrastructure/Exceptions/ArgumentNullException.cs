namespace BoxingClub.Infrastructure.Exceptions
{
    public class ArgumentNullException : BoxingClubException
    {
        public ArgumentNullException(string message, string prop) : base(message, prop)
        {
        }
    }
}
