using LiveTv.Vdr.RestfulApi.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace LiveTv.Vdr.RestfulApi.Client
{
    interface IRestfulApiClient
    {
        Task<InfoResource> RequestInfoResource(CancellationToken cancellationToken);
        Task<ChannelsResource> RequestChannelsResource(CancellationToken cancellationToken);
    }
}
