using RotaMe.Sevices.Models.Owner.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services.Contracts
{
    public interface IProjectsService
    {
        Task<bool> Create(ProjectCreateServiceModel projectCreateServiceModel);
        Task<IEnumerable<ProjectsListServiceModel>> GetAllProjects();
        Task<IEnumerable<OwnerProjectsListServiceModel>> GetOwnerProjects(string ownerId);

        ProjectDetailsServiceModel GetOwnerProjectDetails(string ownerId, int projectId);

        Task<bool> Edit(ProjectEditServiceModel projectEditServiceModel);

        IQueryable<ProjectsListToAddUserServiceModel> GetAllOwnerProjectsToAddUser(string ownerId);

        IQueryable<ProjectsListToCreateEventServiceModel> GetAllOwnerProjectsToCreateEvent(string ownerId);

        IQueryable<ProjectsListToRemoveUserServiceModel> GetAllOwnerProjectsToRemoveUser(string ownerId);
        Task<bool> ProjectRemoveUser(ProjectRemoveUserServiceModel projectRemoveUserServiceModel);

        Task<bool> AddUserToProject(UserAddToProjectServiceModel userAddToProjectServiceModel);
        Task<bool> Delete(int id);
    }
}
