using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.SharedAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.DataMenu
{
    public class Ability : Entry
    {
        [YamlMember(Alias = "id", ApplyNamingConventions = false, Order = 1)]
        public new long Id { get; set; }

        [YamlMember(Alias = "ability_type", ApplyNamingConventions = false, Order = 2)]
        public AbilityType? AbilityType { get; set; }

        [YamlMember(Alias = "icon_id", ApplyNamingConventions = false, Order = 3)]
        public new long IconId { get; set; }

        [YamlMember(Alias = "mp_cost", ApplyNamingConventions = false, Order = 4)]
        public new long MpCost { get; set; }

        [YamlMember(Alias = "unknown1", ApplyNamingConventions = false, Order = 5)]
        public long? Unknown1 { get; set; }

        [YamlMember(Alias = "shared_timer_id", ApplyNamingConventions = false, Order = 6)]
        public long? SharedTimerId { get; set; }

        [YamlMember(Alias = "valid_targets", ApplyNamingConventions = false, Order = 7)]
        public new List<ValidTarget> ValidTargets { get; set; } = [];

        [YamlMember(Alias = "tp_cost", ApplyNamingConventions = false, Order = 8)]
        public long? TpCost { get; set; }

        [YamlMember(Alias = "unknowns", ApplyNamingConventions = false, Order = 9)]
        public string Unknowns { get; set; }
    }
}
