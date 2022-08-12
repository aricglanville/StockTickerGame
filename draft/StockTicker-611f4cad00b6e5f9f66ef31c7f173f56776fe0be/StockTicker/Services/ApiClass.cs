using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockTicker.Interfaces;
using StockTicker.Data;
using StockTicker.Models;
using System;

using ChartDirector;
using System.Net;
using Newtonsoft.Json;


namespace StockTicker.Services
{
    public class ApiClass
    {

        private int weekcounter = 0;
        private DateTime tempDate;

        public List<string> listOfStrings = new List<string>();

        public Stock stock_;

        public ApiClass(string tick,  string dat)
        {

            //Call api and assign response to Stock Object with selected ticker and date
            var ticker = tick;
            var date = dat;
            var apiRequest = $"https://api.polygon.io/v1/open-close/{ticker}/{date}?adjusted=true&apiKey=6TH_lUVoIIueeLAJwbCSPncDIEsGQG0d";
            WebRequest request = WebRequest.Create(apiRequest);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Stock DeserializedObject = JsonConvert.DeserializeObject<Stock>(responseFromServer);

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            stock_ = DeserializedObject;
        }
    }
}
