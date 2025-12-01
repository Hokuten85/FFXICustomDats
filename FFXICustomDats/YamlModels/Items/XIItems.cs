using FFXICustomDats.YamlModels.Items.ItemTypes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items
{
    public class XIItems<T> where T : Item
    {
        [YamlMember(Alias = "items", ApplyNamingConventions = false)]
        public T[] Items { get; set; }
    }
}
