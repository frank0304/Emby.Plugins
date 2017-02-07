using LiveTv.Vdr.Configuration;
using LiveTv.Vdr.RestfulApi.Resources;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System;
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

        public async Task<EventsResource> RequestEventsResource(CancellationToken cancellationToken, string channelId, DateTime endDate)
        {
            long span = (long) (endDate.ToLocalTime() - DateTime.Now.ToLocalTime()).TotalSeconds;

            var resourceStr = string.Format("{0}/{1}?timespan={2}", Constants.ResourceName.Events, channelId, span);
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

        #endregion
        
        public async Task DeleteRecording(string recordingFileName, CancellationToken cancellationToken)
        {
            var baseUri = Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;
            var uri = string.Format("{0}/recordings{1}", baseUri, recordingFileName);

            var options = new HttpRequestOptions()
            {                
                CancellationToken = cancellationToken,
                Url = uri
            };

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
