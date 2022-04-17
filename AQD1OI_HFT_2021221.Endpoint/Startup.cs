using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQD1OI_HFT_2021221.Logic;
using AQD1OI_HFT_2021221.Repository;
using AQD1OI_HFT_2021221.Data;
using System.Text.Json.Serialization;
using AQD1OI_HFT_2021221.Endpoint.Services;

namespace AQD1OI_HFT_2021221.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IBikeLogic, BikeLogic>();
            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<IRentalLogic, RentalLogic>();
            services.AddTransient<IBikeRepository, BikeRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<BikeDbContext, BikeDbContext>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
