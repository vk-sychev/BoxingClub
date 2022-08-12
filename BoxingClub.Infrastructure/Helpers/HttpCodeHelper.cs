using System;
using System.Net;
using BoxingClub.Infrastructure.Exceptions;
using ArgumentNullException = BoxingClub.Infrastructure.Exceptions.ArgumentNullException;

namespace BoxingClub.Infrastructure.Helpers
{
    public static class HttpCodeHelper
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
        }
    }
}
