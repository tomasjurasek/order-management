using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("hardLimit", opt =>
    {
        opt.PermitLimit = 10;
        opt.Window = TimeSpan.FromSeconds(12);
    });
});


var app = builder.Build();

app.UseRateLimiter();
app.MapReverseProxy();

app.Run();