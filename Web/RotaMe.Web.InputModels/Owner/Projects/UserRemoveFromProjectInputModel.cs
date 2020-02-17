using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.InputModels.Owner.Projects
{
    public class UserRemoveFromProjectInputModel
    {
        public string Username { get; set; }

        public int ProjectId { get; set; }
    }
}
