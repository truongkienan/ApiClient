using System.Net.Http.Headers;

namespace WebApp.Models
{
    public abstract class BaseRepository
    {
        protected CallCenterContext context;
        public BaseRepository(CallCenterContext context)
        {
            this.context = context;
        }
        public BaseRepository(){}        

        public async Task<T> Get<T>(string url, string token = null)
        {
            return await Fetch<T>(url, new StrategyGet(), token);
        }
        public async Task<T> Delete<T>(string url, string token = null)
        {
            return await Fetch<T>(url, new StrategyDelete(), token); 
        }        

        protected async Task<T> Fetch<T>(string url, StrategyFetch strategy, string token = null)
        {
            return await Execute<T>(url, new FetchExecute(strategy), token);
        }

        protected async Task<Out> Save<In, Out>(string url, In obj, StrategySave strategy, string token = null)
        {
            return await Execute<Out>(url, new SaveExecute<In>(strategy, obj), token);
        }

        public async Task<Out> Post<In, Out>(string url, In obj)
        {
            SaveRepository repository = new PostRepository();
            return await repository.Save<In, Out>(url, obj);
        }

        public async Task<Out> Put<In, Out>(string url, In obj)
        {
            SaveRepository repository = new PutRepository();
            return await repository.Save<In, Out>(url, obj);            
        }

        protected async Task<T> Execute<T>(string url, StrategyExecute strategy, string token = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri("https://localhost:44396");
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                HttpResponseMessage message = await strategy.Execute(client, url);
                if (message.IsSuccessStatusCode)
                {
                    return await message.Content.ReadFromJsonAsync<T>();
                }
                return default(T);
            }
        }
    }
}
