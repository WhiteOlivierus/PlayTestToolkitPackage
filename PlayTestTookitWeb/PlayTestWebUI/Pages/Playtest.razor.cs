using Microsoft.AspNetCore.Components;
using PlayTestWebUI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlayTestWebUI.Pages
{
    public partial class Playtest : ComponentBase
    {
        [Parameter] public string Title { get; set; }

        private IList<DataFile> Data { get; set; } = new List<DataFile>();

        private bool HasData { get => Data.Any(); }

        protected override async Task OnInitializedAsync() => await RefreshProjects();

        private async Task RefreshProjects()
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new("http://PlayTestBuildsAPI:80")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"/api/data/{Title}");

            if (!response.IsSuccessStatusCode)
                return;

            Stream stream = await response.Content.ReadAsStreamAsync();
            Data = await JsonSerializer.DeserializeAsync<IList<DataFile>>(stream);
        }
    }
}
