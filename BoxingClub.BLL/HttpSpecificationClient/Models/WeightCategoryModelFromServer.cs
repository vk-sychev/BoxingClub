using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingClub.BLL.Implementation.HttpSpecificationClient.Models
{
    public class WeightCategoryModelFromServer
    {
        public int Id { get; set; }

        public decimal MinValue { get; set; }

        public decimal MaxValue { get; set; }

        public string Title { get; set; }
    }
}
