﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxingClub.Web.Models
{
    public class TournamentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("Needs Med Examination?")]
        public bool IsMedCertificateNecessary { get; set; }

        public List<AgeCategoryViewModel> AgeCategories { get; set; } = new List<AgeCategoryViewModel>();

        public List<WeightCategoryViewModel> WeightCategories { get; set; } = new List<WeightCategoryViewModel>();

        public List<StudentFullViewModel> Students { get; set; } = new List<StudentFullViewModel>();
    }
}
