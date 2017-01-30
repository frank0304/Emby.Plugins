using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.MediaInfo;
using System.Collections.Generic;

namespace LiveTv.Vdr
{
    internal class LiveTvHelper
    {
        internal static MediaSourceInfo CreateMediaSourceInfo(string channelId, string uri)
        {
            return new MediaSourceInfo()
            {
                Id = channelId,
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
