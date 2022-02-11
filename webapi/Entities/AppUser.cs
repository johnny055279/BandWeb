using System.Collections.Generic;
namespace webapi.Entities;
using System;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser<int>
{
    public string Gender { get; set; }
    public string NickName { get; set; }
    public DateTime LastLoginTime { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
    public ICollection<PostComment> PostComments { get; set; }
    public ICollection<PostLike> PostLikes { get; set; }
    public ICollection<UserTicketOrder> TicketOrders { get; set; }
}