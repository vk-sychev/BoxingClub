using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Net;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.Infrastructure.HttpSwitcher
{
    static public class HttpCodeHelper
    {
        public static int GetSwitchHttpCode(Type exceptionType)
        {
            if (exceptionType == typeof(NotFoundException)) return (int)HttpStatusCode.NotFound;
            if (exceptionType == typeof(ArgumentNullException)) return (int)HttpStatusCode.BadRequest;
            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
