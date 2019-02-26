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
        Task<EventsResource> RequestEventsResource(CancellationToken cancellationToken, string channelId, string eventid); 
        Task<RecordingsResource> RequestRecordingsResource(CancellationToken cancellationToken);
        Task<int> RequestRecordingNo(string recordingFileName, CancellationToken cancellationToken);
        Task<TimersResource> RequestTimersResource(CancellationToken cancellationToken);
        Task CreateTimer(string channel, string eventid, string minpre, string minpost, CancellationToken cancellationToken);
        Task DeleteRecording(string recordingId, CancellationToken cancellationToken);
        Task DeleteTimer(string timerId, CancellationToken cancellationToken);
        Task <TimersResource> UpdateTimer(string timer_id, string minpre, string minpost, CancellationToken cancellationToken);
        
    }
}
