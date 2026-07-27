using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
          services.AddMcpServer()
              .WithHttpTransport(options => options.Stateless = true)
              .WithToolsFromAssembly();
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
              endpoints.MapMcp("/mcp");

              // Discovery-documenten voor MCP Server Cards (SEP-2127) + Agentic Resource
              // Discovery. De AI Catalog staat op de well-known URI en verwijst naar de
              // Server Card op de gereserveerde locatie <streamable-http-url>/server-card.
              // Static files serveert dotfile-mappen (.well-known) niet, dus mappen we
              // deze expliciet mét de door de spec vereiste media types, CORS en caching.
              endpoints.MapGet("/.well-known/ai-catalog.json",
                  context => ServeDiscoveryDoc(context, env, "ai-catalog.json", "application/ai-catalog+json"));

              endpoints.MapGet("/mcp/server-card",
                  context => ServeDiscoveryDoc(context, env, "mcp-server-card.json", "application/mcp-server-card+json"));
          });
      }

      // Serveert een discovery-document (AI Catalog / MCP Server Card) volgens de
      // eisen uit SEP-2127 docs/discovery.md: publieke CORS, cacheable, en 304 op
      // basis van een ETag zodat clients If-None-Match kunnen gebruiken.
      private static async Task ServeDiscoveryDoc(
          HttpContext context, IWebHostEnvironment env, string fileName, string contentType)
      {
          var path = Path.Combine(env.WebRootPath, "well-known", fileName);
          var etag = $"\"{fileName}-1.0.0\"";

          var response = context.Response;
          response.Headers.AccessControlAllowOrigin = "*";
          response.Headers.AccessControlAllowMethods = "GET";
          response.Headers.AccessControlAllowHeaders = "Content-Type, If-None-Match";
          response.Headers.AccessControlExposeHeaders = "ETag";
          response.Headers.ETag = etag;
          response.Headers.CacheControl = "public, max-age=3600";

          if (context.Request.Headers.IfNoneMatch == etag)
          {
              response.StatusCode = StatusCodes.Status304NotModified;
              return;
          }

          response.ContentType = contentType;
          await response.SendFileAsync(path);
      }
  }
}