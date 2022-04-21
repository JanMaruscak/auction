namespace Aukce.Data
{
    public class Bid
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public int AuctionId { get; set; }
        public int Amount { get; set; }
        public DateTime Time { get; set; }
    }
}
