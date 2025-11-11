using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace FFXICustomDats.DatModels
{
    public partial class Equipment
    {
        [YamlMember(Alias = "level", ApplyNamingConventions = false)]
        public int Level { get; set; }

        [YamlMember(Alias = "slots", ApplyNamingConventions = false)]
        public List<Slot> Slots { get; set; } = [];

        [YamlMember(Alias = "races", ApplyNamingConventions = false)]
        public List<Race> Races { get; set; } = [];

        [YamlMember(Alias = "jobs", ApplyNamingConventions = false)]
        public List<Job> Jobs { get; set; } = [];

        [YamlMember(Alias = "superior_level", ApplyNamingConventions = false)]
        public byte SuperiorLevel { get; set; }

        [YamlMember(Alias = "shield_size", ApplyNamingConventions = false)]
        public byte ShieldSize { get; set; }

        [YamlMember(Alias = "max_charges", ApplyNamingConventions = false)]
        public byte MaxCharges { get; set; }

        [YamlMember(Alias = "casting_time", ApplyNamingConventions = false)]
        public byte CastingTime { get; set; }

        [YamlMember(Alias = "use_delay", ApplyNamingConventions = false)]
        public byte UseDelay { get; set; }

        [YamlMember(Alias = "reuse_delay", ApplyNamingConventions = false)]
        public uint ReuseDelay { get; set; }

        [YamlMember(Alias = "unknown1", ApplyNamingConventions = false)]
        public int Unknown1 { get; set; }

        [YamlMember(Alias = "ilevel", ApplyNamingConventions = false)]
        public byte Ilevel { get; set; }

        [YamlMember(Alias = "unknown2", ApplyNamingConventions = false)]
        public int Unknown2 { get; set; }

        [YamlMember(Alias = "unknown3", ApplyNamingConventions = false)]
        public int Unknown3 { get; set; }
    }
}
