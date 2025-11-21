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
    public partial class ArmorItem : Item
    {
        [YamlMember(Alias = "equipment", ApplyNamingConventions = false, Order = 8)]
        public Equipment Equipment { get; set; } = new Equipment();
    }
}
