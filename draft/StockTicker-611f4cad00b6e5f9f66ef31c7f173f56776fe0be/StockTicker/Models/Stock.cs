namespace StockTicker.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public DateTime startDate { get; set; }
        public double Balance { get; set; }
        public double OpenPrice { get; set; }
        public int StockOwned { get; set; }

    }
}
