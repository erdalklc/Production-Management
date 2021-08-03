using EPM.Track.Repository.Repository;
using EPM.Track.Service.Helpers;
using EPM.Track.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace EPM.Track.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration , IHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment _hostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddScoped<ITrackService, TrackService>(); 
            services.AddSingleton<ITrackRepository, TrackRepository>();
            services.AddSingleton<IEgemenRepository, EgemenRepository>();  
            services.AddControllers();
            services.AddControllersWithViews()
              .AddNewtonsoftJson(options => {
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                  options.UseMemberCasing();
              }
          );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
