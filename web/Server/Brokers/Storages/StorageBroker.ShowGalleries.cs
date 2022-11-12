using Dapper;
using FMFT.Web.Server.Models.ShowGalleries;
using FMFT.Web.Server.Models.ShowGalleries.DTOs;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<IEnumerable<ShowGallery>> SelectShowGalleryByShowIdAsync(int showId)
        {
            const string sql = "SELECT * FROM dbo.ShowGallery WHERE ShowId = @showId;";

            return await connection.QueryAsync<ShowGallery>(sql, new { showId });
        }

        public async ValueTask DeleteShowGalleryByIdAsync(int showGalleryId)
        {
            const string sql = "DELETE FROM dbo.ShowGallery WHERE Id = @showGalleryId;";
            await connection.ExecuteAsync(sql, new { showGalleryId });
        }

        public async ValueTask<ShowGallery> InsertShowGalleryAsync(InsertShowGalleryDTO dto)
        {
            const string sql = "INSERT INTO dbo.ShowGallery (ShowId, MediaId) " +
                "OUTPUT INSERTED.Id, INSERTED.ShowId, INSERTED.MediaId, INSERTED.CreateDate " +
                "VALUES (@ShowId, @MediaId);";

            return await connection.QuerySingleOrDefaultAsync<ShowGallery>(sql, dto);
        }
    }
}
