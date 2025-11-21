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
    public partial class FurnishingItem : Item
    {
        [YamlMember(Alias = "furnishing", ApplyNamingConventions = false)]
        public Furnishing Furnishing { get; set; } = new Furnishing();
    }
}
