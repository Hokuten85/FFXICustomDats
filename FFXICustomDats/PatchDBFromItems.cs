using FFXICustomDats.Data;
using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Microsoft.Extensions.Configuration;

namespace FFXICustomDats
{
    public class PatchDBFromItems(IConfiguration config, XidbContext context)
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

            _context.SaveChanges();
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

            _context.SaveChanges();
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

            _context.SaveChanges();
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

            _context.SaveChanges();
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

            _context.SaveChanges();
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

            _context.SaveChanges();
        }

        public void UpdateItemWeapon(WeaponItem item, ItemWeapon weapon)
        {
            if (weapon != null)
            {
                var yamlDmg = item.Weapon.Damage;
                if (item.Weapon.SkillType == SkillType.HandToHand)
                {
                    yamlDmg -= 3; // h2h starts at 3 + h2h skill scaling. Dats seem to factor in the 3 into the weapon dmg
                }

                if (yamlDmg > 0 && weapon.Dmg != yamlDmg)
                {
                    weapon.Dmg = yamlDmg;
                }

                var yamlDelay = item.Weapon.Delay;
                if (item.Weapon.SkillType == SkillType.HandToHand)
                {
                    yamlDelay = item.Weapon.Delay - 240 + 480; // database h2h delay starts at 480, then adds the weapon delay. Dat delays for h2h seem to be the standard delay of 240 + the weapon delay
                }

                if (yamlDelay > 0 && weapon.Delay != yamlDelay)
                {
                    weapon.Delay = yamlDelay;
                }

                if (!SkillTypeHelpers.IsEqual(item.Weapon.SkillType, weapon.Skill))
                {
                    weapon.Skill = (byte)(SkillTypeHelpers.ReverseSkillTypeMap().TryGetValue(item.Weapon.SkillType, out var skillType) ? skillType : 0);
                }

                _context.ItemWeapons.Update(weapon);
            }
        }

        public void UpdateItemPuppet(PuppetItem item, ItemPuppet puppet)
        {
            if (puppet != null)
            {
                if (item.Puppet.Slot != (PuppetSlot)puppet.Slot)
                {
                    puppet.Slot = (byte)item.Puppet.Slot;
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Fire, puppet.Element, Element.Fire))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Fire, Element.Fire);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Ice, puppet.Element, Element.Ice))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Ice, Element.Ice);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Wind, puppet.Element, Element.Air))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Wind, Element.Air);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Earth, puppet.Element, Element.Earth))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Earth, Element.Earth);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Lightning, puppet.Element, Element.Thunder))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Lightning, Element.Thunder);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Water, puppet.Element, Element.Water))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Water, Element.Water);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Light, puppet.Element, Element.Light))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Light, Element.Light);
                }

                if (!ElementHelpers.IsEqual(item.Puppet.ElementCharge.Dark, puppet.Element, Element.Fire))
                {
                    puppet.Element |= ElementHelpers.ElementToBitValue(item.Puppet.ElementCharge.Dark, Element.Dark);
                }

                _context.ItemPuppets.Update(puppet);
            }
        }

        public void UpdateItemFurnishing(FurnishingItem item, ItemFurnishing furnishing)
        {
            if (furnishing != null)
            {
                if (item.Furnishing.Element != (Element)furnishing.Element)
                {
                    furnishing.Element = (byte)item.Furnishing.Element;
                }

                if (item.Furnishing.StorageSlots != furnishing.Storage)
                {
                    furnishing.Storage = (byte)item.Furnishing.StorageSlots;
                }

                _context.ItemFurnishings.Update(furnishing);
            }
        }

        public void UpdateItemMod(ArmorItem item, ItemMod race)
        {
            if (race != null)
            {
                if (Helpers.ConvertEnumListToBit(item.Equipment.Races) != race.Value)
                {
                    race.Value = (short)Helpers.ConvertEnumListToBit(item.Equipment.Races);
                    _context.ItemMods.Update(race);
                }
            }
            else if (!item.Equipment.Races.Contains(Race.All))
            {
                _context.ItemMods.Add(new ItemMod()
                {
                    ItemId = item.Id,
                    ModId = 276,
                    Value = (short)Helpers.ConvertEnumListToBit(item.Equipment.Races)
                });
            }
        }

        public void UpdateItemUsable(UsableItem item, ItemUsable usable)
        {
            if (usable != null) {
                if (item.Usable.ActivationTime != usable.Activation)
                {
                    usable.Activation = (byte)item.Usable.ActivationTime;
                }

                _context.ItemUsables.Update(usable);
            }
        }

        public void UpdateItemUsable(ArmorItem item, ItemUsable usable)
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
                    usable.MaxCharges = item.Equipment.MaxCharges;
                }

                // some significant differences between db and dats. going to skip for now
                //if (item.Equipment.CastingTime != usable.Activation) 
                //{
                //    item.Equipment.CastingTime = usable.Activation;
                //}

                if (item.Equipment.UseDelay != usable.UseDelay)
                {
                    usable.UseDelay = item.Equipment.UseDelay;
                }

                if (item.Equipment.ReuseDelay != usable.ReuseDelay)
                {
                    usable.ReuseDelay = item.Equipment.ReuseDelay;
                }

                _context.ItemUsables.Update(usable);
            }
        }

        public void UpdateItemEquipment(ArmorItem item, ItemEquipment equipment)
        {
            if (equipment != null)
            {
                if (item.Equipment.Level != equipment.Level)
                {
                    equipment.Level = (byte)item.Equipment.Level;
                }

                if (!SlotHelpers.IsEqual(item.Equipment.Slots, equipment.Slot))
                {
                    equipment.Slot = SlotHelpers.YamlListToDBValue(item.Equipment.Slots);
                }

                if (!JobHelpers.IsEqual(item.Equipment.Jobs, equipment.Jobs))
                {
                    equipment.Jobs = JobHelpers.YamlListToDBValue(item.Equipment.Jobs);
                }

                if (item.Equipment.SuperiorLevel != equipment.SuLevel)
                {
                    equipment.SuLevel = item.Equipment.SuperiorLevel;
                }

                if (item.Equipment.ShieldSize != equipment.ShieldSize)
                {
                    equipment.ShieldSize = item.Equipment.ShieldSize;
                }

                if (item.Equipment.Ilevel != equipment.Ilevel)
                {
                    equipment.Ilevel = item.Equipment.Ilevel;
                }

                _context.ItemEquipments.Update(equipment);
            }
        }

        public void UpdateItemBasic(Item item, ItemBasic itemBasic)
        {
            if (itemBasic != null)
            {
                if (!FlagHelpers.IsEqual(item.Flags, itemBasic.Flags))
                {
                    itemBasic.Flags = (ushort)Helpers.YamlListToDBValue(FlagHelpers.ReverseFlagMap(), item.Flags);
                }

                if (item.StackSize != itemBasic.StackSize)
                {
                    itemBasic.StackSize = item.StackSize;
                }

                _context.ItemBasics.Update(itemBasic);
            }
        }
    }
}
