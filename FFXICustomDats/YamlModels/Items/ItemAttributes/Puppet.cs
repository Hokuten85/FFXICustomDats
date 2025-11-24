using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public partial class Puppet
    {
        [YamlMember(Alias = "slot", ApplyNamingConventions = false)]
        public PuppetSlot Slot { get; set; }

        [YamlMember(Alias = "element_charge", ApplyNamingConventions = false)]
        public ElementCharge ElementCharge { get; set; } = new ElementCharge();

        [YamlMember(Alias = "unknown1", ApplyNamingConventions = false)]
        public uint Unknown1 { get; set; }
    }
}
