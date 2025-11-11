using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace FFXICustomDats.DatModels
{
    public partial class Armor : Item
    {
        [YamlMember(Alias = "equipment", ApplyNamingConventions = false, Order = 8)]
        public Equipment Equipment { get; set; } = new Equipment();
    }
}
