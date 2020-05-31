using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HaagsVertaler
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
          services.AddResponseCompression();
          services.AddRazorPages()
              .AddRazorRuntimeCompilation();
      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
          if (env.IsDevelopment())
          {
              app.UseDeveloperExceptionPage();
          }
          else
          {
              app.UseExceptionHandler("/Error");
              app.UseHsts();
          }

          app.UseHttpsRedirection();
          app.UseStaticFiles(new StaticFileOptions()
          {
              HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,               
              OnPrepareResponse = (context) =>
              {
                  var headers = context.Context.Response.GetTypedHeaders();
                  headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                  {
                      Public = true,
                      MaxAge = TimeSpan.FromDays(365)
                  };
              }
          });

          app.UseRouting();

          app.UseAuthorization();

          app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
          });
      }
  }
}