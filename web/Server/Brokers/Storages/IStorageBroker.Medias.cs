using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.DTOs;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Media> SelectMediaByIdAsync(Guid mediaId);
        ValueTask<Media> InsertMediaAsync(InsertMediaDTO dto);
    }
}
