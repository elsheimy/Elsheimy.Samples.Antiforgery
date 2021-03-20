using Elsheimy.Samples.Antiforgery.SecureSample.AngularApp.Controllers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Elsheimy.Samples.Antiforgery.SecureSample.AngularApp
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
      services.AddControllersWithViews().AddRazorRuntimeCompilation();
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });

      services.AddAntiforgery(opts =>
      {
        opts.HeaderName = "X-XSRF-TOKEN";
      });

      services.AddHttpContextAccessor();
      services.AddTransient<ApiController>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      if (!env.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

  app.Use((context, next) =>
  {
    // return current accessed path
    string path = context.Request.Path.Value;

    if (path.IndexOf("/api/", StringComparison.OrdinalIgnoreCase) != -1)
    {
      var tokens = antiforgery.GetAndStoreTokens(context);
      context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
        new CookieOptions() { HttpOnly = false });
    }

    return next();
  });

      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });
    }
  }
}
