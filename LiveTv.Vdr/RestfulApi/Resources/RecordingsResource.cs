using LiveTv.Vdr.Configuration;
using System.Collections.Generic;

namespace LiveTv.Vdr.RestfulApi.Resources
{
    internal class RecordingsResource : IRootResource
    {
        public List<RecordingResource> Recordings { get; set; }

        public string GetResourceName()
        {
            return Constants.ResourceName.Recordings;
        }
    }

    internal class RecordingResource
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string File_name { get; set; }
        public string Relative_file_name { get; set; }
        public string Inode { get; set; }
        public bool Is_new { get; set; }
        public bool Is_edited { get; set; }
        public bool Is_pes_recording { get; set; }
        public int Duration { get; set; }
        public int Filesize_mb { get; set; }
        public string Channel_id { get; set; }
        public int frames_per_second { get; set; }
        // public List<Mark> Marks { get; set; }
        public string Event_title { get; set; }
        public string Event_short_text { get; set; }
        public string Event_description { get; set; }
        public long Event_start_time { get; set; }
        public int Event_duration { get; set; }
        //"additional_media":null,"aux":"Winnetou - Eine neue Welt","sync_action":"","hash":""
    }
}
