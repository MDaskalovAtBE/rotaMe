using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Projects;
using RotaMe.Web.InputModels.Owner.Projects;
using RotaMe.Web.ViewModels.Owner.Events;
using RotaMe.Web.ViewModels.Owner.Projects;
using RotaMe.Web.ViewModels.Owner.Users;

namespace RotaMe.Web.Areas.Owner.Controllers
{
    public class ProjectsController : OwnerController
    {
        private readonly IProjectsService projectsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly string imageFolder = "project-pictures";
        private readonly IUsersService usersService;

        public ProjectsController(IProjectsService projectsService, ICloudinaryService cloudinaryService, IUsersService usersService)
        {
            this.projectsService = projectsService;
            this.cloudinaryService = cloudinaryService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateInputModel projectCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var ownerId = HttpContext.User.FindFirst("Id").Value;

            string pictureUrl = null;

            if (projectCreateInputModel.Image != null)
            {
                pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                    projectCreateInputModel.Image,
                    projectCreateInputModel.Title,
                    imageFolder);
            }

            ProjectCreateServiceModel projectCreateServiceModel = new ProjectCreateServiceModel
            {
                Title = projectCreateInputModel.Title,
                Description = projectCreateInputModel.Description,
                OwnerId = ownerId,
                Slug = projectCreateInputModel.Title.ToLower().Replace(" ", string.Empty),
                Image = pictureUrl
            };


            var result = await this.projectsService.Create(projectCreateServiceModel);


            if (!result)
            {
                return this.View();
            }

            return this.Redirect("/owner/projects/list");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var ownerId = HttpContext.User.FindFirst("Id").Value;

            var projectsFromService = await projectsService.GetOwnerProjects(ownerId);

            var counter = 1;

            var projectslistViewModel = projectsFromService.Select(p => new ProjectListViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description.Substring(0, 50) + "...",
                Image = p.Image,
                Slug = p.Slug,
                UsersCount = p.UsersCount,
                EvenOdd = counter++ % 2 == 0 ? "even" : "odd" 
            });

            return View(projectslistViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var isProjectDeleted = await projectsService.Delete(id);

            if (isProjectDeleted)
            {
                return this.StatusCode(200);
            }

            return this.StatusCode(400);
        }

        [HttpGet]
        public IActionResult AddUser(int id)
        {
            var ownerId = HttpContext.User.FindFirst("Id").Value;
            var users = usersService.GetAllUsersToAdd().To<UsersListToAddToProjectViewModel>().ToList();
            var projects = projectsService.GetAllOwnerProjectsToAddUser(ownerId).To<ProjectsListToAddUserViewModel>().ToList();


            if (id != 0)
            {
                var project = projects.FirstOrDefault(p => p.Id == id);
                if (project != null)
                {
                    project.Selected = true;
                }
            }

            this.ViewData["users"] = users;
            this.ViewData["projects"] = projects;

            return this.View();
        }

        [HttpGet]
        public IActionResult RemoveUser(int id)
        {
            var ownerId = HttpContext.User.FindFirst("Id").Value;
            var projectsServiceModel = projectsService.GetAllOwnerProjectsToRemoveUser(ownerId).ToList();

            var projects = projectsServiceModel.Select(p => new ProjectsListToRemoveUserViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Users = JsonConvert.SerializeObject(p.Users)
            }).ToList();

            if (id != 0)
            {
                var project = projects.FirstOrDefault(p => p.Id == id);
                if (project != null)
                {
                    project.Selected = true;
                }
            }

            this.ViewData["projects"] = projects;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddToProjectInputModel userAddToProjectInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userAddToProjectServiceModel = new UserAddToProjectServiceModel()
            {
                UserId = userAddToProjectInputModel.UserId,
                ProjectId = userAddToProjectInputModel.ProjectId
            };

            var result = await projectsService.AddUserToProject(userAddToProjectServiceModel);

            if (!result)
            {
                return this.Redirect("/owner/projects/addUser");
            }

            return this.Redirect("/owner/projects/list");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(UserRemoveFromProjectInputModel userRemoveFromProjectInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var projectRemoveUserServiceModel = new ProjectRemoveUserServiceModel()
            {
                ProjectId = userRemoveFromProjectInputModel.ProjectId,
                Username = userRemoveFromProjectInputModel.Username
            };

            var result = await projectsService.ProjectRemoveUser(projectRemoveUserServiceModel);

            if (!result)
            {
                return this.View();
            }

            return this.Redirect("/owner/projects/list");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var ownerId = HttpContext.User.FindFirst("Id").Value;
            var project = projectsService.GetOwnerProjectDetails(ownerId, id);
            var projectsViewModel = new ProjectDetailsViewModel() 
            { 
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Image = project.Image,
                Events = project.Events.Select(e => new ProjectEventsListViewModel() 
                { 
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Image = e.Image,
                    Slug = e.Slug,
                    Availabilities = e.Availabilities,
                    EventNeeds = e.EventNeeds,
                    Users = e.Users
                }).ToList(),
                Users = project.Users.Select(u => new ProjectUsersListViewModel() 
                { 
                    Id = u.Id,
                    Avatar = u.Avatar,
                    Username = u.Username,
                    FullName = u.FullName,
                    LastLoggedIn = u.LastLoggedIn
                }).ToList()
            };


            this.ViewData["projectDetails"] = projectsViewModel;

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id, ProjectEditInputModel projectEditInputModel)
        {
            string pictureUrl = null;

            if (projectEditInputModel.Image != null)
            {
                pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                    projectEditInputModel.Image,
                    projectEditInputModel.Title,
                    imageFolder);
            }

            var projectEditServiceModel = new ProjectEditServiceModel()
            {
                Id = id,
                Title = projectEditInputModel.Title,
                Slug = projectEditInputModel.Title.ToLower().Replace(" ", string.Empty),
                Description = projectEditInputModel.Description,
                Image = pictureUrl
            };

            var result = await projectsService.Edit(projectEditServiceModel);

            return this.RedirectToAction("Details", new { id = id });
        }
    }
}