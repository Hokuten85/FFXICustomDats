using FFXICustomDats.Data;
using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using FFXICustomDats.YamlModels.SharedAttributes;
using Microsoft.Extensions.Configuration;

namespace FFXICustomDats
{
    public class PatchItemsFromDB(IConfiguration config, XidbContext context)
    {
        private readonly XidbContext _context = context;
        private readonly IConfiguration _config = config;

        public void UpdateItems(Item[] items)
        {
            var itemIds = items.Select(i => i.Id);
            var itemBasics = _context.ItemBasics.Where(x => itemIds.Contains(x.ItemId)).ToList();

            foreach (var item in items)
            {
                var dbItem = itemBasics.FirstOrDefault(x => x.ItemId == item.Id);
                if (dbItem != null)
                {
                    UpdateItemBasic(item, dbItem);
                }
            }
        }

        public void UpdateArmorItems(ArmorItem[] items)
        {
            UpdateItems(items);

            var itemIds = items.Select(i => i.Id);
            var equipmentList = (from equipment in _context.ItemEquipments.Where(x => itemIds.Contains(x.ItemId))
                                join r in _context.ItemMods.Where(x => x.ModId == 276) //EQUIPMENT_ONLY_RACE
                                    on equipment.ItemId equals r.ItemId into rg
                                from race in rg.DefaultIfEmpty()
                                join u in _context.ItemUsables
                                    on equipment.ItemId equals u.ItemId into ug
                                from usable in ug.DefaultIfEmpty()
                                select new { equipment, race, usable }).ToList(); ;

            foreach (var item in items)
            {
                var dbItem = equipmentList.FirstOrDefault(x => x.equipment.ItemId == item.Id);
                if (dbItem != null)
                {
                    UpdateItemEquipment(item, dbItem.equipment);
                    UpdateItemMod(item, dbItem.race);
                    UpdateItemUsable(item, dbItem.usable);
                }
            }
        }

        public void UpdateFurnishingItems(FurnishingItem[] items)
        {
            UpdateItems(items);

            var itemIds = items.Select(i => i.Id);
            var itemFurnishings = _context.ItemFurnishings.Where(x => itemIds.Contains(x.ItemId)).ToList();

            foreach (var item in items)
            {
                var dbItem = itemFurnishings.FirstOrDefault(x => x.ItemId == item.Id);
                if (dbItem != null)
                {
                    UpdateItemFurnishing(item, dbItem);
                }
            }
        }

        public void UpdatePuppetItems(PuppetItem[] items)
        {
            UpdateItems(items);

            var itemIds = items.Select(i => i.Id);
            var itemPuppets = _context.ItemPuppets.Where(x => itemIds.Contains(x.ItemId)).ToList();

            foreach (var item in items)
            {
                var dbItem = itemPuppets.FirstOrDefault(x => x.ItemId == item.Id);
                if (dbItem != null)
                {
                    UpdateItemPuppet(item, dbItem);
                }
            }
        }

        public void UpdateUsableItems(UsableItem[] items)
        {
            UpdateItems(items);

            var itemIds = items.Select(i => i.Id);
            var itemUsables = _context.ItemUsables.Where(x => itemIds.Contains(x.ItemId)).ToList();

            foreach (var item in items)
            {
                var dbItem = itemUsables.FirstOrDefault(x => x.ItemId == item.Id);
                if (dbItem != null)
                {
                    UpdateItemUsable(item, dbItem);
                }
            }
        }

        public void UpdateWeaponItems(WeaponItem[] items)
        {
            UpdateItems(items);
            UpdateArmorItems(items);

            var itemIds = items.Select(i => i.Id);
            var itemWeapon = _context.ItemWeapons.Where(x => itemIds.Contains(x.ItemId)).ToList();

            foreach (var item in items)
            {
                var dbItem = itemWeapon.FirstOrDefault(x => x.ItemId == item.Id);
                if (dbItem != null)
                {
                    UpdateItemWeapon(item, dbItem);
                }
            }
        }

        public static void UpdateItemWeapon(WeaponItem item, ItemWeapon weapon)
        {
            if (weapon != null)
            {
                var dbDamage = weapon.Dmg;
                if ((SkillTypeHelpers.SKILL_TYPE)weapon.Skill == SkillTypeHelpers.SKILL_TYPE.SKILL_HAND_TO_HAND)
                {
                    dbDamage += 3; // h2h starts at 3 + h2h skill scaling. Dats seem to factor in the 3 into the weapon dmg
                }

                if (item.Weapon.Damage != dbDamage)
                {
                    item.Weapon.Damage = dbDamage;
                }

                var dbDelay = weapon.Delay;
                if ((SkillTypeHelpers.SKILL_TYPE)weapon.Skill == SkillTypeHelpers.SKILL_TYPE.SKILL_HAND_TO_HAND)
                {
                    dbDelay = weapon.Delay - 480 + 240; // database h2h delay starts at 480, then adds the weapon delay. Dat delays for h2h seem to be the standard delay of 240 + the weapon delay
                }

                if (dbDelay > 0 && item.Weapon.Delay != dbDelay)
                {
                    item.Weapon.Delay = dbDelay;
                }

                if (dbDelay > 0 && (item.Weapon.Damage != dbDamage || item.Weapon.Delay != dbDelay)
                    && item.Weapon.DPS != (int)Math.Round(dbDamage * 60.0 / dbDelay * 100))
                {
                    item.Weapon.DPS = (int)Math.Round(dbDamage * 60.0 / dbDelay * 100);
                }

                if (!SkillTypeHelpers.IsEqual(item.Weapon.SkillType, weapon.Skill))
                {
                    item.Weapon.SkillType = SkillTypeHelpers.Map.GetValueOrDefault((SkillTypeHelpers.SKILL_TYPE)weapon.Skill);

                }
            }
        }

        public static void UpdateItemPuppet(PuppetItem item, ItemPuppet puppet)
        {
            if (puppet != null)
            {
                if (item.Puppet.Slot != (PuppetSlot)puppet.Slot)
                {
                    item.Puppet.Slot = (PuppetSlot)puppet.Slot;
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Fire, puppet.Element, Element.Fire))
                {
                    item.Puppet.ElementCharge.Fire = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Fire);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Ice, puppet.Element, Element.Ice))
                {
                    item.Puppet.ElementCharge.Ice = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Ice);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Wind, puppet.Element, Element.Air))
                {
                    item.Puppet.ElementCharge.Wind = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Air);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Earth, puppet.Element, Element.Earth))
                {
                    item.Puppet.ElementCharge.Earth = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Earth);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Lightning, puppet.Element, Element.Thunder))
                {
                    item.Puppet.ElementCharge.Lightning = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Thunder);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Water, puppet.Element, Element.Water))
                {
                    item.Puppet.ElementCharge.Water = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Water);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Light, puppet.Element, Element.Light))
                {
                    item.Puppet.ElementCharge.Light = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Light);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Dark, puppet.Element, Element.Fire))
                {
                    item.Puppet.ElementCharge.Dark = ElementHelpers.GetPuppetElementValue(puppet.Element, Element.Dark);
                }
            }
        }

        public static void UpdateItemFurnishing(FurnishingItem item, ItemFurnishing furnishing)
        {
            if (furnishing != null)
            {
                if (item.Furnishing.Element != (Element)furnishing.Element)
                {
                    item.Furnishing.Element = (Element)furnishing.Element;
                }

                if (item.Furnishing.StorageSlots != furnishing.Storage)
                {
                    item.Furnishing.StorageSlots = furnishing.Storage;
                }
            }
        }

        public void UpdateItemMod(ArmorItem item, ItemMod race)
        {
            if (race != null && _config.GetValue<bool>("TrustDatabaseRace")
                && Helpers.ConvertEnumListToBit(item.Equipment.Races) != race.Value)
            {
                item.Equipment.Races = Helpers.BitsToEnumList<Race>((ushort)race.Value);
            }
        }

        public static void UpdateItemUsable(UsableItem item, ItemUsable usable)
        {
            if (usable != null) {
                if (item.Usable.ActivationTime != usable.Activation)
                {
                    item.Usable.ActivationTime = usable.Activation;
                }
            }
        }

        public static void UpdateItemUsable(ArmorItem item, ItemUsable usable)
        {
            if (usable != null)
            {
                //No valid targets for now. Maybe after looking at spells
                //if (!ValidTargetHelpers.IsEqual(item.ValidTargets, usable.ValidTargets))
                //{
                //    item.ValidTargets = Helpers.DBFlagsToYamlFlags(ValidTargetHelpers.ValidTargetMap, usable.ValidTargets);
                //}

                if (item.Equipment.MaxCharges != usable.MaxCharges)
                {
                    item.Equipment.MaxCharges = usable.MaxCharges;
                }

                // some significant differences between db and dats. going to skip for now
                //if (item.Equipment.CastingTime != usable.Activation) 
                //{
                //    item.Equipment.CastingTime = usable.Activation;
                //}

                if (item.Equipment.UseDelay != usable.UseDelay)
                {
                    item.Equipment.UseDelay = usable.UseDelay;
                }

                if (item.Equipment.ReuseDelay != usable.ReuseDelay)
                {
                    item.Equipment.ReuseDelay = usable.ReuseDelay;
                }
            }
        }

        public static void UpdateItemEquipment(ArmorItem item, ItemEquipment equipment)
        {
            if (equipment != null)
            {
                if (item.Equipment.Level != equipment.Level)
                {
                    item.Equipment.Level = equipment.Level;
                }

                if (!SlotHelpers.IsEqual(item.Equipment.Slots, equipment.Slot))
                {
                    item.Equipment.Slots = Helpers.DBValueToYamlList(SlotHelpers.Map, equipment.Slot);
                }

                if (!JobHelpers.IsEqual(item.Equipment.Jobs, equipment.Jobs))
                {
                    item.Equipment.Jobs = JobHelpers.DBValueToYamlList(equipment.Jobs);
                }

                if (item.Equipment.SuperiorLevel != equipment.SuLevel)
                {
                    item.Equipment.SuperiorLevel = equipment.SuLevel;
                }

                if (item.Equipment.ShieldSize != equipment.ShieldSize)
                {
                    item.Equipment.ShieldSize = equipment.ShieldSize;
                }

                if (item.Equipment.Ilevel != equipment.Ilevel)
                {
                    item.Equipment.Ilevel = equipment.Ilevel;
                }
            }
        }

        public static void UpdateItemBasic(Item item, ItemBasic itemBasic)
        {
            if (itemBasic != null)
            {
                if (!FlagHelpers.IsEqual(item.Flags, itemBasic.Flags))
                {
                    item.Flags = Helpers.DBValueToYamlList(FlagHelpers.Map, itemBasic.Flags);
                }

                if (item.StackSize != itemBasic.StackSize)
                {
                    item.StackSize = itemBasic.StackSize;
                }
            }
        }
    }
}
