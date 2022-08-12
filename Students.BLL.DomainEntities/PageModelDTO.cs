using System.Collections.Generic;

namespace Students.BLL.DomainEntities
{
    public class PageModelDTO<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }

        public int Count { get; set; }
    }
}
