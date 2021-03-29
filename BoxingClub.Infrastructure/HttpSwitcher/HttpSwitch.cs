using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BoxingClub.Infrastructure.HttpSwitcher
{
    static public class HttpSwitch
    {
        public static int SwitchHttpCode(string exceptionType)
        {
            switch (exceptionType)
            {
                case "NotFoundException":
                    return (int)HttpStatusCode.NotFound;
                case "ArgumentNullException":
                    return (int)HttpStatusCode.BadRequest;
                default: return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
