using RotaMe.Data.Models;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Services.Contracts
{
    public interface IRegisterService
    {
        IQueryable<UserGenderServiceModel> GetAllGenders();

        Gender GetGender(string name);
    }
}
