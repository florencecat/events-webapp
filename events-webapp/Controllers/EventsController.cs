using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wa_dev_coursework.Models.EventsContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace wa_dev_coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventsContext eventsContext;

        public EventsController(EventsContext eventsContext)
        {
            this.eventsContext = eventsContext;
        }

        // GET: api/<EventsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent() => await eventsContext.Events.ToListAsync();//.Include(e => e.Author).ToListAsync();

        // GET api/<EventsController>/XXXXXXXX-XXXX-XXXX-XXXXXXXXX
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(Guid id)
        {
            Event? @event = await eventsContext.Events.FindAsync(id);

            if (@event == null) return NotFound();
            else return @event;
        }

        // POST api/<EventsController>
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent([FromBody] Event newEvent)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            newEvent.EventID = Guid.NewGuid();
            eventsContext.Add(newEvent);

            await eventsContext.SaveChangesAsync();

            return CreatedAtAction("PostEvent", new { id = newEvent.EventID }, newEvent);
        }

        // PUT api/<EventsController>/XXXXXXXX-XXXX-XXXX-XXXXXXXXX
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCenter(Guid id, Event @event)
        {
            if (id != @event.EventID) return BadRequest();

            eventsContext.Entry(@event).State = EntityState.Modified;

            try
            {
                await eventsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eventsContext.Events.Any(e => e.EventID == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE api/<EventsController>/XXXXXXXX-XXXX-XXXX-XXXXXXXXX
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenter(Guid id)
        {
            Event? center = await eventsContext.Events.FindAsync(id);

            if (center == null) return NotFound();

            eventsContext.Events.Remove(center);
            await eventsContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
