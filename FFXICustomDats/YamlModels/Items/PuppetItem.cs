using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items
{
    public partial class PuppetItem : Item
    {
        [YamlMember(Alias = "puppet", ApplyNamingConventions = false, Order = 10)]
        public Puppet Puppet { get; set; } = new Puppet();

    }
}
