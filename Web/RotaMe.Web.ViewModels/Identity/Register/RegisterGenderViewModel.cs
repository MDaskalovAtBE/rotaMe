using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RotaMe.Web.ViewModels.Identity.Register
{
    public class RegisterGenderViewModel : IMapFrom<UserGenderServiceModel>
    {
        public string Name { get; set; }
    }
}
