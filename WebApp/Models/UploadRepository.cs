namespace WebApp.Models
{
    public class UploadRepository
    {
        public async Task<string> Upload(IFormFile f, Info obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44396");
                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(f.OpenReadStream()),"f", f.FileName);
                content.Add(new StringContent(obj.Name), "name");
                content.Add(new StringContent(obj.Size.ToString()), "size");

                HttpResponseMessage message = await client.PostAsync("/api/upload", content);
                if (message.IsSuccessStatusCode)
                {
                    return await message.Content.ReadAsStringAsync();
                }
                Console.Write($"*********Error*********{message.StatusCode}");
                return null;
            }
        }
    }
}
