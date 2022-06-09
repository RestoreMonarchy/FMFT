using Dapper;
using FMFT.Web.Shared.Models.Auditoriums;
using FMFT.Web.Shared.Models.Seats;

namespace FMFT.Web.Server.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async ValueTask<Auditorium> SelectAuditoriumByIdAsync(int auditoriumId)
        {
            const string sql = @"SELECT a.*, s.* FROM dbo.Auditoriums a 
                                    LEFT JOIN dbo.Seats s ON a.Id = s.AuditoriumId
                                    WHERE a.Id = @auditoriumId;";

            Auditorium auditorium = null;
            await connection.QueryAsync<Auditorium, Seat, Auditorium>(sql, (a, s) => 
            {
                if (auditorium == null)
                {
                    auditorium = a;
                    auditorium.Seats = new List<Seat>();
                }

                if (s != null)
                {
                    auditorium.Seats.Add(s);
                }   

                return null;
            }, new { auditoriumId });

            return auditorium;
        }

        public async ValueTask<IEnumerable<Auditorium>> SelectAllAuditoriumsAsync()
        {
            const string sql = @"SELECT a.*, s.* FROM dbo.Auditoriums a 
                                    LEFT JOIN dbo.Seats s ON a.Id = s.AuditoriumId";

            List<Auditorium> auditoriums = new();
            await connection.QueryAsync<Auditorium, Seat, Auditorium>(sql, (a, s) =>
            {
                Auditorium auditorium = auditoriums.FirstOrDefault(x => x.Id == a.Id);
                
                if (auditorium == null)
                {
                    auditorium = a;
                    auditorium.Seats = new();
                    auditoriums.Add(auditorium);
                }

                if (s != null)
                {
                    auditorium.Seats.Add(s);
                }                    

                return null;
            });

            return auditoriums;
        }
    }
}
