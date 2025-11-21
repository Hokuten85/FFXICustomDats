using FFXICustomDats.Data;
using FFXICustomDats.YamlModels.Items;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using DatEntities = FFXICustomDats.Data.XiDatEntities;
using YamlItems = FFXICustomDats.YamlModels.Items;

namespace FFXICustomDats
{
    public class WriteDats(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public async Task WriteYamlToDats()
        {
            await WriteNewArmor("armor.yml");
            await WriteNewArmor("armor2.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        private async Task WriteNewArmor(string fileName)
        {
            //var armorList = (from id in _context.ItemDescriptions.Where(x => x.DatFile == fileName)
            //                 join ib in _context.ItemBasics
            //                     on id.Itemid equals ib.Itemid into ibg
            //                 from ib_group in ibg.DefaultIfEmpty()
            //                 join iu in _context.ItemUsables
            //                     on id.Itemid equals iu.Itemid into iug
            //                 from iu_group in iug.DefaultIfEmpty()
            //                 join ie in _context.ItemEquipments
            //                     on id.Itemid equals ie.ItemId into ieg
            //                 from ie_group in ieg.DefaultIfEmpty()
            //                 join im in _context.ItemMods.Where(x => x.ModId == 276)
            //                     on id.Itemid equals im.ItemId into img
            //                 from im_group in img.DefaultIfEmpty()
            //                 select new
            //                 {
            //                     id,
            //                     ib_group,
            //                     iu_group,
            //                     ie_group,
            //                     im_group
            //                 }).ToList();

            //var armor = new FFXIItems<ArmorItem>()
            //{
            //    Items = [.. armorList.Select(x => new ArmorItem
            //    {
            //        Id = x.id.Itemid,
            //        Strings = new Strings()
            //        {
            //            Name = x.id.Name,
            //            ArticleType = Enum.Parse<ArticleType>(x.id.ArticleType),
            //            SingularName = x.id.SingularName,
            //            PluralName = x.id.PluralName,
            //            Description = x.id.Description,
            //        },
            //        Flags = FlagConversion.ConvertBitFlagsToYaml(x.ib_group?.Flags ?? 0, x.id.NoTradePc),
            //        StackSize = x.ib_group?.StackSize ?? 1,
            //        ItemType = Enum.Parse<ItemType>(x.id.ItemType),
            //        ResourceId = x.id.ResourceId,
            //        ValidTargets = ValidTargetConversion.ConvertBitValidTargetsToYaml(x.iu_group?.ValidTargets ?? 0, x.id.ValidTargetObject),
            //        Equipment = new Equipment()
            //        {
            //            Level = x.ie_group?.Level ?? 0,
            //            Slots = SlotConversion.ConvertBitSlotsToYaml(x.ie_group?.Slot ?? 0),
            //            Races = RaceConversion.ConvertBitRacesToYaml(x.im_group?.Value ?? 0),
            //            Jobs = JobConversion.ConvertBitJobsToYaml(x.ie_group?.Jobs ?? 0),
            //            SuperiorLevel = x.ie_group?.SuLevel ?? 0,
            //            ShieldSize = x.ie_group?.ShieldSize ?? 0,
            //            MaxCharges = x.iu_group?.MaxCharges ?? 0,
            //            CastingTime = x.iu_group?.Activation ?? 0,
            //            UseDelay = x.iu_group?.UseDelay ?? 0,
            //            ReuseDelay = x.iu_group?.ReuseDelay ?? 0,
            //            Unknown1 = x.id.Unknown1,
            //            Ilevel = x.ie_group?.Ilevel ?? 0,
            //            Unknown2 = x.id.Unknown2,
            //            Unknown3 = x.id.Unknown3,
            //        },
            //        IconBytes = Encoding.ASCII.GetString(x.id.IconBytes),
            //    })]
            //};    

            //var yaml = SerializeToYaml(armor);
            //WriteYamlFile(yaml, $"new_{fileName}");
        }

        private static String SerializeToYaml<T>(FFXIItems<T> ffxiItems) where T : YamlItems.Item
        {
            var serializer = new SerializerBuilder().Build();
            return serializer.Serialize(ffxiItems);
        }

        private void WriteYamlFile(string yaml, string fileName)
        {
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
