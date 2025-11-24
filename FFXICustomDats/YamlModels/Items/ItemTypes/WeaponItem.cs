using FFXICustomDats.Data.XiDatEntities;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class WeaponItem : ArmorItem
    {
        public WeaponItem() { }
        public WeaponItem(Data.XiDatEntities.Item item, ItemString strings, ItemEquipment equipment, ItemWeapon weapon) : base(item, strings, equipment)
        {
            Weapon = new Weapon()
            {
                Damage = weapon.Damage,
                Delay = weapon.Delay,
                DPS = weapon.Dps,
                SkillType = (SkillType)weapon.SkillType,
                JugSize = weapon.JugSize,
                Unknown1 = weapon.Unknown1
            };
        }

        [YamlMember(Alias = "weapon", ApplyNamingConventions = false, Order = 9)]
        public Weapon Weapon { get; set; } = new Weapon();
    }
}
