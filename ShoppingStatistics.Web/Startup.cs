using FileReaderComponent;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShoppingStatistics.Core.Services;
using ShoppingStatistics.Web.Services;

namespace ShoppingStatistics.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileReaderService>(sp => new FileReaderService());
            services.AddScoped<DataAnalyzer>();
            services.AddScoped<DataProcessor>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
