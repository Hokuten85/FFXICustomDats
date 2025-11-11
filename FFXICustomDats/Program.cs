using FFXICustomDats.Data;
using FFXICustomDats.Data.Entities;
using FFXICustomDats.DatModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Diagnostics;
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

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.home.json", optional: true, reloadOnChange: true);
builder.Services.AddDbContext<XidbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("XidbConnectionString"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.23-mariadb"));
});
builder.Services.AddTransient<BuildDats>();
var host = builder.Build();

var app = host.Services.GetRequiredService<BuildDats>();
await app.MainMenu();

public class BuildDats
{
    private readonly IConfiguration _config;
    private readonly XidbContext _context;
    public BuildDats(IConfiguration config, XidbContext context)
    {
        _config = config;
        _context = context;
    }
    
    public async Task MainMenu()
    {
        bool endApp = false;

        while (!endApp)
        {
            Console.Clear();
            Console.WriteLine("FFXI Dat Generator");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("\t1 - Parse Dats and Populate xidb tables");
            Console.WriteLine("\t2 - Read xidb tables and create Dats");
            Console.WriteLine("\t3 - Quit");
            Console.WriteLine();
            Console.WriteLine("Type a number, and then press Enter:");

            if (int.TryParse(Console.ReadLine(), out int numInput))
            {
                switch (numInput)
                {
                    case 1:
                        await ParseDats();
                        break;
                    case 2:
                        await WriteDats();
                        break;
                    case 3:
                        endApp = true;
                        break;
                }
            }
        }
    }

    private async Task ParseDats()
    {
        await ParseItems(@"C:\XI_Tinkerer\raw_data\items\armor.yml");
        await ParseItems(@"C:\XI_Tinkerer\raw_data\items\armor2.yml");
        await ParseItems(@"C:\XI_Tinkerer\raw_data\items\general_items.yml");
        await ParseItems(@"C:\XI_Tinkerer\raw_data\items\general_items2.yml");
        Console.WriteLine();
    }

    private async Task WriteDats()
    {
        await WriteNewArmor(@"C:\XI_Tinkerer\raw_data\items\new_armor.yml");
        Console.WriteLine();
    }

    private async Task ParseItems(string filePath)
    {
        FileStream fileStream = new(filePath, FileMode.Open);
        using var reader = new StreamReader(fileStream);

        var input = new StringReader(reader.ReadToEnd());
        var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

        var ffxiItems = deserializer.Deserialize<FFXIItems>(input);

        await UpsertItemDescription(ffxiItems, filePath);
        await UpdateItemBasic(ffxiItems);
    }

    private async Task UpsertItemDescription(FFXIItems ffxiItems, string filePath)
    {
        FileInfo fileInfo = new(filePath);

        foreach (var item in ffxiItems.Items)
        {
            var dbItem = _context.ItemDescriptions.FirstOrDefault(x => x.Itemid == item.Id);
            var newItem = new ItemDescription()
            {
                Itemid = item.Id,
                Name = item.Strings.Name,
                ArticleType = item.Strings.ArticleType.ToString(),
                SingularName = item.Strings.SingularName,
                PluralName = item.Strings.PluralName,
                Description = item.Strings.Description,
                ItemType = item.ItemType.ToString(),
                ResourceId = item.ResourceId,
                NoTradePc = item.Flags.Any(x => x.Equals(Flag.NoTradePC)),
                Unknown1 = item.Equipment.Unknown1,
                Unknown2 = item.Equipment.Unknown2,
                Unknown3 = item.Equipment.Unknown3,
                IconBytes = Encoding.ASCII.GetBytes(item.IconBytes),
                DatFile = fileInfo.Name
            };

            if (dbItem == null)
            {
                _context.ItemDescriptions.Add(newItem);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newItem, makeEqual: true))
            {
                _context.ItemDescriptions.Update(dbItem);
            }
        }

        await _context.SaveChangesAsync();
    }

    private async Task UpdateItemBasic(FFXIItems ffxiItems)
    {
        foreach (var item in ffxiItems.Items)
        {
            var itemBasic = _context.ItemBasics.FirstOrDefault(x => x.Itemid == item.Id);
            if (itemBasic != null)
            {
                var newIB = new ItemBasic()
                {
                    Itemid = item.Id,
                    Subid = itemBasic.Subid,
                    Name = itemBasic.Name,
                    Sortname = itemBasic.Sortname,
                    Type = itemBasic.Type,
                    StackSize = item.StackSize,
                    Flags = FlagConversion.ConvertYamlFlagsToBit(item.Flags),
                    AH = itemBasic.AH,
                    BaseSell = itemBasic.BaseSell,
                };

                if (!ObjectComparerUtility.ObjectsAreEqual(itemBasic, newIB, makeEqual: true))
                {
                    _context.ItemBasics.Update(itemBasic);
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    private async Task WriteNewArmor(string filePath)
    {
        var armorList = (from id in _context.ItemDescriptions.Where(x => x.DatFile == "armor.yml")
                     join ib in _context.ItemBasics
                         on id.Itemid equals ib.Itemid
                     join iu in _context.ItemUsables
                         on id.Itemid equals iu.Itemid into iug
                     from iu_group in iug.DefaultIfEmpty()
                     join ie in _context.ItemEquipments
                         on id.Itemid equals ie.ItemId into ieg
                     from ie_group in ieg.DefaultIfEmpty()
                     join im in _context.ItemMods.Where(x => x.ModId == 276)
                         on id.Itemid equals im.ItemId into img
                     from im_group in img.DefaultIfEmpty()
                     select new
                     {
                         id,
                         ib,
                         iu_group,
                         ie_group,
                         im_group
                     }).ToList();

        var armor = armorList.Select(x => new Armor
        {
            Id = x.id.Itemid,
            Strings = new Strings()
            {
                Name = x.id.Name,
                ArticleType = Enum.Parse<ArticleType>(x.id.ArticleType),
                SingularName = x.id.SingularName,
                PluralName = x.id.PluralName,
                Description = x.id.Description,
            },
            Flags = FlagConversion.ConvertBitFlagsToYaml(x.ib.Flags, x.id.NoTradePc),
            StackSize = x.ib.StackSize,
            ItemType = Enum.Parse<ItemType>(x.id.ItemType),
            ResourceId = x.id.ResourceId,
            ValidTargets = ValidTargetConversion.ConvertBitValidTargetsToYaml(x.iu_group?.ValidTargets ?? 0),
            Equipment = new Equipment()
            {
                Level = x.ie_group?.Level ?? 0,
                Slots = SlotConversion.ConvertBitSlotsToYaml(x.ie_group?.Slot ?? 0),
                Races = RaceConversion.ConvertBitRacesToYaml(x.im_group?.Value ?? 0),
                Jobs = JobConversion.ConvertBitJobsToYaml(x.ie_group?.Jobs ?? 0),
                SuperiorLevel = x.ie_group?.SuLevel ?? 0,
                ShieldSize = x.ie_group?.ShieldSize ?? 0,
                MaxCharges = x.iu_group?.MaxCharges ?? 0,
                CastingTime = x.iu_group?.Activation ?? 0,
                UseDelay = x.iu_group?.UseDelay ?? 0,
                ReuseDelay = x.iu_group?.ReuseDelay ?? 0,
                Unknown1 = x.id.Unknown1,
                Ilevel = x.ie_group?.Ilevel ?? 0,
                Unknown2 = x.id.Unknown2,
                Unknown3 = x.id.Unknown3,
            },
            IconBytes = Encoding.ASCII.GetString(x.id.IconBytes),
        });

        await SerializeToYaml(new FFXIItems() { Items = [.. armor] });
    }

    private static async Task<String> SerializeToYaml(FFXIItems ffxiItems)
    {
        var serializer = new SerializerBuilder().Build();
        var yaml = serializer.Serialize(ffxiItems);

        using StreamWriter writetext = new(@"C:\XI_Tinkerer\raw_data\items\new_armor.yml");
        await writetext.WriteAsync(yaml);

        return yaml;
    }
}

public static class ObjectComparerUtility
{
    public static bool ObjectsAreEqual<T>(T obj1, T obj2, bool makeEqual = false)
    {
        var areEqual = true;
        foreach (var prop in obj1.GetType().GetProperties())
        {
            if (prop.GetValue(obj1) != prop.GetValue(obj2))
            {
                areEqual = false;

                if (makeEqual)
                {
                    prop.SetValue(obj1, prop.GetValue(obj2));
                }
            }
        }

        return areEqual;
    }
}



