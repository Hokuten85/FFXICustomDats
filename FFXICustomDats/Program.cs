using FFXICustomDats;
using FFXICustomDats.Data;
using FFXICustomDats.Data.XiDatEntities;
using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static System.Reflection.Metadata.BlobBuilder;
using YamlItems = FFXICustomDats.YamlModels.Items;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.home.json", optional: true, reloadOnChange: true);
builder.Services.AddDbContext<XidbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("XidbConnectionString"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.23-mariadb"));
});
builder.Services.AddDbContext<XiDatContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("XiDatConnectionString"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.23-mariadb"));
});
builder.Services.AddTransient<CustomDats>();
builder.Services.AddTransient<ParseYaml>();
builder.Services.AddTransient<ExportDats>();
builder.Services.AddTransient<WriteDats>();
var host = builder.Build();

var app = host.Services.GetRequiredService<CustomDats>();
app.MainMenu();

