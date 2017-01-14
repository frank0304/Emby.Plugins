using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Drawing;
using MediaBrowser.Controller.LiveTv;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.LiveTv;
using LiveTv.Vdr.RestfulApi.Client;
using LiveTv.Vdr.RestfulApi.Resources;
using LiveTv.Vdr.RestfulApi;

namespace LiveTv.Vdr
{
    class LiveTvService : ILiveTvService
    {
        #region Fields

        private readonly IRestfulApiClient _restfulApiClient;
        //private readonly IHttpClient _httpClient;
        //private readonly IJsonSerializer _jsonSerializer;
        public readonly ILogger _logger;

        #endregion 

        #region Properties

        public string HomePageUrl
        {
            get { return "http://www.tvdr.de/"; }
        }

        public string Name
        {
            get { return "LiveTV.Vdr"; }
        }       

        private IRestfulApiClient RestfulApiClient
        {
            get { return _restfulApiClient; }
        }

        #endregion

        #region Events

        public event EventHandler DataSourceChanged;
        public event EventHandler<RecordingStatusChangedEventArgs> RecordingStatusChanged;

        #endregion

        #region Ctor

        public LiveTvService(IHttpClient httpClient, IJsonSerializer jsonSerializer, ILogger logger)
        {
            _logger = logger;
            _restfulApiClient = new VdrRestfulApiClient(httpClient, jsonSerializer);

            _logger.Info("[LiveTV.Vdr] Service created.");
        }

        #endregion

        public Task CancelSeriesTimerAsync(string timerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CancelTimerAsync(string timerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CloseLiveStream(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CreateSeriesTimerAsync(SeriesTimerInfo info, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CreateTimerAsync(TimerInfo info, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecordingAsync(string recordingId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ImageStream> GetChannelImageAsync(string channelId, CancellationToken cancellationToken)
        {
            // Leave as is. This is handled by supplying image url to ChannelInfo
            return null;
        }

        public async Task<IEnumerable<ChannelInfo>> GetChannelsAsync(CancellationToken cancellationToken)
        {
            _logger.Info("[LiveTV.Vdr]  {0}...", nameof(GetStatusInfoAsync));

            var channelsResource = await RestfulApiClient.RequestChannelsResource(cancellationToken);
            
            var channelInfoList = new List<ChannelInfo>();
            foreach (ChannelResource chRes in channelsResource?.Channels)
            {
                channelInfoList.Add(Converters.ChannelResourceToChannelInfo(chRes));                    
            }

            return channelInfoList;
        }

        public Task<MediaSourceInfo> GetChannelStream(string channelId, string streamId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<MediaSourceInfo>> GetChannelStreamMediaSources(string channelId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<SeriesTimerInfo> GetNewTimerDefaultsAsync(CancellationToken cancellationToken, ProgramInfo program = null)
        {
            throw new NotImplementedException();
        }

        public Task<ImageStream> GetProgramImageAsync(string programId, string channelId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProgramInfo>> GetProgramsAsync(string channelId, DateTime startDateUtc, DateTime endDateUtc, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ImageStream> GetRecordingImageAsync(string recordingId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecordingInfo>> GetRecordingsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<MediaSourceInfo> GetRecordingStream(string recordingId, string streamId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<MediaSourceInfo>> GetRecordingStreamMediaSources(string recordingId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SeriesTimerInfo>> GetSeriesTimersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //TODO: think about ConnectionCheckAsync (sse nextpvr)
        public async Task<LiveTvServiceStatusInfo> GetStatusInfoAsync(CancellationToken cancellationToken)
        {            
            _logger.Info("[LiveTV.Vdr]  {0}...", nameof(GetStatusInfoAsync));

            var infoResource = await RestfulApiClient.RequestInfoResource(cancellationToken);

            if (infoResource == null)
                return new LiveTvServiceStatusInfo { Status = LiveTvServiceStatus.Unavailable };

            return new LiveTvServiceStatusInfo
            {
                Status = LiveTvServiceStatus.Ok,
                Version = infoResource.Vdr.Version,
                Tuners = new List<LiveTvTunerInfo>() //TODO:
            };
        }

        public Task<IEnumerable<TimerInfo>> GetTimersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RecordLiveStream(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetTuner(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSeriesTimerAsync(SeriesTimerInfo info, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTimerAsync(TimerInfo info, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
