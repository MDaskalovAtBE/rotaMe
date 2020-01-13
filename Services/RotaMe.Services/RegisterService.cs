using RotaMe.Data;
using RotaMe.Data.Models;
using RotaMe.Services.Contracts;
using RotaMe.Services.Mapping;
using RotaMe.Sevices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotaMe.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly RotaMeDbContext context;

        public RegisterService(RotaMeDbContext context)
        {
            this.context = context;
        }

        public IQueryable<UserGenderServiceModel> GetAllGenders()
        {
            var genders = this.context.Genders;

            return genders.To<UserGenderServiceModel>();
        }

        public Gender GetGender(string name)
        {
            var gender = this.context.Genders.SingleOrDefault(gender => gender.Name == name);

            return gender;
        }
    }
}
