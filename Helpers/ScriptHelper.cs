using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace ScrapingFunction.Helpers
{
    public class ScriptHelper
    {
        public string GetTotalScriptSize(HtmlNodeCollection scriptNodes, string host)
        {
            IList<string> urls = new List<string>();
            float totalScriptContentLength = 0;
            foreach (var script in scriptNodes)
            {
                string scriptUrl = "";

                var scriptSrc = script.GetAttributeValue("src", "");

                if (scriptSrc.StartsWith("http"))
                {
                    scriptUrl = scriptSrc;
                }
                else
                {
                    if (scriptSrc.StartsWith('/'))
                    {
                        scriptUrl = $"https://{host}{scriptSrc}";
                    }
                    else
                    {
                        scriptUrl = $"https://{host}/{scriptSrc}";
                    }
                }

                try
                {
                    var webRequest = HttpWebRequest.Create(scriptUrl);
                    var webResponse = webRequest.GetResponse();
                    float contentLength = webResponse.ContentLength;

                    totalScriptContentLength += contentLength;

                    urls.Add(scriptUrl);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return new DataLengthHelper().GetDataSize(totalScriptContentLength);
        }
    }
}
