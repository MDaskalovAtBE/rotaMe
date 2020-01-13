using RotaMe.Data.Models;
using RotaMe.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Sevices.Models
{
    public class UserGenderServiceModel : IMapFrom<Gender>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
