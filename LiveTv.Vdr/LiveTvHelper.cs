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
                SupportsProbing = true,
            };
        }
    }
}
