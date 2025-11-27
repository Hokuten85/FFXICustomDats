using FFXICustomDats.YamlModels.Items.ItemAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class UsableItem : Item
    {
        public UsableItem() { }

        [YamlMember(Alias = "usable_item", ApplyNamingConventions = false, Order = 10)]
        public Usable Usable { get; set; } = new Usable();

    }
}
