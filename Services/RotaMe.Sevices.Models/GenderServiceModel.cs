using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class GenderServiceModel : IMapFrom<Gender>
    {
        public string Name { get; set; }
    }
}
