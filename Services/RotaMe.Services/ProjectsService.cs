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
        private readonly UserManager<RotaMeUser> userManager;

        public ProjectsService(RotaMeDbContext context, UserManager<RotaMeUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
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

        public IQueryable<ProjectsListToAddUserServiceModel> GetAllOwnerProjectsToAddUser(string ownerId)
        {

            return context.Projects.Where(p => p.OwnerId == ownerId).To<ProjectsListToAddUserServiceModel>();
        }

        public IQueryable<ProjectsListToCreateEventServiceModel> GetAllOwnerProjectsToCreateEvent(string ownerId)
        {
            return context.Projects.Where(p => p.OwnerId == ownerId).To<ProjectsListToCreateEventServiceModel>();
        }

        public IQueryable<ProjectsListToRemoveUserServiceModel> GetAllOwnerProjectsToRemoveUser(string ownerId)
        {
            return this.context.Projects.Where(p => p.OwnerId == ownerId).Include(p => p.Users).ThenInclude(u => u.User).To<ProjectsListToRemoveUserServiceModel>();

        }

        public async Task<bool> AddUserToProject(UserAddToProjectServiceModel userAddToProjectServiceModel)
        {
            var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == userAddToProjectServiceModel.ProjectId);

            if (project == null)
            {
                return false;
            }

            var userProject = await context.UserProjects.FirstOrDefaultAsync(up => 
                up.ProjectId == userAddToProjectServiceModel.ProjectId && 
                up.UserId == userAddToProjectServiceModel.UserId);

            if (userProject != null)
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

        public async Task<bool> ProjectRemoveUser(ProjectRemoveUserServiceModel projectRemoveUserServiceModel)
        {
            var user = await userManager.FindByNameAsync(projectRemoveUserServiceModel.Username);
            var projects = await context.Projects.FirstOrDefaultAsync(p => p.Id == projectRemoveUserServiceModel.ProjectId);

            var userProject = await context.UserProjects.FirstOrDefaultAsync(up => 
                up.ProjectId == projectRemoveUserServiceModel.ProjectId 
                && up.UserId == user.Id);

            projects.Users.Remove(userProject); 
            
            await context.SaveChangesAsync();

            return true;
        }

        public ProjectDetailsServiceModel GetOwnerProjectDetails(string ownerId, int projectId)
        {

            return context.Projects
                .Where(p => p.OwnerId == ownerId)
                .Include(p => p.Events)
                .Include(p => p.Events)
                .Include(p => p.Users).ThenInclude(u => u.User)
                .To<ProjectDetailsServiceModel>()
                .FirstOrDefault(p => p.Id == projectId);

        }

        public async Task<bool> Edit(ProjectEditServiceModel projectEditServiceModel)
        {
            var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == projectEditServiceModel.Id);

            if (project == null)
            {
                return false;
            }

            project.Title = projectEditServiceModel.Title;
            project.Slug = projectEditServiceModel.Slug;
            project.Description = projectEditServiceModel.Description;

            if (projectEditServiceModel.Image != null)
            {
                project.Image = projectEditServiceModel.Image;
            }

            context.Projects.Update(project);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
