using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientAdapters.Models
{
    public class TokenResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public string AccessToken { get; set; }
    }
}
