using FFXICustomDats.YamlModels.SharedAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public partial class Furnishing
    {
        [YamlMember(Alias = "element", ApplyNamingConventions = false)]
        public Element Element { get; set; }

        [YamlMember(Alias = "storage_slots", ApplyNamingConventions = false)]
        public int StorageSlots { get; set; }

        [YamlMember(Alias = "unknown3", ApplyNamingConventions = false)]
        public uint Unknown3 { get; set; }
    }
}
