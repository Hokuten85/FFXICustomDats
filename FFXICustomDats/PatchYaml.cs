using FFXICustomDats.YamlModels;
using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace FFXICustomDats
{
    public class PatchYaml(IConfiguration config, PatchItemsFromDB patch)
    {
        private readonly PatchItemsFromDB _patch = patch;
        private readonly string _rawData = config.GetValue<string>("RawData") ?? string.Empty;
        private readonly string _originalData = config.GetValue<string>("OriginalData") ?? string.Empty;
        private readonly string _yamlPatches = config.GetValue<string>("YamlPatches") ?? string.Empty;

        public void PatchYamlFromXidb()
        {
            UpdateYamlFromDB<ArmorItem>(@"items\armor.yml");
            UpdateYamlFromDB<ArmorItem>(@"items\armor2.yml");
            UpdateYamlFromDB<FurnishingItem>(@"items\general_items.yml");
            UpdateYamlFromDB<FurnishingItem>(@"items\general_items2.yml");
            UpdateYamlFromDB<PuppetItem>(@"items\puppet_items.yml");
            UpdateYamlFromDB<UsableItem>(@"items\usable_items.yml");
            UpdateYamlFromDB<WeaponItem>(@"items\weapons.yml");
            UpdateYamlFromDB(@"data_menu.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        public void PatchYamlFromFiles()
        {
            UpdateYamlFromFile<ArmorItem>(@"items\armor.yml", @"items\armor_patch.yml");
            UpdateYamlFromFile<ArmorItem>(@"items\armor2.yml", @"items\armor2_patch.yml");
            UpdateYamlFromFile<FurnishingItem>(@"items\general_items.yml", @"items\general_items_patch.yml");
            UpdateYamlFromFile<FurnishingItem>(@"items\general_items2.yml", @"items\general_items2_patch.yml");
            UpdateYamlFromFile<PuppetItem>(@"items\puppet_items.yml", @"items\puppet_items_patch.yml");
            UpdateYamlFromFile<UsableItem>(@"items\usable_items.yml", @"items\usable_items_patch.yml");
            UpdateYamlFromFile<WeaponItem>(@"items\weapons.yml", @"items\weapons_patch.yml");
            UpdateYamlFromFile(@"data_menu.yml", @"data_menu_patch.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        public void ClearRawDataDir()
        {
            foreach (FileInfo file in new DirectoryInfo(_rawData).EnumerateFiles())
            {
                file.Delete();
            }
        }

        private void UpdateYamlFromDB<T>(string fileName) where T : Item
        {
            Console.WriteLine($"Update Yaml from xidb {fileName}");
            var origFilePath = Path.Combine(_originalData, fileName);
            var newFilePath = Path.Combine(_rawData, fileName);

            var updateFilePath = Path.Exists(newFilePath) ? newFilePath : origFilePath;
            if (Path.Exists(updateFilePath))
            {
                UpdateItemsFromDB<T>(updateFilePath, newFilePath);
            }
        }

        private void UpdateYamlFromDB(string fileName)
        {
            Console.WriteLine($"Update Yaml from xidb {fileName}");
            var origFilePath = Path.Combine(_originalData, fileName);
            var newFilePath = Path.Combine(_rawData, fileName);

            var updateFilePath = Path.Exists(newFilePath) ? newFilePath : origFilePath;
            if (Path.Exists(updateFilePath))
            {
                UpdateItemsFromDB(updateFilePath, newFilePath);
            }
        }

        public void UpdateYamlFromFile<T>(string fileName, string patchFile) where T : Item
        {
            Console.WriteLine($"Update Yaml from file {fileName}");
            var (updateFilePath, patchFilePath, newFilePath) = GetFilePaths(fileName, patchFile);

            if (Path.Exists(updateFilePath) && Path.Exists(patchFilePath))
            {
                Console.WriteLine($"Patching {updateFilePath} with {patchFilePath}");
                DoPatchAndWriteFile<T>(updateFilePath, patchFilePath, newFilePath);
            }
        }

        public void UpdateYamlFromFile(string fileName, string patchFile)
        {
            Console.WriteLine($"Update Yaml from file {fileName}");
            var (updateFilePath, patchFilePath, newFilePath) = GetFilePaths(fileName, patchFile);

            if (Path.Exists(updateFilePath) && Path.Exists(patchFilePath))
            {
                Console.WriteLine($"Patching {updateFilePath} with {patchFilePath}");
                DoPatchAndWriteFile(updateFilePath, patchFilePath, newFilePath);
            }
        }

        private (string, string, string) GetFilePaths(string fileName, string patchFile)
        {
            var origFilePath = Path.Combine(_originalData, fileName);
            var newFilePath = Path.Combine(_rawData, fileName);
            var patchFilePath = Path.Combine(_yamlPatches, patchFile);

            var updateFilePath = Path.Exists(newFilePath) ? newFilePath : origFilePath;

            return (updateFilePath, patchFilePath, newFilePath);
        }

        private static void DoPatchAndWriteFile<T>(string updateFilePath, string patchFilePath, string filePath) where T : Item
        {
            var items = Helpers.DeserializeYaml<T>(updateFilePath);
            var patchItems = Helpers.DeserializeYaml<T>(patchFilePath);

            Patch(items, patchItems);
            SerializeAndWriteFile(items, filePath);
        }

        private static void DoPatchAndWriteFile(string updateFilePath, string patchFilePath, string filePath)
        {
            var dataMenu = Helpers.DeserializeYaml(updateFilePath);
            var patchDataMenu = Helpers.DeserializeYaml(patchFilePath);

            Patch(dataMenu, patchDataMenu);
            SerializeAndWriteFile(dataMenu, filePath);
        }

        private static void SerializeAndWriteFile(object thing, string filePath)
        {
            var yamlString = SerializeToYaml(thing);
            if (!string.IsNullOrWhiteSpace(yamlString))
            {
                WriteNewYamlFile(yamlString, filePath);
            }
        }

        private static void Patch<T>(XIItems<T> items, XIItems<T> patchItems) where T : Item
        {
            foreach (var patchItem in patchItems.Items)
            {
                var item = items.Items.FirstOrDefault(x => x.Id == patchItem.Id);
                if (item != null)
                {
                    DeepCopy(item, patchItem);
                }
            }
        }

        private static void Patch(XIDataMenu dataMenu, XIDataMenu patchDataMenu)
        {
            var patchEntries = patchDataMenu.Sections.FirstOrDefault(x => x.Type == SectionType.Mgc_)?.Entries;
            var originalEntries = dataMenu.Sections.FirstOrDefault(x => x.Type == SectionType.Mgc_)?.Entries;
            if (patchEntries != null && originalEntries != null)
            {
                foreach (var patchDM in patchEntries.EntryList)
                {
                    var originalDM = originalEntries.EntryList.FirstOrDefault(x => x.Id == patchDM.Id);
                    if (originalDM != null)
                    {
                        DeepCopy(originalDM, patchDM);
                    }
                }
            }
        }

        private static void DeepCopy<T>(T original, T newItem) where T : class
        {
            foreach (var prop in original.GetType().GetProperties())
            {
                if (!(prop.GetValue(original) == prop.GetValue(newItem))
                    && prop.GetValue(newItem) != (prop.GetType().IsValueType ? Activator.CreateInstance(prop.GetType()) : null))
                {
                    prop.SetValue(original, prop.GetValue(newItem));
                }
            }
        }

        private static String SerializeToYaml(object thing)
        {
            Console.WriteLine($"Serializing");

            var serializer = new SerializerBuilder().Build();
            return serializer.Serialize(thing);
        }

        private static void WriteNewYamlFile(string yaml, string filePath)
        {
            Console.WriteLine($"Writing file {filePath}");
            if (!Path.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);
            }

            using StreamWriter writetext = new(filePath);
            writetext.Write(yaml);
            writetext.Flush();
            writetext.Close();
            writetext.Dispose();
        }

        private void UpdateItemsFromDB<T>(string updateFilePath, string newFilePath) where T : Item
        {
            var items = Helpers.DeserializeYaml<T>(updateFilePath);

            if (typeof(T) == typeof(ArmorItem))
            {
                _patch.UpdateArmorItems(items.Items as ArmorItem[] ?? []);
            }
            else if (typeof(T) == typeof(FurnishingItem))
            {
                _patch.UpdateFurnishingItems(items.Items as FurnishingItem[] ?? []);
            }
            else if (typeof(T) == typeof(PuppetItem))
            {
                _patch.UpdatePuppetItems(items.Items as PuppetItem[] ?? []);
            }
            else if (typeof(T) == typeof(UsableItem))
            {
                _patch.UpdateUsableItems(items.Items as UsableItem[] ?? []);
            }
            else if (typeof(T) == typeof(WeaponItem))
            {
                _patch.UpdateWeaponItems(items.Items as WeaponItem[] ?? []);
            }

            SerializeAndWriteFile(items, newFilePath);
        }

        private void UpdateItemsFromDB(string updateFilePath, string newFilePath)
        {
            var misc = Helpers.DeserializeYaml(updateFilePath);

            SerializeAndWriteFile(misc, newFilePath);
        }
    }
}
