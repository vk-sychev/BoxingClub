using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Helpers
{
    public static class PageSizeHelper
    {
        public static List<int> GetPageSizeList(int maxValue)
        {
            List<int> sizeList = new List<int>();
            for (int i=1; i<=maxValue; i++)
            {
                sizeList.Add(i);
            }
            return sizeList;
        }
    }
}
