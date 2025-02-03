using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Radzen;
using VoteWithYourWallet.Web.Components;
using VoteWithYourWallet.Web.Services;

namespace VoteWithYourWallet.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Configuration.AddEnvironmentVariables();
        builder.Configuration.AddCommandLine(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        builder.Services.AddDbContextFactory<PrimaryContext>(options =>
                options
                    .UseSqlServer(builder.Configuration.GetConnectionString("PrimaryContext")!, ssOptions =>
                    {
                        ssOptions.CommandTimeout(60 * 60); // 1 hour timeout
                        ssOptions.EnableRetryOnFailure();
                    })
#if DEBUG
                    .EnableSensitiveDataLogging()
#endif
        );
        
        builder.Services.AddSingleton<ICacheService, CacheService>();
        
        builder.Services.AddRadzenComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}