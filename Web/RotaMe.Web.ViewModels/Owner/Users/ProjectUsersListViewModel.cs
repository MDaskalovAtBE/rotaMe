using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models.Owner.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Owner.Users
{
    public class ProjectUsersListViewModel : IMapFrom<ProjectUsersListServiceModel>
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime LastLoggedIn { get; set; }
    }
}
