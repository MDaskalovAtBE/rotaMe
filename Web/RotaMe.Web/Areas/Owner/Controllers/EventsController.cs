using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using RotaMe.Web.InputModels.Owner.Events;
using RotaMe.Web.ViewModels.Owner.Projects;

namespace RotaMe.Web.Areas.Owner.Controllers
{
    public class EventsController : OwnerController
    {
        private readonly IProjectsService projectsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IEventsService eventsService;
        private readonly string imageFolder = "event-pictures";

        public EventsController(IProjectsService projectsService, ICloudinaryService cloudinaryService, IEventsService eventsService)
        {
            this.projectsService = projectsService;
            this.cloudinaryService = cloudinaryService;
            this.eventsService = eventsService;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var ownerId = HttpContext.User.FindFirst("Id").Value;
            var projects = projectsService.GetAllOwnerProjectsToCreateEvent(ownerId).To<ProjectsListToCreateEventViewModel>().ToList();

            if (id != 0)
            {
                var project = projects.FirstOrDefault(p => p.Id == id);
                if (project != null)
                {
                    project.Selected = true;
                }
            }

            this.ViewData["projects"] = projects;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventCreateInputModel eventCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }


            string pictureUrl = null;

            if (eventCreateInputModel.Image != null)
            {
                pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                    eventCreateInputModel.Image,
                    eventCreateInputModel.Name,
                    imageFolder);
            }

            EventCreateServiceModel eventCreateServiceModel = new EventCreateServiceModel
            {
                Name = eventCreateInputModel.Name,
                ProjectId = eventCreateInputModel.ProjectId,
                Description = eventCreateInputModel.Description,
                Image = pictureUrl,
                Slug = eventCreateInputModel.Name.ToLower().Replace(" ", string.Empty),
            };


            var result = await this.eventsService.Create(eventCreateServiceModel);


            if (!result)
            {
                return this.Redirect("/owner/events/create");
            }

            return this.Redirect("/owner/projects/list");
        }
    }
}