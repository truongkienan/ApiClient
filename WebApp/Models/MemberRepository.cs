using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class MemberRepository : BaseRepository
    {
        public async Task<int> Add(RegisterModel obj)
        {
            return await Post<RegisterModel, int>("/api/member/register",obj);
        }

        //public async Task<int> Add(RegisterModel obj)
        //{
        //    using (HttpClient client=new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44396");
        //        HttpResponseMessage message = await client.PostAsJsonAsync("/api/member/register", obj);
        //        if (message.IsSuccessStatusCode)
        //        {
        //            return await message.Content.ReadFromJsonAsync<int>();
        //        }
        //        return -1;
        //    }
        //}
        public MemberRepository(CallCenterContext context) : base(context)
        {
        }

        public async Task<List<Member>> GetMembers(string token)
        {
            return await Get<List<Member>>("/api/member", token);

            //using (HttpClient client =new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44396");
            //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            //    HttpResponseMessage message = await client.GetAsync("/api/member");
            //    if (message.IsSuccessStatusCode)
            //    {
            //        return await message.Content.ReadFromJsonAsync<List<Member>>();
            //    }
            //    return null;
            //}
        }
        public List<MemberAccessToken> GetMemberAccessTokens(string id)
        {
            return context.MemberAccessTokens.FromSqlRaw("GetAccessTokensByMember @Id", new SqlParameter("@Id", id)).ToList();
        }
        public int UpdateToken(Member obj)
        {
            string sql = "UPDATE Member SET AccessToken =@Token WHERE MemberId=@Id";
            SqlParameter[] sp =
            {
                new SqlParameter("@Token", obj.AccessToken),
                new SqlParameter("@Id", obj.Id)
            };
            return context.Database.ExecuteSqlRaw(sql, sp);
        }
        public List<Member> GetCustomerByDriver(string id)
        {
            string sql = "SELECT Member.* FROM Member JOIN Journey ON Member.MemberId=Journey.CustomerId AND DriverId=@Id AND Longitude IS NOT NULL";
            return context.Members.FromSqlRaw<Member>(sql, new SqlParameter("@Id",id)).ToList();
        }
        public Member GetDriverByCustomer(string id)
        {
            string sql = "SELECT Member.* FROM Member JOIN Journey ON Member.MemberId=Journey.DriverId AND CustomerId=@Id";
            return context.Members.FromSqlRaw<Member>(sql, new SqlParameter("@Id", id)).SingleOrDefault();
        }
        public List<Member> GetDrivers()
        {
            return context.Members.Where(p=>p.Role=="Driver").ToList();
        }
        public int UpdateLocation(Member obj)
        {
            string sql = "UPDATE Member SET Longitude=@Long, Latitude=@Lat WHERE MemberId=@Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Long", obj.Longitude),
                new SqlParameter("@Lat", obj.Latitude),
                new SqlParameter("@Id", obj.Id),
            };
            return context.Database.ExecuteSqlRaw(sql,parameters);
        }

        public async Task<ResponseLogin> Login(LoginModel obj)
        {
            return await Post<LoginModel, ResponseLogin>("/api/member/login", obj);
        }

        //public async Task<ResponseLogin> Login(LoginModel obj)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44396");
        //        HttpResponseMessage message = await client.PostAsJsonAsync("/api/member/login", obj);
        //        if (message.IsSuccessStatusCode)
        //        {
        //            return await message.Content.ReadFromJsonAsync<ResponseLogin>();

        //        }
        //        return null;
        //    }
        //    //return context.Members.SingleOrDefault(p=>p.Username==obj.Usr 
        //    //&& p.Password==Helper.Hash(obj.Usr+ "@#$!&" + obj.Pwd));
        //}
    }
}
