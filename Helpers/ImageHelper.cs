using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using PuppeteerSharp;
using ScrapingFunction.Models;

namespace ScrapingFunction.Helpers
{
    public class ImageHelper
    {
        public async Task<IList<ImageModel>> GetImageData(ElementHandle[] imageElements)
        {
            IList<ImageModel> images = new List<ImageModel>();

            foreach (var imageElement in imageElements)
            {
                string imageName;
                int originalImageHeight = 0;
                int originalImageWidth = 0;
                int usedImageHeight = 0;
                int usedImageWidth = 0;
                string imageSize;
                float imageContentLength;

                var imgSrc = await imageElement.EvaluateFunctionAsync<string>("i=>i.src");
                string[] imgSrcSplit = imgSrc.Split('/');

                if (imgSrcSplit.Length > 0)
                {
                    imageName = imgSrcSplit[^1];
                }
                else
                {
                    imageName = imgSrc;
                }

                try
                {
                    var webRequest = HttpWebRequest.Create(imgSrc);
                    var webResponse = webRequest.GetResponse();
                    float contentLength = webResponse.ContentLength;

                    Stream stream = webResponse.GetResponseStream();

                    //TODO: Get Original Image dimension

                    usedImageHeight = int.Parse(await imageElement.EvaluateFunctionAsync<string>("i => i.height"));
                    usedImageWidth = int.Parse(await imageElement.EvaluateFunctionAsync<string>("i => i.width"));

                    //TODO:Evaluate original image size with used image size.

                    imageContentLength = contentLength;

                    imageSize = new DataLengthHelper().GetDataSize(contentLength);
                }
                catch (Exception)
                {
                    imageSize = "Couldn't determine image size";
                    imageContentLength = 0;
                }

                ImageModel image = new ImageModel()
                {
                    ImageUri = imgSrc,
                    ImageName = imageName,
                    UsedImageHeight = usedImageHeight,
                    UsedImageWidth = usedImageWidth,
                    OriginalImageHeight = originalImageHeight,
                    OriginalImageWidth = originalImageWidth,
                    ImageSize = imageSize,
                    ImageContentLength = imageContentLength
                };
                images.Add(image);
            }

            return images;
        }
    }
}
