namespace StockTicker.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public DateTime From { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
    }
}
