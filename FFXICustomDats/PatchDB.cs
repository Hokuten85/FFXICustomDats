using FFXICustomDats.Data;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace FFXICustomDats
{
    public class PatchDB(IConfiguration config, PatchDBFromItems patch)
    {
        private readonly string _originalData = config.GetValue<string>("OriginalData") ?? string.Empty;
        private readonly PatchDBFromItems _patch = patch;

        public void PatchXidbFromYaml()
        {
            UpdateDB<ArmorItem>(@"items\armor.yml");
            UpdateDB<ArmorItem>(@"items\armor2.yml");
            UpdateDB<FurnishingItem>(@"items\general_items.yml");
            UpdateDB<FurnishingItem>(@"items\general_items2.yml");
            UpdateDB<PuppetItem>(@"items\puppet_items.yml");
            UpdateDB<UsableItem>(@"items\usable_items.yml");
            UpdateDB<WeaponItem>(@"items\weapons.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        private void UpdateDB<T>(string fileName) where T : Item
        {
            Console.WriteLine($"Update Yaml from xidb {fileName}");
            var origFilePath = Path.Combine(_originalData, fileName);

            if (Path.Exists(origFilePath))
            {
                UpdateDBFromItems<T>(origFilePath);
            }
        }

        private void UpdateDBFromItems<T>(string updateFilePath) where T : Item
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
        }
    }
}
