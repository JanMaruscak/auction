using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace auction.Data;

public class UserService
{
    public ApplicationDbContext DbContext;
    public UserManager<IdentityUser> UserManager;
    public RoleManager<IdentityRole> RoleManager;
    public UserService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        DbContext = dbContext;
        UserManager = userManager;
        RoleManager = roleManager;
        //AddBaseRoles();
    }

    public void AddBaseRoles()
    {
        RoleManager.CreateAsync(new IdentityRole("Ucetni"));
        RoleManager.CreateAsync(new IdentityRole("Admin"));
        RoleManager.CreateAsync(new IdentityRole("Supervizor"));
    }
}