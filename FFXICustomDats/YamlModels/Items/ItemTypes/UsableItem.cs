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

namespace FFXICustomDats.YamlModels.Items.ItemTypes
{
    public partial class UsableItem : Item
    {
        public UsableItem() { }
        public UsableItem(Data.XiDatEntities.Item item, Data.XiDatEntities.ItemString strings, ItemUsable usable) : base(item, strings)
        {
            Usable = new Usable()
            {
                ActivationTime = usable.ActivationTime,
                Unknown1 = usable.Unknown1,
                Unknown2 = usable.Unknown2,
                Unknown3 = usable.Unknown3,
            };
        }

        [YamlMember(Alias = "usable_item", ApplyNamingConventions = false, Order = 10)]
        public Usable Usable { get; set; } = new Usable();

    }
}
