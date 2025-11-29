using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace FFXICustomDats
{
    public class PatchYaml(IConfiguration config, PatchItemsFromDB patch)
    {
        private readonly PatchItemsFromDB _patch = patch;
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
            Console.WriteLine($"Update Yaml from xidb {originalYamlFile}");
            var origFilePath = Path.Combine(_exportDatDir, originalYamlFile);
            var newFilePath = Path.Combine(_generateYamlDir, $"new_{originalYamlFile}");

            var updateFilePath = Path.Exists(newFilePath) ? newFilePath : origFilePath;
            if (Path.Exists(updateFilePath))
            {
                if (typeof(T) == typeof(ArmorItem))
                {
                    UpdateArmorItemFromDB(updateFilePath, originalYamlFile);
                }
                else if (typeof(T) == typeof(FurnishingItem))
                {
                    UpdateFurnishingItemFromDB(updateFilePath, originalYamlFile);
                }
                else if (typeof(T) == typeof(PuppetItem))
                {
                    UpdatePuppetItemFromDB(updateFilePath, originalYamlFile);
                }
                else if (typeof(T) == typeof(UsableItem))
                {
                    UpdateUsableItemFromDB(updateFilePath, originalYamlFile);
                }
                else if (typeof(T) == typeof(WeaponItem))
                {
                    UpdateWeaponItemFromDB(updateFilePath, originalYamlFile);
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

        private void UpdateWeaponItemFromDB(string updateFilePath, string orginalFileName)
        {
            var items = Helpers.DeserializeYaml<WeaponItem>(updateFilePath);
            _patch.UpdateWeaponItems(items.Items);

            SerializeAndWriteFile(items, orginalFileName);
        }

        private void UpdateUsableItemFromDB(string updateFilePath, string orginalFileName)
        {
            var items = Helpers.DeserializeYaml<UsableItem>(updateFilePath);
            _patch.UpdateUsableItems(items.Items);

            SerializeAndWriteFile(items, orginalFileName);
        }

        private void UpdatePuppetItemFromDB(string updateFilePath, string orginalFileName)
        {
            var items = Helpers.DeserializeYaml<PuppetItem>(updateFilePath);
            _patch.UpdatePuppetItems(items.Items);

            SerializeAndWriteFile(items, orginalFileName);
        }

        private void UpdateFurnishingItemFromDB(string updateFilePath, string orginalFileName)
        {
            var items = Helpers.DeserializeYaml<FurnishingItem>(updateFilePath);
            _patch.UpdateFurnishingItems(items.Items);

            SerializeAndWriteFile(items, orginalFileName);
        }

        private void UpdateArmorItemFromDB(string updateFilePath, string orginalFileName)
        {
            var items = Helpers.DeserializeYaml<ArmorItem>(updateFilePath);
            _patch.UpdateArmorItems(items.Items);

            SerializeAndWriteFile(items, orginalFileName);
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
