using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TAuth
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
         services.AddSingleton<JwtHandler>();
         services.AddMvc();
         services.AddControllers();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env, JwtHandler jwtHandler)
      {
         jwtHandler.AddCert("c:\\dev\\out.pfx", "test");
         if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
      }
   }
}
