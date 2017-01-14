using MediaBrowser.Controller.LiveTv;
using System;

namespace LiveTv.Vdr.RestfulApi.Resources
{
    internal class ChannelResource
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string Channel_id { get; set; }
        public bool Image { get; set; }
        public string Group { get; set; }
        public int Transponder { get; set; }
        public string Stream { get; set; }
        public bool Is_atsc { get; set; }
        public bool Is_cable { get; set; }
        public bool Is_terr { get; set; }
        public bool Is_sat { get; set; }
        public bool Is_radio { get; set; }
        public int Index { get; set; }
    }
}