using System.ComponentModel.DataAnnotations;

namespace wa_dev_coursework.Models.EventsContext
{
    public class Organization
    {
        // Properties
        [Key]
        public Guid OrganizationID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
