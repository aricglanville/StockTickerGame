using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockTicker.Interfaces;
using StockTicker.Data;
using StockTicker.Models;
using System;
using ChartDirector;
using System.Net;
using Newtonsoft.Json;
using StockTicker.Services;

namespace StockTicker.Pages
{


    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ApiClass apiCall;

        public DateTime startDate ;

        public List<string> listOfStrings = new List<string>();

        public string test;

        public double OpenPrice = 0.0;
        
        public double Balance = 10000.00;

        public int StockOwned = 0;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
  

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

        }

//***********Helper functions*************************

        //this function should be used to update the data shown on the charts 
        public string ProgressGameplay()
        {
            startDate = NextDay(startDate);
            //check if the day is today or in the future, if so we need to quit or it will break
            if (startDate >= DateTime.Now)
            {
                return QuitGame();
            }
            var dateTest = startDate.ToString("yyyy-MM-dd");
            //progress the game state
            apiCall = new ApiClass(test, dateTest);
            test = apiCall.stock_.Symbol.ToString();
            OpenPrice = apiCall.stock_.OpenPrice;
            OpenPrice = Math.Truncate(OpenPrice * 100) / 100;
            
            return "The price for {test} is ${OpenPrice}";

        }

        public DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = DateTime.Today.AddYears(-2);
            int range = (DateTime.Today.AddMonths(-6) - start).Days;
            DateTime randDay = start.AddDays(gen.Next(range));
            if (randDay.DayOfWeek == DayOfWeek.Saturday || randDay.DayOfWeek == DayOfWeek.Sunday)
            {
                randDay = randDay.AddDays(2);
            }
                return randDay;
        }

        public DateTime NextDay(DateTime start)
        {
            DateTime Day = start.AddDays(7);
            return Day;
        }

        //quit game completely
        public string QuitGame()
        {
            //sell all stock the user has and add the money to bank account
            double amount = StockOwned * OpenPrice;
            Balance += amount;

            //TODO: show ending results by returning a string to the Ajax handler
            return "Game Over! You ended with $" + Balance + "\n\r" + "That means your overall gain/loss was $" + (Balance-10000);
        }

        //************Ajax functions**********************

        public IActionResult OnPostAjaxGameStart(string val)
        {
            //Call api and assign response to Stock Object with selected ticker and date
            startDate = RandomDay();
            var dateTest = startDate.ToString("yyyy-MM-dd");
            apiCall = new ApiClass(val, dateTest);
            test = val.ToString();
            OpenPrice = apiCall.stock_.OpenPrice;
            OpenPrice = Math.Truncate(OpenPrice * 100) / 100;
            return new JsonResult($"The price for {test} is ${OpenPrice} - ");
        }


        public IActionResult OnPostAjaxBuy(string val)
        {
            int buyAmount = Convert.ToInt32(val);
            double cost = OpenPrice * buyAmount;
            //check to see if we have enough money
            if (cost > Balance)
            {
                return new JsonResult("You do not have enough money to buy " + val + " shares.");
            }
            
            //do stock and money algorithm
            Balance -= cost;
            StockOwned += buyAmount;
                       
            return new JsonResult("You bought " + val + " shares and spent  $" + cost + " \n\r" + ProgressGameplay());
        }

        public IActionResult OnPostAjaxSell(string val)
        {
            int sellAmount = Convert.ToInt32(val);

            //check to see if we even have enough stock to sell
            if (sellAmount > StockOwned)
            {
                return new JsonResult("You do not have enough stock to sell " + val + " shares.");
            }
            
            //do stock and money algorithm
            double money = OpenPrice * sellAmount;
            Balance += money;
            StockOwned -= sellAmount;

            return new JsonResult("You sold " + val + " shares and made $" + money + " \n\r" + ProgressGameplay());
        }

        public IActionResult OnPostAjaxHold()
        {
            //do nothing and progress the game



            return new JsonResult("You decided to hold. " + " \n\r" + ProgressGameplay());
        }

        public IActionResult OnPostAjaxQuit()
        {
            return new JsonResult(QuitGame()); 
        }

    }
}