using System.ComponentModel;

namespace HttpClients.Models
{
    public class BoxingGroupLiteModel
    {
        public int Id { get; set; }

        [DisplayName("Group's Name")]
        public string Name { get; set; }

        [DisplayName("Coach")]
        public string CoachId { get; set; }

        public UserModel Coach { get; set; }

        [DisplayName("Full Name")]
        public string CoachFullName { get { return (Coach != null) ? $"{Coach.Surname} {Coach.Name} {Coach.Patronymic}" : string.Empty; } }
    }
}
