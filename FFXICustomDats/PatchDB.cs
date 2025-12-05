using FFXICustomDats.Data;
using FFXICustomDats.PatchItems;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace FFXICustomDats
{
    public class PatchDB(IConfiguration config, PatchDBFromItems patch, PatchDBFromDataMenu patchDM)
    {
        private readonly string _originalData = config.GetValue<string>("OriginalData") ?? string.Empty;
        private readonly PatchDBFromItems _patch = patch;
        private readonly PatchDBFromDataMenu _patchDM = patchDM;

        public void PatchXidbFromYaml()
        {
            UpdateItemDB<ArmorItem>(@"items\armor.yml");
            UpdateItemDB<ArmorItem>(@"items\armor2.yml");
            UpdateItemDB<FurnishingItem>(@"items\general_items.yml");
            UpdateItemDB<FurnishingItem>(@"items\general_items2.yml");
            UpdateItemDB<PuppetItem>(@"items\puppet_items.yml");
            UpdateItemDB<UsableItem>(@"items\usable_items.yml");
            UpdateItemDB<WeaponItem>(@"items\weapons.yml");
            UpdateSpellDB(@"data_menu.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        private void UpdateItemDB<T>(string fileName) where T : Item
        {
            Console.WriteLine($"Update DB from yaml {fileName}");
            var origFilePath = Path.Combine(_originalData, fileName);

            if (Path.Exists(origFilePath))
            {
                UpdateDBFromItems<T>(origFilePath);
            }
        }

        private void UpdateSpellDB(string fileName)
        {
            Console.WriteLine($"Update DB from yaml {fileName}");
            var origFilePath = Path.Combine(_originalData, fileName);

            if (Path.Exists(origFilePath))
            {
                UpdateDBFromSpells(origFilePath);
            }
        }

        private void UpdateDBFromSpells(string updateFilePath)
        {
            var dataMenu = Helpers.DeserializeYaml(updateFilePath);
            var entries = dataMenu.Sections.FirstOrDefault(x => x.Type == SectionType.Mgc_)?.Entries;
            _patchDM.UpdateSpells(entries.SpellList);
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
