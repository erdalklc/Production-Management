using EPM.Repository.Base;
using EPM.Service.Base;
using EPM.Tools.Helpers;
using EPM.Tools.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 

namespace EPM_Web
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
            // Add framework services.
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
             
            services.AddScoped<AppFilterAttribute>();
            services.AddSingleton<IMongoRepository, MongoRepository>();
            services.AddSingleton<IEPMRepository, EPMRepository>();
            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUretimService, UretimService>();
            services.AddScoped<ISatinAlmaService, SatinAlmaService>();  
            services.AddScoped<IUretimIzleService, UretimIzleService>();
            services.AddSingleton<IADAccountService, ADAccountService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddControllersWithViews()
                 .AddNewtonsoftJson(options => {
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                     options.UseMemberCasing();
                }
             ); 
        }
         
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization(); 

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
