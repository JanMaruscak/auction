namespace Aukce.Data
{
    public class Auction
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Bid> Bids { get; set; }
        public Category Category { get; set; }
        public int HoursLength { get; set; }
        public int MinBid { get; set; }
        public bool MinBidInPercentage { get; set; }
        public string ImgUrl { get; set; }
    }
}
