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
    public partial class UsableItem : Item
    {
        [YamlMember(Alias = "usable_item", ApplyNamingConventions = false, Order = 10)]
        public Usable Usable { get; set; } = new Usable();

    }
}
