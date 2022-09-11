using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Client.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        ValueTask UpdateUserCultureAsync(int userId, CultureId cultureId);
    }
}