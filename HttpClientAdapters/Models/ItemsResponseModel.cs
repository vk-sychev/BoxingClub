using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpClients.Models;

namespace HttpClientAdapters.Models
{
    public class ItemsResponseModel<T> where T: class
    {
        public HttpStatusCode StatusCode { get; set; }

        public List<T> Items { get; set; } = new List<T>();
    }
}
