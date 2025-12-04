using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.SharedAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.DataMenu
{
    public class Entry
    {
        [YamlMember(Alias = "valid_targets", ApplyNamingConventions = false, Order = 8)]
        public List<ValidTarget> ValidTargets { get; set; } = [];

        [YamlMember(Alias = "mp_cost", ApplyNamingConventions = false, Order = 9)]
        public long MpCost { get; set; }

        [YamlMember(Alias = "id", ApplyNamingConventions = false, Order = 10)]
        public long Id { get; set; }

        [YamlMember(Alias = "icon_id", ApplyNamingConventions = false, Order = 11)]
        public long IconId { get; set; }

        [YamlMember(Alias = "unknowns", ApplyNamingConventions = false, Order = 12)]
        public string Unknowns { get; set; }
    }
}
