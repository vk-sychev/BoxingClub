using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Models;

namespace HttpClientAdapters.Models
{
    public class UsersResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public List<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
