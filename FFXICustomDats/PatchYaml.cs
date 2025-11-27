using FFXICustomDats.Data;
using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FFXICustomDats
{
    public class PatchYaml(IConfiguration config, XidbContext context)
    {
        private readonly XidbContext _context = context;
        private readonly IConfiguration _config = config;
        private readonly string _exportDatDir = config.GetValue<string>("ExportDatDir");
        private readonly string _generateYamlDir = config.GetValue<string>("GenerateYamlDir");
        private readonly string _yamlPatchDir = config.GetValue<string>("YamlPatchDir");

        public void PatchYamlFromXidb()
        {
            UpdateYamlFromDB<ArmorItem>("armor.yml");
            UpdateYamlFromDB<ArmorItem>("armor2.yml");
            UpdateYamlFromDB<FurnishingItem>("general_items.yml");
            UpdateYamlFromDB<FurnishingItem>("general_items2.yml");
            UpdateYamlFromDB<PuppetItem>("puppet_items.yml");
            UpdateYamlFromDB<UsableItem>("usable_items.yml");
            UpdateYamlFromDB<WeaponItem>("weapons.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        public void PatchYamlFromFiles()
        {
            UpdateYamlFromFile<ArmorItem>("armor.yml", "armor_patch.yml");
            UpdateYamlFromFile<ArmorItem>("armor2.yml", "armor2_patch.yml");
            UpdateYamlFromFile<FurnishingItem>("general_items.yml", "general_items_patch.yml");
            UpdateYamlFromFile<FurnishingItem>("general_items2.yml", "general_items2_patch.yml");
            UpdateYamlFromFile<PuppetItem>("puppet_items.yml", "puppet_items_patch.yml");
            UpdateYamlFromFile<UsableItem>("usable_items.yml", "usable_items_patch.yml");
            UpdateYamlFromFile<WeaponItem>("weapons.yml", "weapons_patch.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        public void ClearGenerateYamlDir()
        {
            foreach (FileInfo file in new DirectoryInfo(_generateYamlDir).EnumerateFiles())
            {
                file.Delete();
            }
        }

        private void UpdateYamlFromDB<T>(string originalYamlFile) where T : Item
        {
            Console.WriteLine($"Update Yaml from file xidb");
            var origFilePath = Path.Combine(_exportDatDir, originalYamlFile);
            var newFilePath = Path.Combine(_generateYamlDir, $"new_{originalYamlFile}");

            var updateFilePath = Path.Exists(newFilePath) ? newFilePath : origFilePath;
            if (Path.Exists(updateFilePath))
            {
                //var patchItems = GetItemsFromDB<ArmorItem>(items.Items.Select(x => x.Id));
                

                if (typeof(T) == typeof(ArmorItem))
                {
                    UpdateArmorItemFromDB(updateFilePath);
                }
                else if (typeof(T) == typeof(FurnishingItem))
                {

                }
                else if (typeof(T) == typeof(PuppetItem))
                {

                }
                else if (typeof(T) == typeof(UsableItem))
                {

                }
                else if (typeof(T) == typeof(WeaponItem))
                {

                }
            }
        }

        public void UpdateYamlFromFile<T>(string originalYamlFile, string patchFile) where T : Item
        {
            Console.WriteLine($"Update Yaml from file {originalYamlFile}");

            var origFilePath = Path.Combine(_exportDatDir, originalYamlFile);
            var newFilePath = Path.Combine(_generateYamlDir, $"new_{originalYamlFile}");
            var patchFilePath = Path.Combine(_yamlPatchDir, patchFile);

            var updateFilePath = Path.Exists(newFilePath) ? newFilePath : origFilePath;
            if (Path.Exists(updateFilePath) && Path.Exists(patchFilePath))
            {
                Console.WriteLine($"Patching {updateFilePath} with {patchFilePath}");
                var items = Helpers.DeserializeYaml<T>(origFilePath);
                var patchItems = Helpers.DeserializeYaml<T>(patchFilePath);

                DoPatchAndWriteFile(items, patchItems, originalYamlFile);
            }
        }

        private void DoPatchAndWriteFile<T>(FFXIItems<T> items, FFXIItems<T> patchItems, string originalYamlFile) where T : Item
        {
            Patch(items, patchItems);
            SerializeAndWriteFile(items, originalYamlFile);
        }

        private void SerializeAndWriteFile<T>(FFXIItems<T> items, string originalYamlFile) where T : Item
        {
            var yamlString = SerializeToYaml(items);
            if (!string.IsNullOrWhiteSpace(yamlString))
            {
                WriteNewYamlFile(yamlString, originalYamlFile);
            }
        }

        private static void Patch<T>(FFXIItems<T> items, FFXIItems<T> patchItems) where T : Item
        {
            foreach(var patchItem in patchItems.Items)
            {
                var item = items.Items.FirstOrDefault(x => x.Id == patchItem.Id);
                if (item != null)
                {
                    DeepCopy(item, patchItem);
                }
            }
        }

        private static void DeepCopy<T>(T original, T newItem) where T : Item
        {
            foreach (var prop in original.GetType().GetProperties())
            {
                if (!prop.GetValue(original).Equals(prop.GetValue(newItem))
                    && prop.GetValue(newItem) != (prop.GetType().IsValueType ? Activator.CreateInstance(prop.GetType()) : null))
                {
                    prop.SetValue(original, prop.GetValue(newItem));
                }
            }
        }

        private static String SerializeToYaml<T>(FFXIItems<T> ffxiItems) where T : Item
        {
            Console.WriteLine($"Serializing");

            var serializer = new SerializerBuilder().Build();
            return serializer.Serialize(ffxiItems);
        }

        private void WriteNewYamlFile(string yaml, string fileName)
        {
            Console.WriteLine($"Writing file {fileName}");
            if (!Path.Exists(_generateYamlDir))
            {
                Directory.CreateDirectory(_generateYamlDir);
            }

            using StreamWriter writetext = new($@"{_generateYamlDir}\new_{fileName}");
            writetext.Write(yaml);
        }

        private void UpdateArmorItemFromDB(string updateFilePath)
        {
            var items = Helpers.DeserializeYaml<ArmorItem>(updateFilePath);
            UpdateArmorItems(items);

            SerializeAndWriteFile(items, new FileInfo(updateFilePath).Name);
        }

        private void UpdateArmorItems(FFXIItems<ArmorItem> items)
        {
            var itemIds = items.Items.Select(i => i.Id);

            var dbItems = (from itemBasic in _context.ItemBasics.Where(x => itemIds.Contains(x.Itemid))
                        join equipment in _context.ItemEquipments
                            on itemBasic.Itemid equals equipment.ItemId
                        join r in _context.ItemMods.Where(x => x.ModId == 276) //EQUIPMENT_ONLY_RACE
                            on itemBasic.Itemid equals r.ItemId into rg
                        from race in rg.DefaultIfEmpty()
                        join u in _context.ItemUsables
                            on itemBasic.Itemid equals u.Itemid into ug
                        from usable in ug.DefaultIfEmpty()
                        select new { itemBasic, equipment, race, usable }).ToList();

            foreach (var item in items.Items)
            {
                var dbItem = dbItems.FirstOrDefault(x => x.itemBasic.Itemid == item.Id);
                if (dbItem != null)
                {
                    UpdateItemBasic(item, dbItem.itemBasic);
                    UpdateItemEquipment(item, dbItem.equipment, dbItem.race, dbItem.usable);
                }
            }
        }

        private void UpdateItemEquipment(ArmorItem item, ItemEquipment itemEquipment, ItemMod race, ItemUsable usable)
        {
            if (item.Equipment.Level != itemEquipment.Level)
            {
                item.Equipment.Level = itemEquipment.Level;
            }

            if (!SlotHelpers.IsEqual(item.Equipment.Slots, itemEquipment.Slot))
            {
                item.Equipment.Slots = Helpers.DBFlagsToYamlFlags(SlotHelpers.SlotMap, itemEquipment.Slot);
            }

            if (!JobHelpers.IsEqual(item.Equipment.Jobs, itemEquipment.Jobs))
            {
                item.Equipment.Jobs = JobHelpers.DBFlagsToYamlFlags(itemEquipment.Jobs);
            }

            if (item.Equipment.SuperiorLevel != itemEquipment.SuLevel)
            {
                item.Equipment.SuperiorLevel = itemEquipment.SuLevel;
            }

            if (item.Equipment.ShieldSize != itemEquipment.ShieldSize)
            {
                item.Equipment.ShieldSize = itemEquipment.ShieldSize;
            }

            if (item.Equipment.Ilevel != itemEquipment.Ilevel)
            {
                item.Equipment.Ilevel = itemEquipment.Ilevel;
            }

            if (race != null && _config.GetValue<bool>("TrustDatabaseRace")
                && Helpers.ConvertEnumListToBit(item.Equipment.Races) != race.Value)
            {
                item.Equipment.Races = Helpers.BitsToEnumList<Race>((ushort)race.Value);
            }

            if (usable != null)
            {
                //validtargets
                //maxcharges
                //castingtime
                //usedelay
                //reusedelay
            }
        }

        private void UpdateItemBasic(Item item, ItemBasic itemBasic)
        {
            if (!FlagHelpers.IsEqual(item.Flags, itemBasic.Flags))
            {
                item.Flags = Helpers.DBFlagsToYamlFlags(FlagHelpers.FlagMap, itemBasic.Flags);
            }

            if (item.StackSize != itemBasic.StackSize)
            {
                item.StackSize = itemBasic.StackSize;
            }
        }

        private static class ObjectComparerUtility
        {
            public static bool ObjectsAreEqual<T>(T obj1, T obj2, bool makeEqual = false)
            {
                var areEqual = true;
                foreach (var prop in obj1.GetType().GetProperties())
                {
                    if (!prop.GetValue(obj1).Equals(prop.GetValue(obj2)))
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
    }
}
