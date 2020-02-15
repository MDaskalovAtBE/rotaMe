using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaMe.Services.Contracts;
using RotaMe.Sevices.Models.Owner.Projects;
using RotaMe.Web.InputModels.Owner.Projects;

namespace RotaMe.Web.Areas.Owner.Controllers
{
    public class ProjectsController : OwnerController
    {
        private readonly IProjectsService projectsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly string imageFolder = "project-pictures";

        public ProjectsController(IProjectsService projectsService, ICloudinaryService cloudinaryService)
        {
            this.projectsService = projectsService;
            this.cloudinaryService = cloudinaryService;
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
    }
}