using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items
{
    public partial class FFXIItems<T> where T : Item
    {
        [YamlMember(Alias = "items", ApplyNamingConventions = false)]
        public T[] Items { get; set; }
    }
}
