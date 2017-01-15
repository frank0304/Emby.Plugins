using LiveTv.Vdr.Configuration;
using System.Collections.Generic;

namespace LiveTv.Vdr.RestfulApi.Resources
{
    internal class EventsResource : IRootResource
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public List<EventResource> Events { get; set; }

        public string GetResourceName()
        {
            return Constants.ResourceName.Events;
        }
    }

    internal class EventResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Short_text { get; set; }
        public string Description { get; set; }
        public int Start_time { get; set; }
        public string Channel { get; set; }
        public string Channel_name { get; set; }
        public int Duration { get; set; }
        //"table_id":79,"version":31,
        public int Images { get; set; }
        public bool Timer_exists { get; set; }
        public bool Timer_active { get; set; }
        public int Timer_id { get; set; }
        //"parental_rating":0
        //"vps":1484380800
        //"components":[{"stream":5,"type":11,"language":"deu","description":"HD-Video"},{"stream":2,"type":3,"language":"deu","description":"stereo"},{"stream":4,"type":66,"language":"deu","description":"Dolby Digital 2.0"},{"stream":3,"type":1,"language":"deu","description":""},{"stream":2,"type":3,"language":"deu","description":"ohne Audiodeskription"},{"stream":3,"type":32,"language":"deu","description":"DVB-Untertitel"}]
        public List<string> Contents { get; set; }
        //"raw_contents":[64],"details":[],"additional_media":null
    }
}
