using StockTicker.Models;
using System.Collections;

namespace StockTicker.Services
{
    public interface IGet
    {
        Task<IEnumerable<Stock>> GetData(string ticker, string accTok);
    }
}
