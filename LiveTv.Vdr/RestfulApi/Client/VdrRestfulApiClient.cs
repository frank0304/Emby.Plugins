using LiveTv.Vdr.Configuration;
using LiveTv.Vdr.RestfulApi.Resources;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Controller.LiveTv;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Net;

namespace LiveTv.Vdr.RestfulApi.Client
{
    internal class VdrRestfulApiClient : IRestfulApiClient
    {
        #region Fields

        private string _baseUri;
        private IHttpClient _httpClient;
        private IJsonSerializer _jsonSerializer;
        private ILogger _logger;

        private ILiveTvManager _liveTvManager;

        private LiveTvService GetService()
        {
            return _liveTvManager.Services.OfType<LiveTvService>().First();
        }

        #endregion

        #region Ctor

        internal VdrRestfulApiClient(IHttpClient httpClient, IJsonSerializer jsonSerializer, ILogger logger)
        {
            _baseUri =  Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;
            _httpClient = httpClient;
            _jsonSerializer = jsonSerializer;
            _logger = logger;
        }

        #endregion

        #region ResourceRequests

        public async Task<InfoResource> RequestInfoResource(CancellationToken cancellationToken)
        {
            return await RequestResource<InfoResource>(cancellationToken, Constants.ResourceName.Info);
        }

        public async Task<ChannelsResource> RequestChannelsResource(CancellationToken cancellationToken)
        {
            return await RequestResource<ChannelsResource>(cancellationToken, Constants.ResourceName.Channels);
        }

        public async Task<EventsResource> RequestEventsResource(CancellationToken cancellationToken, string channelId, DateTimeOffset endDate)
        {
            long span = (long) (endDate.ToLocalTime() - DateTime.Now.ToLocalTime()).TotalSeconds;

            var resourceStr = string.Format("{0}/{1}?timespan={2}", Constants.ResourceName.Events, channelId, span);
            return await RequestResource<EventsResource>(cancellationToken, resourceStr);
        }
       public async Task<EventsResource> RequestEventsResource(CancellationToken cancellationToken, string channelId, string eventid)
       {
           /* Returns event (epg) information based on channelId and eventId
            Eaxample curl string   curl  -v localhost:8002/events.json/S19.2E-1-1019-10301/48722 */
           var resourceStr = string.Format("{0}/{1}/{2}", Constants.ResourceName.Events, channelId, eventid);
           return await RequestResource<EventsResource>(cancellationToken, resourceStr);
       } 
        public async Task<RecordingsResource> RequestRecordingsResource(CancellationToken cancellationToken)
        {
            return await RequestResource<RecordingsResource>(cancellationToken, Constants.ResourceName.Recordings);
        }

        public async Task<int> RequestRecordingNo(string recordingFileName, CancellationToken cancellationToken)
        {            
            var recordingsRes = await RequestResource<RecordingsResource>(cancellationToken, Constants.ResourceName.Recordings);
            var recRes = recordingsRes.Recordings.Find(res => res.File_name.Equals(recordingFileName));
            return recRes.Number;
        }

        public async Task<TimersResource> RequestTimersResource(CancellationToken cancellationToken)
        {
            return await RequestResource<TimersResource>(cancellationToken, Constants.ResourceName.Timers);
        }

        public async Task CreateTimer(string _channel, string _eventid, string _minpre, string _minpost, CancellationToken cancellationToken)
        {
            /* Example curl token : 
             curl -v -H "Content-Type: application/json" -d 'channel=S19.2E-1-1019-10301&eventid=46967&premin=0&postmin=0' localhost:8002/timers.jsoncurl -v -H "Content-Type: application/json" 
             -d 'channel=S19.2E-1-1019-10301&eventid=46931&minpre=3&minpost=4' localhost:8002/timers.json */
            //var timerSettings = new{channel = _channel, eventid = _eventid, premin = _minpre, postmin = _minpost};
            var baseUri = Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;
            var postData = new Dictionary<string, string>
            {
                { "channel", _channel }, 
                { "eventid", _eventid }, 
                { "minpre", _minpre }, 
                { "minpost", _minpost },
            };         
            //var postContent = _jsonSerializer.SerializeToString(timerSettings);
            var options = new HttpRequestOptions
            {
                RequestContentType = "application/json",
                CancellationToken = cancellationToken,
                Url = string.Format("{0}/timers", baseUri),
            };
            options.SetPostData(postData);
            //options.LogRequestAsDebug = true;
            //options.LogErrorResponseBody = true;
            _logger.Debug("[LiveTV.Vdr] CreateRecording : {0}",options.RequestContentType.ToString());
            _logger.Debug("[LiveTV.Vdr] CreateRecording : {0}",options.Url.ToString());
            _logger.Debug("[LiveTV.Vdr] CreateRecording : {0}",options.RequestContent.ToString());
            try
            {
                var stream = await _httpClient.SendAsync(options, HttpMethod.Post.Method).ConfigureAwait(false);
            }
            catch (HttpException ex)
            {
                _logger.Error(string.Format("[LiveTV.Vdr] CreateRecording with exception: {0}", ex.Message));
                _logger.Error(string.Format("[LiveTV.Vdr] CreateRecording with exception: {0}", ex.InnerException));
                throw new LiveTvConflictException();
            }                        
            //return await RequestResource<TimersResource>(cancellationToken, Constants.ResourceName.Timers);
        }
        public async Task <TimersResource> UpdateTimer(string timer_id, string minpre, string minpost, CancellationToken cancellationToken)
        {
            // curl -X PUT -v -H "Content-Type: application/json" -d 'timer_id=S19.2E-1-1019-10301:0:1550876400:25:158&minpre=15&minpost=8' localhost:8002/timers.json
            var baseUri = Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;

            var options = new HttpRequestOptions
            {
                RequestContentType = "application/json",
                CancellationToken = cancellationToken,
                Url = string.Format("{0}/timers", baseUri),
            };   
            var postData = new Dictionary<string, string>
            {
                { "timer_id", timer_id }, 
                { "minpre", minpre }, 
                { "minpost", minpost },
            }; 
            options.SetPostData(postData);           
            _logger.Debug("[LiveTV.Vdr] UpdateTimer : TimerId={0}, minpre={1}, minpost={2}",timer_id.ToString(),minpre.ToString(),minpost.ToString());
            TimersResource TimerUpdated;
            try
            {
                var stream = await _httpClient.SendAsync(options, HttpMethod.Put.Method).ConfigureAwait(false);
                TimerUpdated = _jsonSerializer.DeserializeFromStream<TimersResource>(stream.Content);
                // var allTimers = await GetService().GetTimersAsync(cancellationToken);
                return TimerUpdated;
            }
            catch (HttpException ex)
            {                
                _logger.Error("[LiveTV.Vdr]  {0}: {1}", nameof(UpdateTimer), ex.InnerException.ToString());
                return null;
            }
        }
        public async Task DeleteTimer(string timerId, CancellationToken cancellationToken)
        {
            var baseUri = Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;
            var uri = string.Format("{0}/timers{1}", baseUri, timerId);

            var options = new HttpRequestOptions()
            {                
                CancellationToken = cancellationToken,
                Url = uri
            };
            _logger.Debug("[LiveTV.Vdr] DeleteTimer: {0}",options.ToString());
            try
            {
                _logger.Info("[LiveTV.Vdr] DeleteTimer  {0} : ", timerId);
                var stream = await _httpClient.SendAsync(options, HttpMethod.Delete.Method).ConfigureAwait(false);
            }
            catch (HttpException ex)
            {                
                _logger.Error("[LiveTV.Vdr]  {0}: {1}", nameof(DeleteTimer), ex.InnerException.ToString());
            }
        }
        public async Task DeleteRecording(string recordingFileName, CancellationToken cancellationToken)
        {
            var baseUri = Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;
            var uri = string.Format("{0}/recordings{1}", baseUri, recordingFileName);

            var options = new HttpRequestOptions()
            {                
                CancellationToken = cancellationToken,
                Url = uri
            };
            _logger.Debug("[LiveTV.Vdr] DeleteRecording : {0}",options.ToString());
            try
            {
                _logger.Info("[LiveTV.Vdr]  {0}: Deleting {1}", nameof(DeleteRecording), recordingFileName);
                var stream = await _httpClient.SendAsync(options, HttpMethod.Delete.Method).ConfigureAwait(false);
            }
            catch (HttpException ex)
            {                
                _logger.Error("[LiveTV.Vdr]  {0}: {1}", nameof(DeleteRecording), ex.InnerException.ToString());
            }
        }
        #endregion
        private async Task<T> RequestResource<T>(CancellationToken cancellationToken, string resource)  where T  : IRootResource
        {
            var options = new HttpRequestOptions()
            {
                RequestContentType = "application/json",
                CancellationToken = cancellationToken,
                Url = string.Format("{0}/{1}", _baseUri, resource)
            };

            T restResource;
            using (var stream = await _httpClient.Get(options).ConfigureAwait(false))
            {
                restResource = _jsonSerializer.DeserializeFromStream<T>(stream);
            }

            return restResource;
        }
    }
}
