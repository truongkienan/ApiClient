namespace WebApp.Models
{
    public class StatusRepository:BaseRepository
    {
        public async Task<List<Status>> GetStatuses()
        {
            return await Get<List<Status>>("/api/status");
            //using (HttpClient client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.BaseAddress = new Uri("https://localhost:44396");
            //    HttpResponseMessage message = await client.GetAsync("/api/status");
            //    if (message.IsSuccessStatusCode)
            //    {
            //        return await message.Content.ReadFromJsonAsync<List<Status>>();
            //    }
            //    return null;
            //}
        }

        public async Task<Status> GetStatusById(byte id)
        {
            return await Get<Status>($"/api/status/{id}");
            //using (HttpClient client=new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.BaseAddress = new Uri("https://localhost:44396");
            //    HttpResponseMessage message = await client.GetAsync($"/api/status/{id}");
            //    if (message.IsSuccessStatusCode)
            //    {
            //        return await message.Content.ReadFromJsonAsync<Status>();
            //    }
            //    return null;
            //}
        }

        public async Task<int> Add(Status obj)
        {
            return await Post<Status, int>("/api/status", obj);
            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44396");
            //    HttpResponseMessage message = await client.PostAsJsonAsync("/api/status", obj);
            //    if (message.IsSuccessStatusCode)
            //    {
            //        return await message.Content.ReadFromJsonAsync<int>();
            //    }
            //    return -1;
            //}
        }
        public async Task<int> Edit(Status obj)
        {
            return await Put<Status, int>("/api/status", obj);
            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44396");
            //    HttpResponseMessage message = await client.PutAsJsonAsync("/api/status", obj);
            //    if (message.IsSuccessStatusCode)
            //    {
            //        return await message.Content.ReadFromJsonAsync<int>();
            //    }
            //    return -1;
            //}
        }

        public async Task<int> Delete(byte id)
        {
            return await Delete<int>($"/api/status/{id}");

            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44396");
            //    HttpResponseMessage message = await client.DeleteAsync($"/api/status/{id}");
            //    if (message.IsSuccessStatusCode)
            //    {
            //        return await message.Content.ReadFromJsonAsync<int>();
            //    }
            //    return -1;
            //}
        }
    }

    //public class StatusRepository : BaseRepository
    //{
    //    public StatusRepository(CallCenterContext context) : base(context)
    //    {
    //    }
    //    public List<Status> GetStatuses()
    //    {
    //        return context.Statuses.ToList();
    //    }
    //}

}
