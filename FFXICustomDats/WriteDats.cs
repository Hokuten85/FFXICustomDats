using FFXICustomDats.Data;
using FFXICustomDats.Data.XiDatEntities;
using FFXICustomDats.YamlModels.Items;
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

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        private void GetDBItemsAndWriteYaml<T>(string fileName) where T : YamlItems.Item
        {
            var exportDatDir = _config.GetValue<string>("ExportDatDir");

            string itemYaml = string.Empty;
            if (typeof(T) == typeof(ArmorItem))
            {
                itemYaml = SerializeToYaml(GetArmorItems(fileName));
            }
            else if (typeof(T) == typeof(FurnishingItem))
            {
                itemYaml = SerializeToYaml(GetGeneralItems(fileName));
            }

            if (!string.IsNullOrWhiteSpace(itemYaml))
            {
                WriteYamlFile(itemYaml, $"new_{fileName}");
            }
        }

        private FFXIItems<ArmorItem> GetArmorItems(string fileName)
        {
            Console.WriteLine($"Fetching info for {fileName}");

            var armorList = (from item in _datContext.Items.Where(x => x.DatFile == fileName)
                            join equipment in _datContext.ItemEquipments
                                on item.ItemId equals equipment.ItemId
                            join strings in _datContext.ItemStrings
                                on item.ItemId equals strings.ItemId
                            select new { item, equipment, strings }).ToList();

            return new FFXIItems<ArmorItem>()
            {
                Items = [.. armorList.Select(x => new ArmorItem() {
                    Id = x.item.ItemId,
                    Strings = new Strings()
                    {
                        Name = x.strings.Name,
                        ArticleType = (ArticleType)x.strings.ArticleType,
                        SingularName = x.strings.SingularName,
                        PluralName = x.strings.PluralName,
                        Description = x.strings.Description,
                    },
                    Flags = Helpers.BitsToEnumList<Flag>(x.item.Flags),
                    StackSize = x.item.StackSize,
                    ItemType = (ItemType)x.item.ItemType,
                    ResourceId = x.item.ResourceId,
                    ValidTargets = Helpers.BitsToEnumList<ValidTarget>(x.item.ValidTargets),
                    Equipment = new Equipment()
                    {
                        Level = x.equipment.Level,
                        Slots = Helpers.BitsToEnumList<Slot>(x.equipment.Slot),
                        Races = [(Race)x.equipment.Races],
                        Jobs = Helpers.JobBitsToEnumList(x.equipment.Jobs),
                        SuperiorLevel = x.equipment.SuperiorLevel,
                        ShieldSize = x.equipment.ShieldSize,
                        MaxCharges = x.equipment.MaxCharges,
                        CastingTime = x.equipment.CastingTime,
                        UseDelay = x.equipment.UseDelay,
                        ReuseDelay = x.equipment.ReuseDelay,
                        Unknown1 = x.equipment.Unknown1,
                        Ilevel = x.equipment.ILevel,
                        Unknown2 = x.equipment.Unknown2,
                        Unknown3 = x.equipment.Unknown3,
                    },
                    IconBytes = Encoding.ASCII.GetString(x.item.IconBytes),
                })]
            };
        }

        private FFXIItems<FurnishingItem> GetGeneralItems(string fileName)
        {
            Console.WriteLine($"Fetching info for {fileName}");

            var itemList = (from item in _datContext.Items.Where(x => x.DatFile == fileName)
                            join strings in _datContext.ItemStrings
                                on item.ItemId equals strings.ItemId
                            join furnishing in _datContext.ItemFurnishings
                                on item.ItemId equals furnishing.ItemId
                             select new { item, furnishing, strings }).ToList();

            return new FFXIItems<FurnishingItem>()
            {
                Items = [.. itemList.Select(x => new FurnishingItem() {
                    Id = x.item.ItemId,
                    Strings = new Strings()
                    {
                        Name = x.strings.Name,
                        ArticleType = (ArticleType)x.strings.ArticleType,
                        SingularName = x.strings.SingularName,
                        PluralName = x.strings.PluralName,
                        Description = x.strings.Description,
                    },
                    Flags = Helpers.BitsToEnumList<Flag>(x.item.Flags),
                    StackSize = x.item.StackSize,
                    ItemType = (ItemType)x.item.ItemType,
                    ResourceId = x.item.ResourceId,
                    ValidTargets = Helpers.BitsToEnumList<ValidTarget>(x.item.ValidTargets),
                    Furnishing = new Furnishing() 
                    {
                        Element = (Element)x.furnishing.Element,
                        StorageSlots = x.furnishing.StorageSlots,
                        Unknown3 = x.furnishing.Unknown3
                    },
                    IconBytes = Encoding.ASCII.GetString(x.item.IconBytes),
                })]
            };
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
