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
using System.Globalization;

namespace StockTicker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        

        public ApiClass apiCall;

        private DateTime startDate;

        public List<string> listOfStrings = new List<string>();

        public string test;

        public double OpenPrice;

        public double Balance = 10000.00;

        public int StockOwned = 0;

        public IActionResult OnGet()
        {
            return Page();
        }

        public DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = DateTime.Today.AddYears(-2);
            int range = (DateTime.Today.AddMonths(-12) - start).Days;
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

        //************Ajax functions**********************

        public IActionResult OnPostAjaxGameStart(string val)
        {
            //Call api and assign response to Stock Object with selected ticker and date
            startDate = RandomDay();
            var dateTest = startDate.ToString("yyyy-MM-dd");
            apiCall = new ApiClass(val, dateTest);
            test = apiCall.stock_.Symbol.ToString();
            OpenPrice = apiCall.stock_.Open;
            OpenPrice = Math.Truncate(OpenPrice * 100) / 100;

            return new JsonResult($"The price for {test} is ${OpenPrice} - {startDate} ");
        }


        public IActionResult OnPostAjaxAccept(string val, string valTwo)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(valTwo, culture);

            tempDate = NextDay(tempDate);

            var dateTest = tempDate.ToString("yyyy-MM-dd");
            apiCall = new ApiClass(val, dateTest);
            test = apiCall.stock_.Symbol.ToString();
            OpenPrice = apiCall.stock_.Open;
            OpenPrice = Math.Truncate(OpenPrice * 100) / 100;

            return new JsonResult($"The price for {test} is ${OpenPrice} - {tempDate} ");
        }

    }
}