using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StockTicker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }

        public string StockName { get; set; }
        
        [Precision(18, 2)]
        public decimal Money { get; set; }

        public DateTime LastDate { get; set; }


    }
}
