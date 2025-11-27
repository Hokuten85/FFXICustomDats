using FFXICustomDats;
using FFXICustomDats.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.home.json", optional: true, reloadOnChange: true);
builder.Services.AddDbContext<XidbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("XidbConnectionString"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.23-mariadb"));
});
builder.Services.AddTransient<CustomDats>();
builder.Services.AddTransient<PatchYaml>();
builder.Services.AddTransient<ExportDats>();
var host = builder.Build();

var app = host.Services.GetRequiredService<CustomDats>();
app.MainMenu();

