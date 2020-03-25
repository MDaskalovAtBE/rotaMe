using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Events;
using RotaMe.Web.InputModels.Owner.Events;
using RotaMe.Web.ViewModels.Owner.Events;
using RotaMe.Web.ViewModels.Owner.Projects;
using RotaMe.Web.ViewModels.Owner.Users;

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var isEventDeleted = await eventsService.Delete(id);

            if (isEventDeleted)
            {
                return this.StatusCode(200);
            }

            return this.StatusCode(400);
        }

        [HttpPost]
        public async Task<IActionResult> NeedDelete(int id)
        {

            var isEventNeedDeleted = await eventsService.NeedDelete(id);

            if (isEventNeedDeleted)
            {
                return this.StatusCode(200);
            }

            return this.StatusCode(400);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var eventFromDb = eventsService.GetEventDetails(id);

            var eventViewModel = new EventDetailsViewModel()
            {
                Id = eventFromDb.Id,
                Description = eventFromDb.Description,
                Image = eventFromDb.Image,
                Name = eventFromDb.Name,
                ProjectId = eventFromDb.ProjectId,
                EventNeeds = eventFromDb.EventNeeds.Select(en => new EventEventNeedsListViewModel()
                {
                    Id = en.Id,
                    EventId = en.EventId,
                    Date = en.Date,
                    MaximumUsers = en.MaximumUsers,
                    MinimalUsers = en.MinimalUsers,
                }).ToList(),
                Availabilities = eventFromDb.Availabilities.Select(a => new EventAvailabilitiesListViewModel()
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    EventId = a.EventId,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Assigned = a.Assigned
                }).ToList(),
                Users = eventFromDb.Users.Select(u => new EventUsersListViewModel()
                {
                    Id = u.Id,
                    Avatar = u.Avatar,
                    FullName = u.FullName,
                    Username = u.Username,
                    LastLoggedIn = u.LastLoggedIn,
                }).ToList(),
            };


            this.ViewData["eventDetails"] = eventViewModel;
            this.ViewData["eventId"] = eventViewModel.Id;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id, EventEditInputModel eventEditInputModel)
        {
            string pictureUrl = null;

            if (eventEditInputModel.Image != null)
            {
                pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                    eventEditInputModel.Image,
                    eventEditInputModel.Name,
                    imageFolder);
            }

            var eventEditServiceModel = new EventEditServiceModel()
            {
                Id = id,
                Name = eventEditInputModel.Name,
                Slug = eventEditInputModel.Name.ToLower().Replace(" ", string.Empty),
                Description = eventEditInputModel.Description,
                Image = pictureUrl
            };

            var result = await eventsService.Edit(eventEditServiceModel);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> NeedEdit(int id, EventNeedEditInputModel eventNeedEditInputModel)
        {
            var eventNeedEditServiceModel = new EventNeedEditServiceModel()
            {
                Id = eventNeedEditInputModel.NeedId,
                Date = eventNeedEditInputModel.NeedDate,
                MinimalUsers = eventNeedEditInputModel.MinUsers,
                MaximumUsers = eventNeedEditInputModel.MaxUsers
            };

            var result = await eventsService.NeedEdit(eventNeedEditServiceModel);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateNeed(EventNeedCreateInputModel eventNeedCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var eventNeed = new EventNeedCreateServiceModel()
            {
                EventId = eventNeedCreateInputModel.EventId,
                Date = eventNeedCreateInputModel.Date,
                MaximumUsers = eventNeedCreateInputModel.MaximumUsers,
                MinimalUsers = eventNeedCreateInputModel.MinimalUsers
            };


            var result = await this.eventsService.CreateNeed(eventNeed);


            return this.Redirect($"/owner/events/details/{eventNeed.EventId}");
        }
    }
}