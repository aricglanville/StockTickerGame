using System.ComponentModel.DataAnnotations;

namespace StockTicker.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public int TransactionAmount { get; set; }
        public DateTime DateTime { get; set; }

    }
}
