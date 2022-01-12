using System.Collections.Generic;
namespace webapi.Entities;
using Microsoft.AspNetCore.Identity;
public class AppUser : IdentityUser<int>
{
    public string Gender { get; set; }
    public string NickName { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
}


