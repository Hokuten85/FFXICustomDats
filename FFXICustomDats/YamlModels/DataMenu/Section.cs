using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items
{
    public class Section
    {
        [YamlMember(Alias = "type", ApplyNamingConventions = false)]
        public SectionType Type { get; set; }

        [YamlMember(Alias = "entries", ApplyNamingConventions = false)]
        public Entries Entries { get; set; }
    }

    public enum SectionType
    {
        Mnc2,
        Mon_,
        Levc,
        Mgc_,
        Comm,
    }
}
