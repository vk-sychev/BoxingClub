using System.Collections.Generic;
using System.ComponentModel;

namespace BoxingClub.WEB.Models
{
    public class BoxingGroupFullViewModel
    {
        public int Id { get; set; }

        [DisplayName("Group's Name")]
        public string Name { get; set; }

        [DisplayName("Coach")]
        public string CoachId { get; set; }

        public UserViewModel Coach { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return (Coach!=null)? $"{Coach.Surname} {Coach.Name} {Coach.Patronymic}" : ""; } }

        public List<StudentLiteViewModel> Students { get; set; } = new List<StudentLiteViewModel>();
    }
}
