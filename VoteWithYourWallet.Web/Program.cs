using Microsoft.EntityFrameworkCore;
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
            options.UseNpgsql(builder.Configuration.GetConnectionString("PrimaryContext"), npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(60 * 5); // 5 minute timeout
                    npgsqlOptions.EnableRetryOnFailure();
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