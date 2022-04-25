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
        //AddRole("admin@admin.cz","Admin");
        //AddRole("supervisor@supervisor.cz", "Supervizor");
        //AddRole("accountant@accountant.cz", "Ucetni");
    }

    public async void AddRole(string email, string role)
    {
        var user = await UserManager.FindByEmailAsync(email);
        await UserManager.AddToRoleAsync(user, role);
    }

    public void AddBaseRoles()
    {
        RoleManager.CreateAsync(new IdentityRole("Ucetni"));
        RoleManager.CreateAsync(new IdentityRole("Admin"));
        RoleManager.CreateAsync(new IdentityRole("Supervizor"));
    }

    public List<Client> GetClients()
    {
        return DbContext.Clients.ToList();
    }

    /// <summary>
    /// Accountant only
    /// </summary>
    /// <param name="client"></param>
    /// <param name="amount"></param>
    public void AddMoney(Client client, int amount)
    {
        if(!DbContext.Clients.Any(x=>x.Id == client.Id))
        {
            throw new Exception("Client not found. No money added.");
        }
        else
        {
            var clientToUpdate = GetClient(client.Email);
            clientToUpdate.Money += amount;
        }

        DbContext.SaveChanges();
    }

    public Client GetClient(string email)
    {
        if (!DbContext.Clients.Any(x => x.Email == email))
        {
            throw new Exception("Client not found.");
        }
        return DbContext.Clients.First(x=>x.Email == email);
    }

    public void AddClient(string email)
    {
        var client = new Client() { Email = email, Money = 0 };
        if (DbContext.Clients.Any(x => x.Email == email)) throw new Exception("Trying to add existing client.");
        DbContext.Clients.Add(client);
        DbContext.SaveChanges();
    }
}