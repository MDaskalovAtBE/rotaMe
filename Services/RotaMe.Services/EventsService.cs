using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Sevices.Models.Owner.Events;
using System;
using System.Collections.Generic;
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
    }
}
