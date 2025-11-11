using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace FFXICustomDats.DatModels
{
    public partial class FFXIItems
    {
        [YamlMember(Alias = "items", ApplyNamingConventions = false)]
        public Armor[] Items { get; set; }
    }
}
