using Api.Exstensions;
using Entities;
using Entities.Models;
using Entities.SeedData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.ConfigureIdentity();

            services.ConfigureCors();

            services.ConfigureJwt(Configuration);

            services.ConfigureRepositoryWrapper();
            
            services.AddTransient<SeedData>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedData seedData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            seedData.SeedAdminUserAsync();
            
            app.UseCors("CorsPolicy");

            app.UseMvc();

            app.UseAuthentication();
        }
    }
}
