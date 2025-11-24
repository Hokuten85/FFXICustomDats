using FFXICustomDats.Data.XiDatEntities;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
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
        public PuppetItem() { }
        public PuppetItem(Data.XiDatEntities.Item item, Data.XiDatEntities.ItemString strings, ItemPuppet puppet) : base(item, strings)
        {
            Puppet = new Puppet()
            {
                Slot = (PuppetSlot)puppet.Slot,
                ElementCharge = new ElementCharge()
                {
                    Fire = puppet.Fire,
                    Ice = puppet.Ice,
                    Wind = puppet.Wind,
                    Earth = puppet.Earth,
                    Lightning = puppet.Lightning,
                    Water = puppet.Water,
                    Light = puppet.Light,
                    Dark = puppet.Dark,
                },
                Unknown1 = puppet.Unknown1,
            };
        }

        [YamlMember(Alias = "puppet", ApplyNamingConventions = false, Order = 10)]
        public Puppet Puppet { get; set; } = new Puppet();

    }
}
