using FFXICustomDats.YamlModels.Items.ItemTypes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items
{
    public partial class XIDataMenu
    {
        [YamlMember(Alias = "sections", ApplyNamingConventions = false)]
        public Section[] Sections { get; set; }
    }
}
