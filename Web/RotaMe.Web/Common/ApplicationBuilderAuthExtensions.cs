﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RotaMe.Data;
using RotaMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaMe.Web.Common
{
    public static class ApplicationBuilderAuthExtensions
    {

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Name = "Owner",
                NormalizedName = "OWNER"
            },
        };

        private static readonly Gender[] genders =
        {
            new Gender
            {
                Name = "Male"
            },
            new Gender
            {
                Name = "Female"
            }
        };

        public static void SeedDatabase(
            this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<RotaMeDbContext>())
                {
                    context.Database.Migrate();

                    if (!context.Roles.Any())
                    {
                        context.Roles.AddRange(roles);
                        context.Genders.AddRange(genders);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
