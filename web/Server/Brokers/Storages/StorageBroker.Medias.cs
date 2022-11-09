using Dapper;
using FMFT.Web.Server.Models.Medias;
using FMFT.Web.Server.Models.Medias.DTOs;
using FMFT.Web.Server.Models.Users;
using Microsoft.AspNetCore.Mvc.Localization;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Media> SelectMediaByIdAsync(Guid mediaId)
        {
            const string sql = "SELECT m.*, u.* FROM dbo.Media m " +
                "LEFT JOIN dbo.Users u ON u.Id = m.UserId " +
                "WHERE m.Id = @mediaId;";

            Media media = null;

            await connection.QueryAsync<Media, UserInfo, Media>(sql, (m, u) => 
            {
                media = m;
                media.User = u;

                return null;
            }, new { mediaId });

            return media;
        }

        public async ValueTask<Media> InsertMediaAsync(InsertMediaDTO dto)
        {
            const string sql = "INSERT INTO dbo.Media (Name, ContentType, Content, UserId) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@Name, @ContentType, @Content, @UserId);";

            Guid mediaId = await connection.ExecuteScalarAsync<Guid>(sql, dto);

            return await SelectMediaByIdAsync(mediaId);
        }
    }
}
