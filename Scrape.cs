using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using ScrapingFunction.Helpers;
using ScrapingFunction.Models;

namespace ScrapingFunction
{
    public static class Scrape
    {
        [FunctionName("Scrape")]
        public static async Task<ScanResultModel> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Funtion triggered");
            string url = req.Query["url"];
            int height = int.Parse(req.Query["height"]);
            int width = int.Parse(req.Query["width"]);

            int imageCount = 0;
            int twitterEmbedCount = 0;
            int facebookEmbedCount = 0;
            int youtubeEmbedCount = 0;
            int scriptCount = 0;
            int iFrameCount = 0;
            int cssCount = 0;
            float totalImageContentLength = 0;

            string totalImageSizeString = "";
            string totalScriptSizeString = "";

            IList<ImageModel> imageProperties = new List<ImageModel>();
            ICollection<Trace> traces = new Collection<Trace>();
            if (!string.IsNullOrEmpty(url))
            {
                url = url.StartsWith("http") ? url : $"https://{url}";

                Browser browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    Args = new[] { "--no-sandbox" }
                });

                Page page = await browser.NewPageAsync();

                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = width,
                    Height = height
                });

                await page.Tracing.StartAsync(new TracingOptions
                {
                    Categories = new List<string> { "devtools.timeline" }
                });

                var client = await page.Target.CreateCDPSessionAsync();
                await client.SendAsync("Network.clearBrowserCache");

                await page.GoToAsync(url, timeout: 0);

                string rawTrace = await page.Tracing.StopAsync();

                string content = await page.GetContentAsync();

                //Image
                var imageElements = await page.QuerySelectorAllAsync("img");

                if (imageElements != null)
                {
                    imageCount = imageElements.Count();
                    imageProperties = await new ImageHelper().GetImageData(imageElements);
                }

                await browser.CloseAsync();

                //Trace
                traces = new TraceHelper().GetTraceData(rawTrace);

                Uri requestUri = new Uri(url);
                string host = requestUri.Host;

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);

                var iFrameNodes = doc.DocumentNode.SelectNodes("//iframe");
                var linkNodes = doc.DocumentNode.SelectNodes("//a");
                var scriptNodes = doc.DocumentNode.SelectNodes("//script/@src");
                var cssNodes = doc.DocumentNode.SelectNodes("//link[@rel=\"stylesheet\"]");
                var tweetNodes = doc.DocumentNode.SelectNodes("//blockquote[@class=\"twitter-tweet\"]");
                var fbRootNode = doc.DocumentNode.SelectSingleNode("//div[@id=\"fb-root\"]");

                //Get total size of images
                if (imageProperties != null)
                {
                    foreach (var imageProperty in imageProperties)
                    {
                        totalImageContentLength += imageProperty.ImageContentLength;
                    }

                    //Total image size in page
                    totalImageSizeString = new DataLengthHelper().GetDataSize(totalImageContentLength);
                }

                //Order images by descending order of their size
                imageProperties = imageProperties.OrderByDescending(o => o.ImageContentLength).ToList();

                //Scripts
                if (scriptNodes != null)
                {
                    scriptCount = scriptNodes.Count;
                    totalScriptSizeString = new ScriptHelper().GetTotalScriptSize(scriptNodes, host);
                }

                //CSS
                if (cssNodes != null)
                {
                    cssCount = cssNodes.Count();
                }

                //Twitter Embed
                if (linkNodes != null)
                {
                    foreach (var node in linkNodes)
                    {
                        var linkClass = node.GetAttributeValue("class", "");

                        if (linkClass.Contains("twitter-timeline") ||
                            linkClass.Contains("twitter-moment") ||
                            linkClass.Contains("twitter-hashtag-button") ||
                            linkClass.Contains("twitter-follow-button") ||
                            linkClass.Contains("twitter-mention-button"))
                        {
                            twitterEmbedCount++;
                        }
                    }
                }
                if (tweetNodes != null)
                {
                    foreach (var tweetNode in tweetNodes)
                    {
                        twitterEmbedCount++;
                    }
                }

                //iFrame
                if (iFrameNodes != null)
                {
                    iFrameCount = iFrameNodes.Count();
                    foreach (var iFrameNode in iFrameNodes)
                    {
                        if (iFrameNode.GetAttributeValue("src", "").Contains("facebook.com"))
                        {
                            facebookEmbedCount++;
                        }
                        if (iFrameNode.GetAttributeValue("src", "").Contains("youtube.com"))
                        {
                            youtubeEmbedCount++;
                        }
                    }
                }

                //Facebook Embed
                if (fbRootNode != null)
                {
                    var fbEmbedNodes = doc.DocumentNode.SelectNodes("//div");

                    foreach (var fbEmbedNode in fbEmbedNodes)
                    {
                        var divClass = fbEmbedNode.GetAttributeValue("class", "");
                        if (divClass == "fb-comments" ||
                            divClass == "fb-comment-embed" ||
                            divClass == "fb-post" ||
                            divClass == "fb-video" ||
                            divClass == "fb-like" ||
                            divClass == "fb-page" ||
                            divClass == "fb-save" ||
                            divClass == "fb-share-button")
                        {
                            facebookEmbedCount++;
                        }
                    }
                }
            }

            ScanResultModel scanResult = new ScanResultModel()
            {
                Url = url,
                ImageCount = imageCount,
                ScriptCount = scriptCount,
                IFrameCount = iFrameCount,
                CssCount = cssCount,
                TotalImageSize = totalImageSizeString,
                TotalScriptSize = totalScriptSizeString,
                TwitterEmbedCount = twitterEmbedCount,
                FacebookEmbedCount = facebookEmbedCount,
                YoutubeEmbedCount = youtubeEmbedCount,
                ImageModels = imageProperties,
                Traces = traces
            };

            return scanResult;
        }
    }
}
