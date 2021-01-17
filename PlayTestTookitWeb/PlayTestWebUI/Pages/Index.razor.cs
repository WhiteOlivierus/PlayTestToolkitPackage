using Blazored.SessionStorage;
using Data.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlayTestWebUI.Pages
{
    public partial class Index
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISessionStorageService SessionStorage { get; set; }

        private IList<string> ProjectNames { get; set; } = new List<string>();

        private bool HasProjects { get => ProjectNames.Any(); }

        protected override async Task OnInitializedAsync() => await RefreshProjects();

        private async Task RefreshProjects()
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
            IList<ConfigFile> Projects = await JsonSerializer.DeserializeAsync<IList<ConfigFile>>(stream);

            ProjectNames = (from project in Projects
                            select project.ProjectName).Distinct().ToList();
        }

        private void NavigateTo(string projectName)
        {
            SessionStorage.SetItemAsync("SelectedProject", projectName);

            NavigationManager.NavigateTo("/project", true);
        }
    }
}
