using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items
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
