namespace StockTicker.Models
{

    public class Polygon
    {
        public string ticker { get; set; }
        public int queryCount { get; set; }
        public int resultsCount { get; set; }
        public bool adjusted { get; set; }
        public Result[] results { get; set; }
        public string status { get; set; }
        public string request_id { get; set; }
        public int count { get; set; }
    }

    public class Result
    {
        public int v { get; set; }
        public float vw { get; set; }
        public float o { get; set; }
        public float c { get; set; }
        public float h { get; set; }
        public float l { get; set; }
        public long t { get; set; }
        public int n { get; set; }
    }

}
