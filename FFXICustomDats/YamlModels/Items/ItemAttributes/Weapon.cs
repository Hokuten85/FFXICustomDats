using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public partial class Weapon
    {
        [YamlMember(Alias = "damage", ApplyNamingConventions = false)]
        public uint Damage { get; set; }

        [YamlMember(Alias = "delay", ApplyNamingConventions = false)]
        public int Delay { get; set; }

        [YamlMember(Alias = "dps", ApplyNamingConventions = false)]
        public int DPS { get; set; }

        [YamlMember(Alias = "skill_type", ApplyNamingConventions = false)]
        public SkillType SkillType { get; set; }

        [YamlMember(Alias = "jug_size", ApplyNamingConventions = false)]
        public int JugSize { get; set; } = 0;

        [YamlMember(Alias = "unknown1", ApplyNamingConventions = false)]
        public uint Unknown1 { get; set; }
    }
}
