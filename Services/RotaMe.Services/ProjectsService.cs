using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RotaMe.Data;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
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

        public async Task<IEnumerable<OwnerProjectsListServiceModel>> GetOwnerProjects(string ownerId)
        {
            return await context.Projects.Where(p => p.OwnerId == ownerId).Include(p => p.Users).To<OwnerProjectsListServiceModel>().ToListAsync();
        }

        public async Task<IEnumerable<ProjectsListServiceModel>> GetAllProjects()
        {

            return await context.Projects.Include(p => p.Users).To<ProjectsListServiceModel>().ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var project = await context.Projects.FindAsync(id);

            var result = context.Projects.Remove(project);

            context.SaveChanges();

            return false ? result == null : true;
        }

        public IQueryable<ProjectsListToAddUserServiceModel> GetAllProjectsToAddUser()
        {

            return context.Projects.To<ProjectsListToAddUserServiceModel>();
        }

        public async Task<bool> AddUserToProject(UserAddToProjectServiceModel userAddToProjectServiceModel)
        {
            var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == userAddToProjectServiceModel.ProjectId);

            if (project == null)
            {
                return false;
            }

            project.Users.Add(new UserProject()
            {
                UserId = userAddToProjectServiceModel.UserId,
                ProjectId = userAddToProjectServiceModel.ProjectId
            });

            await context.SaveChangesAsync();

            return true;
        }
    }
}
