using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Taksi.Server.BLL.Services.Implementations;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;
using Taksi.Server.DAL.Repositories.Implementations.Ef;
using Taksi.Server.DAL.Repositories.Interfaces;

namespace Taksi.Server
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
            services.AddDbContext<EfContext>(opt =>
            {
                opt.UseSqlite("Filename=Taksi.db");
            });

            services.AddTransient<IRepository<ClientEntity>, EfClientRepository>();
            services.AddTransient<IRepository<CreditCardEntity>, EfCreditCardRepository>();
            services.AddTransient<IRepository<DriverEntity>, EfDriverRepository>();
            services.AddTransient<IRepository<RideEntity>, EfRideRepository>();
            
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IRideService, RideService>();
            services.AddTransient<IDriverService, DriverService>();

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reports.Server", Version = "v1" });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reports.Server v1"));
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