using PlayTestBuildsAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlayTestWebUI.Pages
{
    public partial class Index
    {
        private List<ConfigFile> projects;

        protected override async Task OnInitializedAsync() => await GetProjects();

        private async Task GetProjects()
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new("http://PlayTestBuildsAPI:80")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("/api/config");

            if (!response.IsSuccessStatusCode)
                return;

            Stream stream = await response.Content.ReadAsStreamAsync();
            projects = await JsonSerializer.DeserializeAsync<List<ConfigFile>>(stream);
        }
    }
}
