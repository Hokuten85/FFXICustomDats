using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public partial class Usable
    {
        [YamlMember(Alias = "activation_time", ApplyNamingConventions = false)]
        public int ActivationTime { get; set; }

        [YamlMember(Alias = "unknown1", ApplyNamingConventions = false)]
        public uint Unknown1 { get; set; }

        [YamlMember(Alias = "unknown2", ApplyNamingConventions = false)]
        public uint Unknown2 { get; set; }

        [YamlMember(Alias = "unknown3", ApplyNamingConventions = false)]
        public uint Unknown3 { get; set; }
    }
}
