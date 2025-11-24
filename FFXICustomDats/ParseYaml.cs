using FFXICustomDats.Data;
using YamlItems = FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items;
using DatEntities = FFXICustomDats.Data.XiDatEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Microsoft.Extensions.Configuration;
using FFXICustomDats.YamlModels.Items.ItemTypes;

namespace FFXICustomDats
{
    public class ParseYaml(IConfiguration config, XiDatContext datContext)
    {
        private readonly XiDatContext _datContext = datContext;
        private readonly IConfiguration _config = config;

        public void ParseYamlFiles()
        {
            UpdateXiDatdbFromYaml<ArmorItem>("armor.yml");
            UpdateXiDatdbFromYaml<ArmorItem>("armor2.yml");
            UpdateXiDatdbFromYaml<FurnishingItem>("general_items.yml");
            UpdateXiDatdbFromYaml<FurnishingItem>("general_items2.yml");
            UpdateXiDatdbFromYaml<PuppetItem>("puppet_items.yml");
            UpdateXiDatdbFromYaml<UsableItem>("usable_items.yml");
            UpdateXiDatdbFromYaml<WeaponItem>("weapons.yml");

            Console.WriteLine("Press any key to return.");
            Console.ReadLine();
        }

        public void UpdateXiDatdbFromYaml<T>(string fileName) where T : YamlItems.Item
        {
            Console.WriteLine($"Parsing {fileName} and Updating xidb");

            var exportDatDir = _config.GetValue<string>("ExportDatDir");
            var filePath = $@"{exportDatDir}\{fileName}";

            if (Path.Exists(filePath))
            {
                var items = DeserializeYaml<T>(filePath);
                UpsertItems(items, filePath);
            }
        }

        public static FFXIItems<T> DeserializeYaml<T>(string filePath) where T : YamlItems.Item
        {
            FileStream fileStream = new(filePath, FileMode.Open);
            using var reader = new StreamReader(fileStream);

            var input = new StringReader(reader.ReadToEnd());
            var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();

            return deserializer.Deserialize<FFXIItems<T>>(input);
        }

        private void UpsertItems<T>(FFXIItems<T> ffxiItems, string filePath) where T : YamlItems.Item
        {
            FileInfo fileInfo = new(filePath);

            var items = _datContext.Items.ToList();
            var itemEquipments = _datContext.ItemEquipments.ToList();
            var itemFurnishings = _datContext.ItemFurnishings.ToList();
            var itemStrings = _datContext.ItemStrings.ToList();
            var itemPuppets = _datContext.ItemPuppets.ToList();
            var itemUsable = _datContext.ItemUsables.ToList();
            var itemWeapon = _datContext.ItemWeapons.ToList();

            foreach (var yamlItem in ffxiItems.Items)
            {
                UpsertItem(items, yamlItem, fileInfo.Name);
                UpsertItemStrings(itemStrings, yamlItem);

                if (typeof(T) == typeof(ArmorItem) || typeof(T) == typeof(WeaponItem))
                {
                    UpsertItemEquipment(itemEquipments, yamlItem as ArmorItem);
                }

                if (typeof(T) == typeof(FurnishingItem))
                {
                    UpsertItemFurnishing(itemFurnishings, yamlItem as FurnishingItem);
                }

                if (typeof(T) == typeof(PuppetItem))
                {
                    UpsertItemPuppet(itemPuppets, yamlItem as PuppetItem);
                }

                if (typeof(T) == typeof(UsableItem))
                {
                    UpsertItemUsable(itemUsable, yamlItem as UsableItem);
                }

                if (typeof(T) == typeof(WeaponItem))
                {
                    UpsertItemWeapon(itemWeapon, yamlItem as WeaponItem);
                }
            }

            _datContext.SaveChanges();
        }

        private void UpsertItem(List<DatEntities.Item> items, YamlItems.Item item, string fileName)
        {
            var dbItem = items.FirstOrDefault(x => x.ItemId == item.Id);
            var newItem = new DatEntities.Item()
            {
                ItemId = item.Id,
                Flags = Helpers.ConvertEnumListToBit(item.Flags),
                StackSize = item.StackSize,
                ItemType = (ushort)item.ItemType,
                ResourceId = item.ResourceId,
                ValidTargets = Helpers.ConvertEnumListToBit(item.ValidTargets),
                IconBytes = Encoding.ASCII.GetBytes(item.IconBytes),
                DatFile = fileName,
            };

            if (dbItem == null)
            {
                _datContext.Items.Add(newItem);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newItem, makeEqual: true))
            {
                _datContext.Items.Update(dbItem);
            }
        }

        private void UpsertItemStrings(List<DatEntities.ItemString> items, YamlItems.Item item)
        {
            var dbItem = items.FirstOrDefault(x => x.ItemId == item.Id);
            var newItem = new DatEntities.ItemString()
            {
                ItemId = item.Id,
                Name = item.Strings.Name,
                ArticleType = (ushort)item.Strings.ArticleType,
                SingularName = item.Strings.SingularName,
                PluralName = item.Strings.PluralName,
                Description = item.Strings.Description,
            };

            if (dbItem == null)
            {
                _datContext.ItemStrings.Add(newItem);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newItem, makeEqual: true))
            {
                _datContext.ItemStrings.Update(dbItem);
            }
        }

        private void UpsertItemEquipment(List<DatEntities.ItemEquipment> items, ArmorItem item)
        {
            var dbItem = items.FirstOrDefault(x => x.ItemId == item.Id);
            var newEquipment = new DatEntities.ItemEquipment()
            {
                ItemId = item.Id,
                Level = (byte)item.Equipment.Level,
                Slot = Helpers.ConvertEnumListToBit(item.Equipment.Slots),
                Races = Helpers.ConvertEnumListToBit(item.Equipment.Races),
                Jobs = Helpers.ConvertEnumListToBit(item.Equipment.Jobs),
                SuperiorLevel = item.Equipment.SuperiorLevel,
                ShieldSize = item.Equipment.ShieldSize,
                MaxCharges = item.Equipment.MaxCharges,
                CastingTime = item.Equipment.CastingTime,
                UseDelay = item.Equipment.UseDelay,
                ReuseDelay = item.Equipment.ReuseDelay,
                ILevel = item.Equipment.Ilevel,
                Unknown1 = item.Equipment.Unknown1,
                Unknown2 = item.Equipment.Unknown2,
                Unknown3 = item.Equipment.Unknown3
            };

            if (dbItem == null)
            {
                _datContext.ItemEquipments.Add(newEquipment);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newEquipment, makeEqual: true))
            {
                _datContext.ItemEquipments.Update(dbItem);
            }
        }

        private void UpsertItemFurnishing(List<DatEntities.ItemFurnishing> items, FurnishingItem item)
        {
            var dbItem = items.FirstOrDefault(x => x.ItemId == item.Id);
            var newFurnishing = new DatEntities.ItemFurnishing()
            {
                ItemId = item.Id,
                StorageSlots = (byte)item.Furnishing.StorageSlots,
                Element = (byte)item.Furnishing.Element,
                Unknown3 = item.Furnishing.Unknown3
            };

            if (dbItem == null)
            {
                _datContext.ItemFurnishings.Add(newFurnishing);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newFurnishing, makeEqual: true))
            {
                _datContext.ItemFurnishings.Update(dbItem);
            }
        }

        private void UpsertItemPuppet(List<DatEntities.ItemPuppet> items, PuppetItem item)
        {
            var dbItem = items.FirstOrDefault(x => x.ItemId == item.Id);
            var newPuppet = new DatEntities.ItemPuppet()
            {
                ItemId = item.Id,
                Slot = (byte)item.Puppet.Slot,
                Fire = item.Puppet.ElementCharge.Fire,
                Ice = item.Puppet.ElementCharge.Ice,
                Wind = item.Puppet.ElementCharge.Wind,
                Earth = item.Puppet.ElementCharge.Earth,
                Lightning = item.Puppet.ElementCharge.Lightning,
                Water = item.Puppet.ElementCharge.Water,
                Light = item.Puppet.ElementCharge.Light,
                Dark = item.Puppet.ElementCharge.Dark,
                Unknown1 = item.Puppet.Unknown1
            };

            if (dbItem == null)
            {
                _datContext.ItemPuppets.Add(newPuppet);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newPuppet, makeEqual: true))
            {
                _datContext.ItemPuppets.Update(dbItem);
            }
        }

        private void UpsertItemUsable(List<DatEntities.ItemUsable> items, UsableItem item)
        {
            var dbItem = items.FirstOrDefault(x => x.ItemId == item.Id);
            var newUsable = new DatEntities.ItemUsable()
            {
                ItemId = item.Id,
                ActivationTime = (byte)item.Usable.ActivationTime,
                Unknown1 = item.Usable.Unknown1,
                Unknown2 = item.Usable.Unknown2,
                Unknown3 = item.Usable.Unknown3
            };

            if (dbItem == null)
            {
                _datContext.ItemUsables.Add(newUsable);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newUsable, makeEqual: true))
            {
                _datContext.ItemUsables.Update(dbItem);
            }
        }

        private void UpsertItemWeapon(List<DatEntities.ItemWeapon> items, WeaponItem item)
        {
            var dbItem = items.FirstOrDefault(x => x.ItemId == item.Id);
            var newWeapon = new DatEntities.ItemWeapon()
            {
                ItemId = item.Id,
                Damage = item.Weapon.Damage,
                Delay = item.Weapon.Delay,
                Dps = item.Weapon.DPS,
                SkillType = (uint)item.Weapon.SkillType,
                JugSize = item.Weapon.JugSize,
                Unknown1 = item.Weapon.Unknown1,
            };

            if (dbItem == null)
            {
                _datContext.ItemWeapons.Add(newWeapon);
            }
            else if (!ObjectComparerUtility.ObjectsAreEqual(dbItem, newWeapon, makeEqual: true))
            {
                _datContext.ItemWeapons.Update(dbItem);
            }
        }

        private static class ObjectComparerUtility
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
    }
}
