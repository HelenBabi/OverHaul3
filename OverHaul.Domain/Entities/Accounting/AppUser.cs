using Microsoft.AspNetCore.Identity;
using Overhaul.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Overhaul.Domain.Entities;

public class AppUser : IdentityUser, IEntity 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[]? RowVersion { get; set; }
    public string LoginCode { get; set; }
    public string? ProfilePicturePath { get; set; }
    public int AppLevelId { get; set; }
    public bool IsAdmin { get; set; }
    public bool SendPass { get; set; } = true;


}
