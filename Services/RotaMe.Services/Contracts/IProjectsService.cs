using RotaMe.Sevices.Models.Owner.Projects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RotaMe.Services.Contracts
{
    public interface IProjectsService
    {
        Task<bool> Create(ProjectCreateServiceModel projectCreateServiceModel);
    }
}
