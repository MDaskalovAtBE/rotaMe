using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Sevices.Models.Owner.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly RotaMeDbContext context;

        public ProjectsService(RotaMeDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(ProjectCreateServiceModel projectCreateServiceModel)
        {
            var projects = await this.context.Projects.ToListAsync();
            var project = projects.FirstOrDefault(p => p.Title == projectCreateServiceModel.Title);
            if (project == null)
            {
                var result = await context.Projects.AddAsync(new Project()
                {
                    Title = projectCreateServiceModel.Title,
                    Description = projectCreateServiceModel.Description,
                    Image = projectCreateServiceModel.Image,
                    Slug = projectCreateServiceModel.Slug,
                    OwnerId = projectCreateServiceModel.OwnerId
                });

                context.SaveChanges();

                return false ? result == null : true;
            }

            return false;
        }
    }
}
