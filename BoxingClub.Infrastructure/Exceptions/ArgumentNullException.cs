namespace BoxingClub.Infrastructure.Exceptions
{
    public class ArgumentNullException : BoxingClubException
    {
        public ArgumentNullException(string prop, string message) : base(message, prop)
        {
        }
    }
}
