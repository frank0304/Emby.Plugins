using LiveTv.Vdr.Configuration;
using System.Collections.Generic;

namespace LiveTv.Vdr.RestfulApi.Resources
{
    internal class TimersResource :IRootResource
    {
        public List<Timer> Timers { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }

        public string GetResourceName()
        {
            return Constants.ResourceName.Timers;
        }
    }

    internal class Timer
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public int Flags { get; set; }
        public int Start { get; set; }
        public string Start_timestamp { get; set; }
        public string Stop_timestamp { get; set; }
        public int Stop { get; set; }
        public int Priority { get; set; }
        public int Lifetime { get; set; }
        public int event_id { get; set; }
        public string Weekdays { get; set; }
        public string Day { get; set; }
        public string Channel { get; set; }
        public string Filename { get; set; }
        public string Channel_name { get; set; }
        public bool Is_pending { get; set; }
        public bool Is_recording { get; set; }
        public bool Is_active { get; set; }
        //aux
    }
}
