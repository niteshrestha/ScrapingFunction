namespace ScrapingFunction.Models
{
    public class Trace
    {
        public string RequestId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int StatusCode { get; set; }
        public string EncodedDataLength { get; set; } //Transfered over network
        public string DecodedBodyLength { get; set; } // Resource size
        public float StartTime { get; set; }
        public float FisnishTime { get; set; }
    }
}
