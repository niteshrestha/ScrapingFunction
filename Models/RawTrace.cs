namespace ScrapingFunction.Models
{
#pragma warning disable IDE1006 // Naming Styles
    public class RawTrace
    {
        public Traceevent[] traceEvents { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public int chromebitness { get; set; }
        public string clockdomain { get; set; }
        public string command_line { get; set; }
        public string cpubrand { get; set; }
        public int cpufamily { get; set; }
        public int cpumodel { get; set; }
        public int cpustepping { get; set; }
        public int gpudevid { get; set; }
        public string gpudriver { get; set; }
        public string gpupsver { get; set; }
        public int gpuvenid { get; set; }
        public string gpuvsver { get; set; }
        public bool highresticks { get; set; }
        public Json_Exporter_Stats json_exporter_stats { get; set; }
        public string networktype { get; set; }
        public int numcpus { get; set; }
        public string osarch { get; set; }
        public string osname { get; set; }
        public string ossession { get; set; }
        public string osversion { get; set; }
        public string oswow64 { get; set; }
        public Perfetto_Trace_Stats perfetto_trace_stats { get; set; }
        public int physicalmemory { get; set; }
        public string productversion { get; set; }
        public string tracecapturedatetime { get; set; }
        public string traceconfig { get; set; }
        public string useragent { get; set; }
        public string v8version { get; set; }
    }

    public class Json_Exporter_Stats
    {
        public int incremental_state_resets { get; set; }
        public int packets_dropped_invalid_incremental_state { get; set; }
        public int packets_with_previous_packet_dropped { get; set; }
        public int sequences_seen { get; set; }
    }

    public class Perfetto_Trace_Stats
    {
        public Buffer_Stats[] buffer_stats { get; set; }
        public int chunks_discarded { get; set; }
        public int data_sources_registered { get; set; }
        public int data_sources_seen { get; set; }
        public int patches_discarded { get; set; }
        public int producers_connected { get; set; }
        public int producers_seen { get; set; }
        public int total_buffers { get; set; }
        public int tracing_sessions { get; set; }
    }

    public class Buffer_Stats
    {
        public int abi_violations { get; set; }
        public int buffer_size { get; set; }
        public int bytes_overwritten { get; set; }
        public int bytes_read { get; set; }
        public int bytes_written { get; set; }
        public int chunks_committed_out_of_order { get; set; }
        public int chunks_discarded { get; set; }
        public int chunks_overwritten { get; set; }
        public int chunks_read { get; set; }
        public int chunks_rewritten { get; set; }
        public int chunks_written { get; set; }
        public int padding_bytes_cleared { get; set; }
        public int padding_bytes_written { get; set; }
        public int patches_failed { get; set; }
        public int patches_succeeded { get; set; }
        public int readaheads_failed { get; set; }
        public int readaheads_succeeded { get; set; }
        public int trace_writer_packet_loss { get; set; }
        public int write_wrap_count { get; set; }
    }

    public class Traceevent
    {
        public int pid { get; set; }
        public int tid { get; set; }
        public long ts { get; set; }
        public string ph { get; set; }
        public string cat { get; set; }
        public string name { get; set; }
        public string s { get; set; }
        public Args args { get; set; }
        public int tts { get; set; }
        public int dur { get; set; }
        public int tdur { get; set; }
        public string id { get; set; }
        public string bind_id { get; set; }
        public bool flow_in { get; set; }
        public bool flow_out { get; set; }
    }

    public class Args
    {
        public Data data { get; set; }
        public int number { get; set; }
        public int sort_index { get; set; }
        public string name { get; set; }
        public int uptime { get; set; }
        public string frame { get; set; }
        public string fileName { get; set; }
        public Begindata beginData { get; set; }
        public Enddata endData { get; set; }
        public int elementCount { get; set; }
        public int usedHeapSizeBefore { get; set; }
        public int usedHeapSizeAfter { get; set; }
        public int afterUserInput { get; set; }
        public string type { get; set; }
        public string labels { get; set; }
    }

    public class Data
    {
        public string requestId { get; set; }
        public string type { get; set; }
        public string frame { get; set; }
        public string url { get; set; }
        public string requestMethod { get; set; }
        public string priority { get; set; }
        public int statusCode { get; set; }
        public string mimeType { get; set; }
        public float encodedDataLength { get; set; }
        public bool fromCache { get; set; }
        public bool fromServiceWorker { get; set; }
        public Timing timing { get; set; }
        public bool isMainFrame { get; set; }
        public string page { get; set; }
        public string name { get; set; }
        public int lineNumber { get; set; }
        public int columnNumber { get; set; }
        public bool streamed { get; set; }
        public string notStreamedReason { get; set; }
        public bool didFail { get; set; }
        public float decodedBodyLength { get; set; }
        public float finishTime { get; set; }
        public string styleSheetUrl { get; set; }
        public float[] clip { get; set; }
        public int nodeId { get; set; }
        public int layerId { get; set; }
        public string navigationId { get; set; }
        public int size { get; set; }
        public int candidateIndex { get; set; }
        public int timerId { get; set; }
        public float timeout { get; set; }
        public bool singleShot { get; set; }
        public string parent { get; set; }
        public string functionName { get; set; }
        public string scriptId { get; set; }
        public object id { get; set; }
        public string state { get; set; }
        public string nodeName { get; set; }
        public int producedCacheSize { get; set; }
        public int readyState { get; set; }
        public float allottedMilliseconds { get; set; }
        public bool timedOut { get; set; }
        public string cacheConsumeOptions { get; set; }
        public int consumedCacheSize { get; set; }
        public bool cacheRejected { get; set; }
    }

    public class Timing
    {
        public float requestTime { get; set; }
        public float proxyStart { get; set; }
        public float proxyEnd { get; set; }
        public float dnsStart { get; set; }
        public float dnsEnd { get; set; }
        public float connectStart { get; set; }
        public float connectEnd { get; set; }
        public float sslStart { get; set; }
        public float sslEnd { get; set; }
        public float workerStart { get; set; }
        public float workerReady { get; set; }
        public float sendStart { get; set; }
        public float sendEnd { get; set; }
        public float receiveHeadersEnd { get; set; }
        public float pushStart { get; set; }
        public float pushEnd { get; set; }
    }

    public class Begindata
    {
        public int startLine { get; set; }
        public string frame { get; set; }
        public string url { get; set; }
        public int dirtyObjects { get; set; }
        public int totalObjects { get; set; }
        public bool partialLayout { get; set; }
    }

    public class Enddata
    {
        public int endLine { get; set; }
        public float[] root { get; set; }
        public int rootNode { get; set; }
        public string state { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
