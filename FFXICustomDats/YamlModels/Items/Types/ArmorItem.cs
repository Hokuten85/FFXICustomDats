using FFXICustomDats.YamlModels.Items.ItemAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class ArmorItem : Item
    {
        public ArmorItem() { }

        [YamlMember(Alias = "equipment", ApplyNamingConventions = false, Order = 8)]
        public Equipment Equipment { get; set; } = new Equipment();
    }
}
