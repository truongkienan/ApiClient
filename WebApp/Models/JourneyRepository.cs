using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class JourneyRepository : BaseRepository
    {
        public JourneyRepository(CallCenterContext context) : base(context)
        {
        }
        public async Task<List<StatisticJourney>> GetStatisticJourneys()
        {
            return await Get<List<StatisticJourney>>("/api/journey/statistic");
        }
        public List<Journey> GetJourneys()
        {
            string sql = "SELECT Journey.*, Dri.Username AS DriverName, Cus.Username AS CustomerName, StatusName FROM Journey "+
                    "JOIN Member AS Dri ON Journey.DriverId = Dri.MemberId JOIN Member AS Cus ON Journey.CustomerId = Cus.MemberId "+
                    "JOIN Status ON Status.StatusId = Journey.StatusId;";
            return context.Journeys.FromSqlRaw<Journey>(sql).ToList();
        }
        public int UpdateStatus(Journey obj)
        {
            string sql = "UPDATE Journey SET StatusId=@StatusId WHERE JourneyId =@Id";
            SqlParameter[] parameters = { 
            new SqlParameter("@Id", obj.JourneyId),
            new SqlParameter("@StatusId", obj.StatusId)
            };
            return context.Database.ExecuteSqlRaw(sql, parameters);
        }
        public Journey GetJourneyById(int id)
        {
            string sql = "SELECT Journey.*, Dri.Username AS DriverName, Cus.Username AS CustomerName, StatusName FROM Journey " +
                    "JOIN Member AS Dri ON Journey.DriverId = Dri.MemberId JOIN Member AS Cus ON Journey.CustomerId = Cus.MemberId " +
                    "JOIN Status ON Status.StatusId = Journey.StatusId WHERE JourneyId=@Id";
            return context.Journeys.FromSqlRaw<Journey>(sql, new SqlParameter("@Id", id)).SingleOrDefault();
        }
    }
}
