using FFXICustomDats.Data;
using FFXICustomDats.Data.XiDatEntities;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using DatEntities = FFXICustomDats.Data.XiDatEntities;
using YamlItems = FFXICustomDats.YamlModels.Items;

namespace FFXICustomDats
{
    public class WriteDats(IConfiguration config, XiDatContext datContext)
    {
        private readonly IConfiguration _config = config;
        private readonly XiDatContext _datContext = datContext;

        public void WriteYamlToDats()
        {
            GetDBItemsAndWriteYaml<ArmorItem>("armor.yml");
            GetDBItemsAndWriteYaml<ArmorItem>("armor2.yml");
            GetDBItemsAndWriteYaml<FurnishingItem>("general_items.yml");
            GetDBItemsAndWriteYaml<FurnishingItem>("general_items2.yml");
            GetDBItemsAndWriteYaml<PuppetItem>("puppet_items.yml");
            GetDBItemsAndWriteYaml<UsableItem>("usable_items.yml");
            GetDBItemsAndWriteYaml<WeaponItem>("weapons.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        private void GetDBItemsAndWriteYaml<T>(string fileName) where T : YamlItems.Item
        {
            var exportDatDir = _config.GetValue<string>("ExportDatDir");

            string itemYaml = string.Empty;

            var typeToFunc = new Dictionary<Type, Func<string, string>>
            {
                { typeof(ArmorItem), GetArmorItems },
                { typeof(FurnishingItem), GetGeneralItems },
                { typeof(PuppetItem), GetPuppetItems },
                { typeof(UsableItem), GetUsableItems },
                { typeof(WeaponItem), GetWeaponItems },
            };

            if (typeToFunc.TryGetValue(typeof(T), out var func))
            {
                itemYaml = func(fileName);
            }

            if (!string.IsNullOrWhiteSpace(itemYaml))
            {
                WriteYamlFile(itemYaml, $"new_{fileName}");
            }
        }

        private string GetArmorItems(string fileName)
        {
            Console.WriteLine($"Fetching info for {fileName}");

            var armorList = (from item in _datContext.Items.Where(x => x.DatFile == fileName)
                            join equipment in _datContext.ItemEquipments
                                on item.ItemId equals equipment.ItemId
                            join strings in _datContext.ItemStrings
                                on item.ItemId equals strings.ItemId
                            select new { item, equipment, strings }).ToList();

            return SerializeToYaml(new FFXIItems<ArmorItem>()
            {
                Items = [.. armorList.Select(x => new ArmorItem(x.item, x.strings, x.equipment))]
            });
        }

        private string GetGeneralItems(string fileName)
        {
            Console.WriteLine($"Fetching info for {fileName}");

            var itemList = (from item in _datContext.Items.Where(x => x.DatFile == fileName)
                            join strings in _datContext.ItemStrings
                                on item.ItemId equals strings.ItemId
                            join furnishing in _datContext.ItemFurnishings
                                on item.ItemId equals furnishing.ItemId
                             select new { item, furnishing, strings }).ToList();

            return SerializeToYaml(new FFXIItems<FurnishingItem>()
            {
                Items = [.. itemList.Select(x => new FurnishingItem(x.item, x.strings, x.furnishing))]
            });
        }

        private string GetPuppetItems(string fileName)
        {
            Console.WriteLine($"Fetching info for {fileName}");

            var itemList = (from item in _datContext.Items.Where(x => x.DatFile == fileName)
                            join strings in _datContext.ItemStrings
                                on item.ItemId equals strings.ItemId
                            join puppet in _datContext.ItemPuppets
                                on item.ItemId equals puppet.ItemId
                            select new { item, puppet, strings }).ToList();

            return SerializeToYaml(new FFXIItems<PuppetItem>()
            {
                Items = [.. itemList.Select(x => new PuppetItem(x.item, x.strings, x.puppet))]
            });
        }

        private string GetUsableItems(string fileName)
        {
            Console.WriteLine($"Fetching info for {fileName}");

            var itemList = (from item in _datContext.Items.Where(x => x.DatFile == fileName)
                            join strings in _datContext.ItemStrings
                                on item.ItemId equals strings.ItemId
                            join usable in _datContext.ItemUsables
                                on item.ItemId equals usable.ItemId
                            select new { item, usable, strings }).ToList();

            return SerializeToYaml(new FFXIItems<UsableItem>()
            {
                Items = [.. itemList.Select(x => new UsableItem(x.item, x.strings, x.usable))]
            });
        }

        private string GetWeaponItems(string fileName)
        {
            Console.WriteLine($"Fetching info for {fileName}");

            var itemList = (from item in _datContext.Items.Where(x => x.DatFile == fileName)
                            join strings in _datContext.ItemStrings
                                on item.ItemId equals strings.ItemId
                            join equipment in _datContext.ItemEquipments
                                on item.ItemId equals equipment.ItemId
                            join weapon in _datContext.ItemWeapons
                                on item.ItemId equals weapon.ItemId
                            select new { item, equipment, weapon, strings }).ToList();

            return SerializeToYaml(new FFXIItems<WeaponItem>()
            {
                Items = [.. itemList.Select(x => new WeaponItem(x.item, x.strings, x.equipment, x.weapon))]
            });
        }

        private static String SerializeToYaml<T>(FFXIItems<T> ffxiItems) where T : YamlItems.Item
        {
            Console.WriteLine($"Serializing");

            var serializer = new SerializerBuilder().Build();
            return serializer.Serialize(ffxiItems);
        }

        private void WriteYamlFile(string yaml, string fileName)
        {
            Console.WriteLine($"Writing file {fileName}");
            var generateYamlDir = _config.GetValue<string>("GenerateYamlDir");
            if (!Path.Exists(generateYamlDir))
            {
                Directory.CreateDirectory(generateYamlDir);
            }

            using StreamWriter writetext = new($@"{generateYamlDir}\{fileName}");
            writetext.Write(yaml);
        }
    }
}
