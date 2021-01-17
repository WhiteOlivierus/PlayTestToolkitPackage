using Blazored.SessionStorage;
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
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISessionStorageService SessionStorage { get; set; }

        private string ProjectName { get; set; }

        private IList<ConfigFile> Configs { get; set; } = new List<ConfigFile>();

        private bool HasPlaytests { get => Configs.Any(); }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            ProjectName = await SessionStorage.GetItemAsync<string>("SelectedProject");
            await RefreshProjects();

            StateHasChanged();
        }

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
            IList<ConfigFile> AllPlaytest = await JsonSerializer.DeserializeAsync<IList<ConfigFile>>(stream);

            foreach (ConfigFile playtest in AllPlaytest)
            {
                if (playtest.ProjectName == ProjectName)
                    Configs.Add(playtest);
            }

            Configs = Configs.OrderBy(r => r.Version).ToList();
        }

        private void NavigateTo(ConfigFile config)
        {
            SessionStorage.SetItemAsync("SelectedConfig", config);

            NavigationManager.NavigateTo("/playtest", true);
        }
    }
}
