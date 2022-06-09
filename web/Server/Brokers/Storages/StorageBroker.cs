using System.Data.SqlClient;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        private readonly IConfiguration configuration;
        private readonly SqlConnection connection;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
    }
}
