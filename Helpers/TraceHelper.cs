using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ScrapingFunction.Models;

namespace ScrapingFunction.Helpers
{
    public class TraceHelper
    {
        public IList<Trace> GetTraceData(string rawTrace)
        {
            var traceData = JsonSerializer.Deserialize<RawTrace>(rawTrace);

            IList<Trace> traces = new List<Trace>();

            foreach (var item in traceData.traceEvents)
            {
                if (item.name == "ResourceSendRequest")
                {
                    string[] resourceNameSplit = item.args.data.url.Split('/');
                    string name;

                    if (resourceNameSplit[^1] != "")
                    {
                        name = resourceNameSplit[^1];
                    }
                    else
                    {
                        name = resourceNameSplit[^2];
                    }

                    Trace trace = new Trace()
                    {
                        RequestId = item.args.data.requestId,
                        Url = item.args.data.url,
                        Name = name,
                        StartTime = item.ts
                    };

                    traces.Add(trace);
                }

                if (item.name == "ResourceReceiveResponse")
                {
                    string rId = item.args.data.requestId;

                    var edit = traces.Where(x => x.RequestId == rId).FirstOrDefault();

                    if (item.args.data.mimeType != null)
                    {
                        edit.Type = item.args.data.mimeType;
                    }

                    edit.StatusCode = item.args.data.statusCode;
                }
                if (item.name == "ResourceFinish")
                {
                    string rId = item.args.data.requestId;

                    var edit = traces.Where(x => x.RequestId == rId).FirstOrDefault();

                    edit.EncodedDataLength = new DataLengthHelper().GetDataSize(item.args.data.encodedDataLength);
                    edit.DecodedBodyLength = new DataLengthHelper().GetDataSize(item.args.data.decodedBodyLength);
                    edit.FisnishTime = item.ts;
                }
            }
            return traces;
        }
    }
}
