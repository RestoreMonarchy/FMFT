using Dapper;
using FMFT.Web.Server.Models.ShowProducts;
using FMFT.Web.Server.Models.ShowProducts.Params;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<IEnumerable<ShowProduct>> SelectShowProductsByShowIdAsync(int showId)
        {
            const string sql = "SELECT * FROM dbo.ShowProducts WHERE ShowId = @showId;";

            return await connection.QueryAsync<ShowProduct>(sql, new { showId });
        }

        public async ValueTask<ShowProduct> SelectShowProductByIdAsync(int showProductId)
        {
            const string sql = "SELECT * FROM dbo.ShowProducts WHERE Id = @showProductId;";

            return await connection.QuerySingleOrDefaultAsync<ShowProduct>(sql, new { showProductId });
        }

        public async ValueTask<ShowProduct> InsertShowProductAsync(AddShowProductParams @params)
        {
            const string sql = "INSERT INTO dbo.ShowProducts (ShowId, Name, Price, IsEnabled) " +
                "OUTPUT INSERTED.* " +
                "VALUES (@ShowId, @Name, @Price, @IsEnabled);";

            return await connection.QuerySingleOrDefaultAsync<ShowProduct>(sql, @params);
        }

        public async ValueTask<ShowProduct> UpdateShowProductAsync(UpdateShowProductParams @params)
        {
            const string sql = @"UPDATE dbo.ShowProducts SET Name = @Name, Price = @Price, IsEnabled = @IsEnabled 
                WHERE Id = @Id AND ShowId = @ShowId;";

            await connection.ExecuteAsync(sql, @params);

            return await SelectShowProductByIdAsync(@params.Id);
        }
    }
}