using Microsoft.Extensions.Logging;
using wa_dev_coursework.Models.EventsContext;

namespace wa_dev_coursework.Data
{
    public class EventsSeed
    {
        public static async Task SeedAsync(EventsContext context)
        {
            try
            {
                if (context.Events.Any() && !context.Database.EnsureCreated()) return;

                Event[] centers = new Event[]
                {
                    new Event{ EventID = Guid.NewGuid(), Archived = false, Date = DateTime.Now, Name = "Дюна", 
                               AuthorID = Guid.Parse(context.Users.First().Id), Description = "1"},
                    new Event{ EventID = Guid.NewGuid(), Archived = false, Date = DateTime.Now, Name = "Брат 2",
                               AuthorID = Guid.Parse(context.Users.First().Id), Description = "1"}
                };

                foreach (Event center in centers)
                    context.Events.Add(center);

                await context.SaveChangesAsync();

                Organization[] pets = new Organization[]
                {
                    new Organization { OrganizationID = Guid.NewGuid(), Name = "Лодзь", Address = "Лежневская ул., 120A, Иваново", Email = "test@test.ru" },
                };

                foreach (Organization pet in pets)
                    context.Organizations.Add(pet);

                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
