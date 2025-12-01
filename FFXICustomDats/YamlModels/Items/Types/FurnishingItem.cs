using FFXICustomDats.YamlModels.Items.ItemAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class FurnishingItem : Item
    {
        public FurnishingItem() { }

        [YamlMember(Alias = "furnishing", ApplyNamingConventions = false, Order = 8)]
        public Furnishing Furnishing { get; set; } = new Furnishing();
    }
}
