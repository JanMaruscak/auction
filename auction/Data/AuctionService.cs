using Aukce.Data;
using Microsoft.EntityFrameworkCore;

namespace auction.Data;

public class AuctionService
{

    public ApplicationDbContext DbContext;
    public AuctionService(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public List<Auction> GetAuctions()
    {
        return DbContext.Auctions.ToList();
    }

    public List<Auction> GetAuctions(int id)
    {
        return DbContext.Auctions.Include(x => x.Category).Where(x => x.Category.Id == id).ToList();
    }

    public void AddAuction(Auction auction)
    {
        DbContext.Auctions.Add(auction);
        DbContext.SaveChanges();
    }

    public void AddCategory(Category category)
    {
        DbContext.Categories.Add(category);
        DbContext.SaveChanges();
    }

    public void BidOnAuction(int auctionId, Bid bid)
    {
        DbContext.Auctions.Include(x=>x.Bids).First(x => x.Id == auctionId).Bids.Add(bid);
        DbContext.SaveChanges();
    }

    public List<Category> GetCategories()
    {
        return DbContext.Categories.ToList();
    }

    public Category GetCategory(string title)
    {
        return DbContext.Categories.First(x => x.Title == title);
    }

    public Auction GetAuction(int id)
    {
        return DbContext.Auctions.Include(x=> x.Bids).First(x => x.Id == id);
    }
}