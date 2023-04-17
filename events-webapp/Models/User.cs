using Microsoft.AspNetCore.Identity;
namespace wa_dev_coursework.Models.EventsContext
{
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
