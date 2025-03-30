using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Domain.Entities;

namespace Overhaul.Application.Models;

public class ProfileModel
{
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    public string UserId { get; set; }
    [Display(Name ="نام")]
    public string FirstName { get; set; }
    [Display(Name ="نام خانوادگی")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    public IFormFile ProfilePic { get; set; }
}
public class ProfileUpdateModel
{
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    public Guid UserId { get; set; }
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    public string ProfilePic { get; set; }
}

public static partial class Extensions
{
    public static AppUser Map(this ProfileUpdateModel model)
    {
        return new AppUser
        {
            
            //Id = model.UserId,
            ProfilePicturePath = model.ProfilePic
        };
    }
}
