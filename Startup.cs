using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emison.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using DinkToPdf.Contracts;
using DinkToPdf;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Emison
{
  public class Startup
  {
    private readonly IHostingEnvironment _env;
    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
      _env = env;
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
          Configuration.GetConnectionString("DefaultConnection")));

      services.AddDefaultIdentity<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddDefaultUI(UIFramework.Bootstrap4)
        .AddEntityFrameworkStores<ApplicationDbContext>();

      var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
      var wkHtmlToPdfPath = Path.Combine(_env.ContentRootPath, $"wkhtmltox/v0.12.4/{architectureFolder}/libwkhtmltox.{GetExtension()}");
      var assemblyContext = new CustomAssemblyLoadContext();
      assemblyContext.LoadUnmanagedLibrary(wkHtmlToPdfPath);

      services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

      services.AddMvc(config =>
      {
        var policy = new AuthorizationPolicyBuilder()
         .RequireAuthenticatedUser()
         .Build();
        config.Filters.Add(new AuthorizeFilter(policy));
      }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    private string GetExtension()
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        return "dylib";

      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        return "so";

      return "dll";
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error"); // TODO: check that
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "areas",
          template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
