using LiveTv.Vdr.RestfulApi.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LiveTv.Vdr.RestfulApi.Client
{
    interface IRestfulApiClient
    {
        Task<InfoResource> RequestInfoResource(CancellationToken cancellationToken);
        Task<ChannelsResource> RequestChannelsResource(CancellationToken cancellationToken);
        Task<EventsResource> RequestEventsResource(CancellationToken cancellationToken, string channelId, DateTimeOffset endDate);
        Task<RecordingsResource> RequestRecordingsResource(CancellationToken cancellationToken);
        Task<int> RequestRecordingNo(string recordingFileName, CancellationToken cancellationToken);
        Task<TimersResource> RequestTimersResource(CancellationToken cancellationToken);
        Task DeleteRecording(string recordingId, CancellationToken cancellationToken);
    }
}
