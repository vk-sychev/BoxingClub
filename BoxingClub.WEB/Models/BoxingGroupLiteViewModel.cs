using System.ComponentModel;

namespace BoxingClub.WEB.Models
{
    public class BoxingGroupLiteViewModel
    {
        public int Id { get; set; }

        [DisplayName("Group's Name")]
        public string Name { get; set; }

        public UserViewModel Coach { get; set; }
    }
}
