using MediaBrowser.Model.Plugins;

namespace LiveTv.Vdr.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string VDR_ServerName { get; set; }
        public int VDR_HTTP_Port { get; set; }
        public int VDR_RestfulApi_Port { get; set; }
        public string VDR_RestfulApi_BaseUrl { get; set; }
        public string VDR_HttpStream_BaseUrl { get; set; }

        public PluginConfiguration()
        {
            VDR_ServerName = "192.168.0.2";
            VDR_HTTP_Port = 3000;
            VDR_RestfulApi_Port = 8002;
            VDR_RestfulApi_BaseUrl = "http://" + VDR_ServerName + ":" + VDR_RestfulApi_Port;
            VDR_HttpStream_BaseUrl = "http://" + VDR_ServerName + ":" + VDR_HTTP_Port;
        }
    }
}
