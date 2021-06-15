using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Models;

namespace HttpClientAdapters.Models
{
    public class PageModelResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public PageModel<UserModel> Users { get; set; }
    }
}
