using Aukce.Data;
using Microsoft.EntityFrameworkCore;

namespace auction.Data;

public class AuctionService
{

    public ApplicationDbContext DbContext;
    //public SignalR SignalR;
    public AuctionService(ApplicationDbContext dbContext)//, SignalR signalR)
    {
        DbContext = dbContext;
        //SignalR = signalR;
    }

    public List<Auction> GetAuctions()
    {
        return DbContext.Auctions.ToList();
    }

    /// <summary>
    /// Input category ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Supervisor only
    /// </summary>
    /// <param name="id"></param>
    public void AuthorizeAuction(int id)
    {
        var auct = GetAuction(id);
        auct.IsAuthorized = true;
        DbContext.SaveChanges();
    }

    public bool BidOnAuction(int auctionId, Bid bid, string clientEmail)
    {
        if (!DbContext.Clients.Any(x => x.Email == clientEmail)) return false;
        var client = DbContext.Clients.First(x => x.Email == clientEmail);
        var auction = DbContext.Auctions.Include(x => x.Bids).First(x => x.Id == auctionId);
        if (client.Money <= bid.Amount) return false;
        if (client.Money <= auction.MinBid && !auction.MinBidInPercentage) return false;
        if (auction.Bids.Count > 0 && client.Money <= auction.Bids.Last().Amount * (1 + auction.MinBid) && auction.MinBidInPercentage) return false;
        if(!auction.IsAuthorized) return false;
        DbContext.Auctions.Include(x=>x.Bids).First(x => x.Id == auctionId).Bids.Add(bid);
        client.Money -= bid.Amount;
        
        //SignalR.SendMessage(client.Email,$"Bid {bid.Amount} on {auction.Title}");
        
        DbContext.SaveChanges();
        return true;
    }

    public List<Auction> GetTop10Auctions()
    {
        return DbContext.Auctions.Include(x => x.Bids).OrderByDescending(x => x.Bids.Count).Take(10).ToList();
    }

    public List<Auction> GetUnAuthorizedAuctions()
    {
        return DbContext.Auctions.Include(x => x.Bids).Where(x => !x.IsAuthorized).ToList();
    }

    public List<Category> GetCategories()
    {
        return DbContext.Categories.ToList();
    }

    public Category GetCategory(string title)
    {
        return DbContext.Categories.First(x => x.Title == title);
    }

    public void EditCategory(Category category)
    {
        DbContext.Entry(DbContext.Categories.First(x => x.Id == category.Id)).CurrentValues.SetValues(category);
        DbContext.SaveChanges();
    }

    public Category GetCategory(int id)
    {
        return DbContext.Categories.First(x => x.Id == id);
    }

    public Auction GetAuction(int id)
    {
        return DbContext.Auctions.Include(x=> x.Bids).First(x => x.Id == id);
    }
}