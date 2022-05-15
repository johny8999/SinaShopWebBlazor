using FrameWork.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using SinaShop.Infrastructure.Core.Configuration;
using SinaShop.Infrastructure.Logger.SeriLoger;
using SinaShop.Infrastructure.Seed.Base.Main;
using SinaShop.WebApp.Authentication;
using SinaShop.WebApp.Config;
using SinaShop.WebApp.Middlewares;
using FrameWork.Consts;

var builder = WebApplication.CreateBuilder(args);
WebApplication app = null;

#region ConfigureServices
{

    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
    {
        builder.Host.UseSerilog_File();
    }
    else
    {
        builder.Host.UseSerilog_SqlServer();
    }
    builder.Services.AddAntiforgery(a=>a.HeaderName.Equals("XSRF-TOKEN"));

    builder.Services.AddCustomLocalization();

    builder.Services.AddRazorPage()
            .AddCustomViewLocalization()
            .AddCustomDataAnnotationLocalization(builder.Services);

    builder.Services.Config();
    builder.Services.AddInject();


    builder.Services.AddCustomIdentity();
    builder.Services.AddJwtAuthentication();
}
#endregion ConfigureServices

#region Configure
{
    app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseCustomLocalization();
    app.UseJWTAuthentication(AuthConst.CookieName,AuthConst.SecretKey);

    app.UseMiddleware<RedirectToValidLangMiddleware>();
    app.UseEndpoints(endpoints =>
    {

        endpoints.MapRazorPages();
    });
}
#endregion Configure

#region ConfigureSeed
{

    using (var ServiceScope = app.Services.CreateScope())
    {
        var Services = ServiceScope.ServiceProvider;
        try
        {
            var _SeedMain = Services.GetRequiredService<ISeed_main>();

            _SeedMain.RunAsync().Wait();
            //var q= _SeedMain.RunAsync().Result;   //for return result
        }
        catch (Exception ex)
        {
            var _Logger = Services.GetRequiredService<FrameWork.Infrastructure.ILogger>();
            _Logger.Fatal(ex);
        }
    }
}
#endregion ConfigureSeed
app.Run();