using LinkShortener.Backgrounds;
using LinkShortener.Services;
using LinkShortenerDatabaseLib;
using LinkShortenerDatabaseLib.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration["POSTGRES_CONNECTION"]);
});

builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<IScopedProcessingService, LinksScopedProcessingService>();

builder.Services.AddSingleton<ILinkGenerator, LinkShortener.Services.LinkGenerator>();

builder.Services.AddHostedService<LinksCleaner>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
