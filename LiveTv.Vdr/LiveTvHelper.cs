using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.MediaInfo;
using System.Collections.Generic;
using System.Globalization;

namespace LiveTv.Vdr
{
    internal class LiveTvHelper
    {
        public static int _liveStreams;
        internal static MediaSourceInfo CreateMediaSourceInfo(string id, string uri)
        {
            _liveStreams++; // to generate a uniquie ID

            return new MediaSourceInfo()
            {
                Id = _liveStreams.ToString(CultureInfo.InvariantCulture),
                Path = uri,
                Protocol = MediaProtocol.Http,
                SupportsProbing = false,
                MediaStreams = new List<MediaStream>
                {
                    new MediaStream
                    {
                        Type = MediaStreamType.Video,
                        // Set the index to -1 because we don't know the exact index of the video stream within the container
                        Index = -1,                        
                        IsInterlaced = true //set to true if unknown to enable deinterlacing
                    },
                    new MediaStream
                    {
                        Type = MediaStreamType.Audio,
                        // Set the index to -1 because we don't know the exact index of the audio stream within the container
                        Index = -1
                    }
                }
            };
        }
    }
}
