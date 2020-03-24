using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services
{
    public class EventsService : IEventsService
    {
        private readonly RotaMeDbContext context;

        public EventsService(RotaMeDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(EventCreateServiceModel eventCreateServiceModel)
        {
            var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == eventCreateServiceModel.ProjectId);

            if (project == null)
            {
                return false;
            }

            context.Events.Add(new Event() { 
                Name = eventCreateServiceModel.Name,
                Description = eventCreateServiceModel.Description,
                ProjectId = project.Id,
                Image = eventCreateServiceModel.Image,
                Slug = eventCreateServiceModel.Slug,
            });

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var eventFromDb = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventFromDb == null)
            {
                return false;
            }

            context.Events.Remove(eventFromDb);
            await context.SaveChangesAsync();

            return true;
        }

        public EventDetailsServiceModel GetEventDetails(int eventId)
        {
            return context.Events
                .Where(e => e.Id == eventId)
                .Include(e => e.EventNeeds)
                .Include(e => e.Availabilities)
                .Include(e => e.Users).ThenInclude(u => u.User)
                .To<EventDetailsServiceModel>()
                .FirstOrDefault(e => e.Id == eventId);
        }
        public async Task<bool> Edit(EventEditServiceModel eventEditServiceModel)
        {
            var @event = await context.Events.FirstOrDefaultAsync(p => p.Id == eventEditServiceModel.Id);

            if (@event == null)
            {
                return false;
            }

            @event.Name = eventEditServiceModel.Name;
            @event.Slug = eventEditServiceModel.Slug;
            @event.Description = eventEditServiceModel.Description;

            if (eventEditServiceModel.Image != null)
            {
                @event.Image = eventEditServiceModel.Image;
            }

            context.Events.Update(@event);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateNeed(EventNeedCreateServiceModel eventNeedCreateServiceModel)
        {
            var eventNeed = await context.EventNeeds.FirstOrDefaultAsync(
                e => e.MaximumUsers == eventNeedCreateServiceModel.MaximumUsers &&
                e.MinimalUsers == eventNeedCreateServiceModel.MinimalUsers &&
                e.EventId == eventNeedCreateServiceModel.EventId);

            if (eventNeed != null)
            {
                return false;
            }

            context.EventNeeds.Add(new EventNeed()
            {
                EventId = eventNeedCreateServiceModel.EventId,
                Date = DateTime.ParseExact(eventNeedCreateServiceModel.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                MaximumUsers = eventNeedCreateServiceModel.MaximumUsers,
                MinimalUsers = eventNeedCreateServiceModel.MinimalUsers
            });

            await context.SaveChangesAsync();

            return true;
        }
    }
}
