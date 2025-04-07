using overhaul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  overhaul.Application.Models.User
{
    public class AppUserBindingModel
    {
        [Required(ErrorMessage ="این فیلد الزامی میباشد")]
        public  string FirstName { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        public  string LastName { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DataType(DataType.EmailAddress)]
        public  string Email { get; set; }

        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public  string ConfirmPassword { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DataType(DataType.EmailAddress)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "این فیلد الزامی میباشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
    public static partial class Extensions
    {
        public static AppUser Map(this AppUserBindingModel model)
        {
            return new AppUser
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
        }
    }
}
