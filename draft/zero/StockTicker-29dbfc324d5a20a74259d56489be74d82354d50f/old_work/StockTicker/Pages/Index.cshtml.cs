using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockTicker.Models;
using StockTicker.Services;

namespace StockTicker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IGet _iGet;

        public List<Stock> tes;


        public IndexModel(ILogger<IndexModel> logger, IGet get)
        {
            _logger = logger;
            _iGet = get;
        }

        //public void OnGet()

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                grabtestAsync();
                return Page();//probably  not right
            }
            else
                return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public async Task grabtestAsync()
        {
            string acc = "CRkWeqjNN_sFLLWYRzuGkkRp_KVfaR9h";
            tes = (List<Stock>)await _iGet.GetData("BAC", acc);
           
        }
    }
}