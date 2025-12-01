using FFXICustomDats.YamlModels.Items.ItemAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class PuppetItem : Item
    {
        public PuppetItem() { }

        [YamlMember(Alias = "puppet", ApplyNamingConventions = false, Order = 10)]
        public Puppet Puppet { get; set; } = new Puppet();

    }
}
