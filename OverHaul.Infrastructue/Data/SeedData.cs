using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.Models;
using Overhaul.Application.Services;
using Overhaul.Domain.Entities;

namespace Overhaul.Infrastructure.Data;

public  class SeedData
{
    //public static async Task SeedData1(UserManager<AppUser> userManager , IRestaurantService _restaurantService )
    //{
    //   await SeedUsers(userManager);
    //   await SeedResturant(_restaurantService);
    //}
    private static async Task SeedUsers(UserManager<AppUser> userManager)
    {
        var appuser= new AppUser
        {
            UserName="09149003744",
            FirstName= "بابک",
            LastName="حاجعلی",
            LoginCode="0",
           // PersenelCode = "0",
            RowVersion= new byte[1] 
        };
        var user = await userManager.FindByNameAsync(appuser.UserName);
        if ( user is null )
        {
          var result =  await userManager.CreateAsync(appuser , "Aa123456!");
        }

         appuser = new AppUser
        {
            UserName = "09148905828",
            FirstName = "بابک",
            LastName = "حاتملو",
            LoginCode = "0",
            // PersenelCode = "0",
            RowVersion = new byte[1]
        };
         user = await userManager.FindByNameAsync(appuser.UserName);
        if (user is null)
        {
            var result = await userManager.CreateAsync(appuser, "Aa123456!");
        }

    }
    
}
