using LiveTv.Vdr.Configuration;
using System;
using System.Collections.Generic;

namespace LiveTv.Vdr.RestfulApi.Resources
{
    internal class InfoResource :IRootResource
    {
        private DateTime _time;

        public string Version { get; set; }
        public long Time { get; set; }        
        public List<RestService> Services { get; set; }
        public string Channel { get; set; }
        public int Eventid { get; set; }
        public int Start_time { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }
        public DiskUsage Diskusage { get; set; }
        public VdrInfo Vdr { get; set; }

        public string GetResourceName()
        {
            return Constants.ResourceName.Info;
        }
    }

    internal class RestService
    {
        //TODO
    }

    internal class VdrInfo
    {
        public string Version { get; set; }
        public List<VdrPlugin> Plugins { get; set; }
        public List<TunerDevice> Devices { get; set; }
    }

    internal class VdrPlugin
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    internal class DiskUsage
    {
        public long Free_mb { get; set; }
        public int Used_percent { get; set; }
        public long Free_minutes { get; set; }
        public string Description_localized { get; set; }
    }

    internal class TunerDevice
    {
    }
}
