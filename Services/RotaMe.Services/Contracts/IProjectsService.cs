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
        IQueryable<ProjectsListToAddUserServiceModel> GetAllProjectsToAddUser();
        IEnumerable<ProjectsListToRemoveUserServiceModel> GetAllProjectsToRemoveUser();
        Task<bool> ProjectRemoveUser(ProjectRemoveUserServiceModel projectRemoveUserServiceModel);

        Task<bool> AddUserToProject(UserAddToProjectServiceModel userAddToProjectServiceModel);
        Task<bool> Delete(int id);
    }
}
