using FFXICustomDats.YamlConverters;
using FFXICustomDats.YamlModels;
using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace FFXICustomDats
{
    public class PatchYaml(IConfiguration config, PatchItemsFromDB patch, PatchDataMenuFromDB patchDM)
    {
        private readonly PatchItemsFromDB _patch = patch;
        private readonly PatchDataMenuFromDB _patchDM = patchDM;
        private readonly string _rawData = config.GetValue<string>("RawData") ?? string.Empty;
        private readonly string _originalData = config.GetValue<string>("OriginalData") ?? string.Empty;
        private readonly string _yamlPatches = config.GetValue<string>("YamlPatches") ?? string.Empty;

        public void PatchYamlFromXidb()
        {
            UpdateItemYamlFromDB<ArmorItem>(@"items\armor.yml");
            UpdateItemYamlFromDB<ArmorItem>(@"items\armor2.yml");
            UpdateItemYamlFromDB<FurnishingItem>(@"items\general_items.yml");
            UpdateItemYamlFromDB<FurnishingItem>(@"items\general_items2.yml");
            UpdateItemYamlFromDB<PuppetItem>(@"items\puppet_items.yml");
            UpdateItemYamlFromDB<UsableItem>(@"items\usable_items.yml");
            UpdateItemYamlFromDB<WeaponItem>(@"items\weapons.yml");
            UpdateDataMenuYamlFromDB(@"data_menu.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        public void PatchYamlFromFiles()
        {
            UpdateItemYamlFromFile<ArmorItem>(@"items\armor.yml", @"items\armor_patch.yml");
            UpdateItemYamlFromFile<ArmorItem>(@"items\armor2.yml", @"items\armor2_patch.yml");
            UpdateItemYamlFromFile<FurnishingItem>(@"items\general_items.yml", @"items\general_items_patch.yml");
            UpdateItemYamlFromFile<FurnishingItem>(@"items\general_items2.yml", @"items\general_items2_patch.yml");
            UpdateItemYamlFromFile<PuppetItem>(@"items\puppet_items.yml", @"items\puppet_items_patch.yml");
            UpdateItemYamlFromFile<UsableItem>(@"items\usable_items.yml", @"items\usable_items_patch.yml");
            UpdateItemYamlFromFile<WeaponItem>(@"items\weapons.yml", @"items\weapons_patch.yml");
            UpdateDataMenuYamlFromFile(@"data_menu.yml", @"data_menu_patch.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        public void ClearRawDataDir()
        {
            foreach (DirectoryInfo directory in new DirectoryInfo(_rawData).EnumerateDirectories())
            {
                directory.Delete(true);
            }

            foreach (FileInfo file in new DirectoryInfo(_rawData).EnumerateFiles())
            {
                file.Delete();
            }
        }

        private void UpdateItemYamlFromDB<T>(string fileName) where T : Item
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

        private void UpdateDataMenuYamlFromDB(string fileName)
        {
            Console.WriteLine($"Update Yaml from xidb {fileName}");
            var origFilePath = Path.Combine(_originalData, fileName);
            var newFilePath = Path.Combine(_rawData, fileName);

            var updateFilePath = Path.Exists(newFilePath) ? newFilePath : origFilePath;
            if (Path.Exists(updateFilePath))
            {
               UpdateDataMenuFromDB(updateFilePath, newFilePath);
            }
        }

        public void UpdateItemYamlFromFile<T>(string fileName, string patchFile) where T : Item
        {
            Console.WriteLine($"Update Yaml from file {fileName}");
            var (updateFilePath, patchFilePath, newFilePath) = GetFilePaths(fileName, patchFile);

            if (Path.Exists(updateFilePath) && Path.Exists(patchFilePath))
            {
                Console.WriteLine($"Patching {updateFilePath} with {patchFilePath}");
                DoPatchAndWriteFile<T>(updateFilePath, patchFilePath, newFilePath);
            }
        }

        public void UpdateDataMenuYamlFromFile(string fileName, string patchFile)
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
            var yamlString = Helpers.SerializeToYaml(thing);
            if (!string.IsNullOrWhiteSpace(yamlString))
            {
                Helpers.WriteNewYamlFile(yamlString, filePath);
            }
        }

        private static void Patch<T>(XIItems<T> items, XIItems<T> patchItems) where T : Item
        {
            foreach (var patchItem in patchItems.Items)
            {
                var item = items.Items.FirstOrDefault(x => x.Id == patchItem.Id);
                if (item != null)
                {
                    Helpers.DeepCopy(item, patchItem);
                }
            }
        }

        private static void Patch(XIDataMenu dataMenu, XIDataMenu patchDataMenu)
        {
            var patchEntries = patchDataMenu.Sections.FirstOrDefault(x => x.Type == SectionType.Mgc_)?.Entries;
            var originalEntries = dataMenu.Sections.FirstOrDefault(x => x.Type == SectionType.Mgc_)?.Entries;
            if (patchEntries != null && originalEntries != null)
            {
                foreach (var patchDM in patchEntries.SpellList)
                {
                    var originalDM = originalEntries.SpellList.FirstOrDefault(x => x.Id == patchDM.Id);
                    if (originalDM != null)
                    {
                        Helpers.DeepCopy(originalDM, patchDM);
                    }
                }
            }
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

        private void UpdateDataMenuFromDB(string updateFilePath, string newFilePath)
        {
            var misc = Helpers.DeserializeYaml(updateFilePath);

            var entries = misc.Sections.FirstOrDefault(x => x.Type == SectionType.Mgc_)?.Entries;
            if (entries != null)
            {
                _patchDM.UpdateSpells(entries.SpellList);
            }

            SerializeAndWriteFile(misc, newFilePath);
        }
    }
}
