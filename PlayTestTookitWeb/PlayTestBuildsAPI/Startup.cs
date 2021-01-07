using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PlayTestBuildsAPI.Models.Settings;
using PlayTestBuildsAPI.Services;
using System;

namespace PlayTestBuildsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PlayTestBuildsSettings>(Configuration.GetSection(nameof(PlayTestBuildsSettings)));

            services.AddSingleton((Func<IServiceProvider, IMongoDBSettings>)(sp => sp.GetRequiredService<IOptions<PlayTestBuildsSettings>>().Value));

            services.AddSingleton<BuildsService>();
            services.AddSingleton<ConfigService>();
            services.AddSingleton<DataService>();

            services.AddSingleton<FileService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlayTestBuildsAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlayTestBuildsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
