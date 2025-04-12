using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VoteWithYourWallet.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<PrimaryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PrimaryContext"), npgsqlOptions =>
        {
            npgsqlOptions.CommandTimeout(60 * 5); // 5 minute timeout
            npgsqlOptions.EnableRetryOnFailure();
        })
#if DEBUG
        .EnableSensitiveDataLogging()
#endif
    );

builder.Services.AddMemoryCache();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Compression does not work with only server configuration.
builder.Services.AddRequestDecompression();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var app = builder.Build();

// Compression does not work with only server configuration.
app.UseRequestDecompression();
app.UseResponseCompression();

app.UseForwardedHeaders();

app.UseRouting();

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("*");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

//app.UseHttpsRedirection();

app.Run();
