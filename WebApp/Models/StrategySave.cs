namespace WebApp.Models
{
    public interface StrategySave
    {
        Task<HttpResponseMessage> Execute<In>(HttpClient client, string url, In obj);
    }

    public class StrategyPost : StrategySave
    {
        public Task<HttpResponseMessage> Execute<In>(HttpClient client, string url, In obj)
        {
            return client.PostAsJsonAsync<In>(url, obj);
        }
    }

    public class StrategyPut : StrategySave
    {
        public Task<HttpResponseMessage> Execute<In>(HttpClient client, string url, In obj)
        {
            return client.PutAsJsonAsync<In>(url, obj);
        }
    }
}
