using BoxingClub.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BoxingClub.Infrastructure.HttpSwitcher
{
    static public class HttpCodeHelper
    {
        public static int GetSwitchHttpCode(Type exceptionType)
        {
            if (exceptionType == typeof(NotFoundException)) return (int)HttpStatusCode.NotFound;
            if (exceptionType == typeof(ArgumentException)) return (int)HttpStatusCode.BadRequest;
            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
