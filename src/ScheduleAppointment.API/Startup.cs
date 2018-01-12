using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleAppointment.API.Factories;
using ScheduleAppointment.API.Factories.Impl;
using ScheduleAppointment.API.Model.DTO;
using ScheduleAppointment.API.Providers;
using ScheduleAppointment.API.Providers.Impl;
using ScheduleAppointment.API.Services;
using ScheduleAppointment.API.Services.Impl;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddTransient<IAvailabilityWeekService, APIAvailabilityWeekService>();
            services.AddTransient<ILoggerProvider, ConsoleLoggerProvider>();
            services.AddTransient<IHttpClientProvider, HttpClientProvider>();
            services.AddTransient<IFactory<AvailabilityWeek, WeekSlots>, WeekSlotsFactory>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API Appointments", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Appointments v1");
            });

            app.UseMvc();
        }
    }
}
