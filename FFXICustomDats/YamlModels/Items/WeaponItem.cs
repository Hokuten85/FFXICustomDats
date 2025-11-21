using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items
{
    public partial class WeaponItem : ArmorItem
    {
        [YamlMember(Alias = "weapon", ApplyNamingConventions = false, Order = 9)]
        public Weapon Weapon { get; set; } = new Weapon();
    }
}
