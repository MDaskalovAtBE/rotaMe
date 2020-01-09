using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Data.Models
{
    public class UserProject
    {
        public string UserId { get; set; }
        public RotaMeUser User { get; set; }


        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
