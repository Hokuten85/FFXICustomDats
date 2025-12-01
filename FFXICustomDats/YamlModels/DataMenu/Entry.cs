using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.SharedAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.DataMenu
{
    public class Entry
    {
        [YamlMember(Alias = "index", ApplyNamingConventions = false)]
        public long? Index { get; set; }

        [YamlMember(Alias = "magic_type", ApplyNamingConventions = false)]
        public MagicType MagicType { get; set; }

        [YamlMember(Alias = "element", ApplyNamingConventions = false)]
        public Element? Element { get; set; }

        [YamlMember(Alias = "valid_targets", ApplyNamingConventions = false)]
        public List<ValidTarget> ValidTargets { get; set; }

        [YamlMember(Alias = "skill_type", ApplyNamingConventions = false)]
        public SkillType SkillType { get; set; }

        [YamlMember(Alias = "mp_cost", ApplyNamingConventions = false)]
        public long MpCost { get; set; }

        [YamlMember(Alias = "cast_time", ApplyNamingConventions = false)]
        public long? CastTime { get; set; }

        [YamlMember(Alias = "recast_time", ApplyNamingConventions = false)]
        public long? RecastTime { get; set; }

        [YamlMember(Alias = "level_required", ApplyNamingConventions = false)]
        public Dictionary<Job, long> LevelRequired { get; set; }

        [YamlMember(Alias = "id", ApplyNamingConventions = false)]
        public long Id { get; set; }

        [YamlMember(Alias = "icon_id", ApplyNamingConventions = false)]
        public long IconId { get; set; }

        [YamlMember(Alias = "unknowns", ApplyNamingConventions = false)]
        public Unknowns Unknowns { get; set; }

        [YamlMember(Alias = "ability_type", ApplyNamingConventions = false)]
        public AbilityType? AbilityType { get; set; }

        [YamlMember(Alias = "unknown1", ApplyNamingConventions = false)]
        public long? Unknown1 { get; set; }

        [YamlMember(Alias = "shared_timer_id", ApplyNamingConventions = false)]
        public long? SharedTimerId { get; set; }

        [YamlMember(Alias = "tp_cost", ApplyNamingConventions = false)]
        public long? TpCost { get; set; }
    }

    public class Entries
    {
        public List<Entry> EntryList = [];
        public string String;

        public static implicit operator Entries(List<Entry> EntryList) => new Entries { EntryList = EntryList };
        public static implicit operator Entries(string String) => new Entries { String = String };
    }
}
