namespace WebApp.Models
{
    public abstract class SaveRepository
    {
        public async Task<Out> Save<In, Out>(string url, In obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44396");
                
                HttpResponseMessage message = await Execute(client, url, obj);
                if (message.IsSuccessStatusCode)
                {
                    return await message.Content.ReadFromJsonAsync<Out>();
                }
                return default(Out);
            }
        }

        protected abstract Task<HttpResponseMessage> Execute<In>(HttpClient client, string url, In obj);
    }

    public class PostRepository : SaveRepository
    {
       protected override Task<HttpResponseMessage> Execute<In>(HttpClient client, string url, In obj)
        {
            return client.PostAsJsonAsync<In>(url, obj);
        }
    }

    public class PutRepository : SaveRepository
    {
        protected override Task<HttpResponseMessage> Execute<In>(HttpClient client, string url, In obj)
        {
            return client.PutAsJsonAsync<In>(url, obj);
        }
    }
}
