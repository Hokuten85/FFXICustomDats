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
    public partial class FurnishingItem : Item
    {
        public FurnishingItem() { }
        public FurnishingItem(Data.XiDatEntities.Item item, ItemString strings, ItemFurnishing furnishing) : base(item, strings)
        {
            Furnishing = new Furnishing()
            {
                Element = (Element)furnishing.Element,
                StorageSlots = furnishing.StorageSlots,
                Unknown3 = furnishing.Unknown3
            };
        }

        [YamlMember(Alias = "furnishing", ApplyNamingConventions = false)]
        public Furnishing Furnishing { get; set; } = new Furnishing();
    }
}
