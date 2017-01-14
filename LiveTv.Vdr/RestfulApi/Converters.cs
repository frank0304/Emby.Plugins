using LiveTv.Vdr.RestfulApi.Resources;
using MediaBrowser.Controller.LiveTv;
using MediaBrowser.Model.LiveTv;

namespace LiveTv.Vdr.RestfulApi
{
    internal static class Converters
    {
        internal static ChannelInfo ChannelResourceToChannelInfo(ChannelResource chRes)
        {
            var baseUri = Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;

            return new ChannelInfo()
            {
                ChannelType = chRes.Is_radio ? ChannelType.Radio : ChannelType.TV,
                HasImage = chRes.Image,
                Id = chRes.Channel_id,
                ImageUrl = chRes.Image ?
                    string.Format("{0}/channels/image/{1}", baseUri, chRes.Channel_id) :
                    null,
                Name = chRes.Name,
                Number = chRes.Number.ToString(),
            };
        }
        
    }
}
