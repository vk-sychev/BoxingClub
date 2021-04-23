using BoxingClub.Infrastructure.Exceptions;
using BoxingClub.Infrastructure.Helpers;
using System;
using System.Net;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.Infrastructure.HttpSwitcher
{
    static public class HttpCodeHelper
    {
        public static ErrorViewModel GetSwitchHttpCode(Type exceptionType)
        {
            if (exceptionType == typeof(NotFoundException)) return new ErrorViewModel
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "404 - Page Not Found"
            };

            if (exceptionType == typeof(ArgumentNullException)) return new ErrorViewModel
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Bad Request"
            };

            return new ErrorViewModel
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Internal server error"
            };

            /*            if (exceptionType == typeof(NotFoundException)) return "404 - Page Not Found";
                        if (exceptionType == typeof(ArgumentNullException)) return "Bad Request occurred";
                        return "Internal server error occured while processing your request";*/
        }
    }
}
