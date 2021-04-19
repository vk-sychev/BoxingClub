namespace BoxingClub.Infrastructure.Exceptions
{
    public class InvalidOperationException : BoxingClubException
    {
        public InvalidOperationException(string message) : base(message)
        {
        }
    }
}
