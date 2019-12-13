namespace ScrapingFunction.Models
{
    public class ImageModel
    {
        public string ImageUri { get; set; }
        public string ImageName { get; set; }
        public int UsedImageHeight { get; set; }
        public int UsedImageWidth { get; set; }
        public int OriginalImageHeight { get; set; }
        public int OriginalImageWidth { get; set; }
        public string ImageSize { get; set; }
        public float ImageContentLength { get; set; }
    }
}
