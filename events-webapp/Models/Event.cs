using System.ComponentModel.DataAnnotations;

namespace wa_dev_coursework.Models.EventsContext
{
    public class Event
    {
        // Properties
        [Key]
        public Guid EventID { get; set; }
        [Required]
        public bool Archived { get; set; }
        [Required]
        public Guid AuthorID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public int? TicketCost { get; set; }
    }
}
