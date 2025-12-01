using FFXICustomDats.YamlModels.Items.ItemAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class WeaponItem : ArmorItem
    {
        public WeaponItem() { }

        [YamlMember(Alias = "weapon", ApplyNamingConventions = false, Order = 9)]
        public Weapon Weapon { get; set; } = new Weapon();
    }
}
