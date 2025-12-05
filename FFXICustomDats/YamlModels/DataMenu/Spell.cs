using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.SharedAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.DataMenu
{
    public class Spell : Entry
    {
        [YamlMember(Alias = "index", ApplyNamingConventions = false, Order = 1)]
        public long? Index { get; set; }

        [YamlMember(Alias = "magic_type", ApplyNamingConventions = false, Order = 2)]
        public MagicType? MagicType { get; set; }

        [YamlMember(Alias = "element", ApplyNamingConventions = false, Order = 3)]
        public Element? Element { get; set; }

        [YamlMember(Alias = "valid_targets", ApplyNamingConventions = false, Order = 4)]
        public new List<ValidTarget> ValidTargets { get; set; } = [];

        [YamlMember(Alias = "skill_type", ApplyNamingConventions = false, Order = 5)]
        public SkillType SkillType { get; set; }

        [YamlMember(Alias = "mp_cost", ApplyNamingConventions = false, Order = 6)]
        public new long MpCost { get; set; }

        [YamlMember(Alias = "cast_time", ApplyNamingConventions = false, Order = 7)]
        public long? CastTime { get; set; }

        [YamlMember(Alias = "recast_time", ApplyNamingConventions = false, Order = 8)]
        public long? RecastTime { get; set; }

        [YamlMember(Alias = "level_required", ApplyNamingConventions = false, Order = 9)]
        public Dictionary<Job, long> LevelRequired { get; set; }
    }
}
