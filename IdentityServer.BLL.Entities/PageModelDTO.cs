using System.Collections.Generic;

namespace IdentityServer.BLL.Entities
{
    public class PageModelDTO<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }

        public int Count { get; set; }
    }
}
