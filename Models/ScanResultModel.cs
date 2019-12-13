using System.Collections.Generic;

namespace ScrapingFunction.Models
{
    public class ScanResultModel
    {
        public string Url { get; set; }
        public int ImageCount { get; set; }
        public int ScriptCount { get; set; }
        public int IFrameCount { get; set; }
        public int CssCount { get; set; }
        public string TotalImageSize { get; set; }
        public string TotalScriptSize { get; set; }
        public int TwitterEmbedCount { get; set; }
        public int FacebookEmbedCount { get; set; }
        public int YoutubeEmbedCount { get; set; }
        public IEnumerable<ImageModel> ImageModels { get; set; }
        public IEnumerable<Trace> Traces { get; set; }
    }
}
