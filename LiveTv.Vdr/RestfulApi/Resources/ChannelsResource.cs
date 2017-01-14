using LiveTv.Vdr.Configuration;
using System.Collections.Generic;

namespace LiveTv.Vdr.RestfulApi.Resources
{
    internal class ChannelsResource : IRootResource
    {
        public List<ChannelResource> Channels { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }


        public string GetResourceName()
        {
            return Constants.ResourceName.Channels;
        }
    }
}
