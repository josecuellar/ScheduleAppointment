using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Providers.Impl;
using ScheduleAppointment.API.Services;
using ScheduleAppointment.API.Services.Impl;

namespace ScheduleAppointment.API
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
            services.AddMvc();
            services.AddTransient<ILoggerProvider, ConsoleLoggerProvider>();
            services.AddTransient<IHttpClientProvider, RestSharpHttpClientProvider>();
            services.AddTransient<IAvailabilityWeekService, APIAvailabilityWeekService>();

            services.Configure<Settings.APIGetAvailabilityWeekSettings>(Configuration.GetSection("APIGetAvailabilityWeekSettings"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
