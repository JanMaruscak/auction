using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Aukce.Data;

namespace auction.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Bid> Bids { get; set; }
}