﻿using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models.Owner.Projects
{
    public class ProjectsListToAddUserServiceModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
