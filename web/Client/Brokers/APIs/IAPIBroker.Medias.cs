using FMFT.Web.Client.Models.API;
using Microsoft.AspNetCore.Components.Forms;

namespace FMFT.Web.Client.Brokers.APIs
{
    public partial interface IAPIBroker
    {
        ValueTask<APIResponse> GetAllMediaAsync();
        ValueTask<APIResponse> UploadMediaAsync(IBrowserFile browserFile);
    }
}
