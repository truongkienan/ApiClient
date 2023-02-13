namespace WebApp.Models
{
    //Strategy Pattern
    public interface StrategyFetch
    {
        Task<HttpResponseMessage> Execute(HttpClient client, string url);
    }

    public class StrategyGet : StrategyFetch
    {
        public Task<HttpResponseMessage> Execute(HttpClient client, string url)
        {
            return client.GetAsync(url);
        }
    }

    public class StrategyDelete: StrategyFetch
    {
        public Task<HttpResponseMessage> Execute(HttpClient client, string url)
        {
            return client.DeleteAsync(url);
        }
    }
}
