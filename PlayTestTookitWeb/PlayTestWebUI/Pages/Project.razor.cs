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
    public partial class Project : ComponentBase
    {
        [Parameter] public string Title { get; set; }

        private IList<ConfigFile> Playtests { get; set; } = new List<ConfigFile>();

        private bool HasPlaytests { get => Playtests.Any(); }

        protected override async Task OnInitializedAsync() => await RefreshProjects();

        private async Task RefreshProjects()
        {
            using HttpClient client = new HttpClient
            {
                BaseAddress = new("http://PlayTestBuildsAPI:80")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"/api/config");

            if (!response.IsSuccessStatusCode)
                return;

            Stream stream = await response.Content.ReadAsStreamAsync();
            Playtests = await JsonSerializer.DeserializeAsync<IList<ConfigFile>>(stream);
        }
    }
}
