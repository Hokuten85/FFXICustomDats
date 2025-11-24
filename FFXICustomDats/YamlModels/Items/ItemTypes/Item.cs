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
    public partial class Item
    {
        public Item() { }
        public Item(FFXICustomDats.Data.XiDatEntities.Item item, ItemString strings)
        {
            Id = item.ItemId;
            Strings = new Strings()
            {
                Name = strings.Name,
                ArticleType = (ArticleType)strings.ArticleType,
                SingularName = strings.SingularName,
                PluralName = strings.PluralName,
                Description = strings.Description,
            };
            Flags = Helpers.BitsToEnumList<Flag>(item.Flags);
            StackSize = item.StackSize;
            ItemType = (ItemType)item.ItemType;
            ResourceId = item.ResourceId;
            ValidTargets = Helpers.BitsToEnumList<ValidTarget>(item.ValidTargets);
            IconBytes = Encoding.ASCII.GetString(item.IconBytes);
        }

        [YamlMember(Alias = "id", ApplyNamingConventions = false, Order = 1)]
        public ushort Id { get; set; }

        [YamlMember(Alias = "strings", ApplyNamingConventions = false, Order = 2)]
        public Strings Strings { get; set; } = new Strings();

        [YamlMember(Alias = "flags", ApplyNamingConventions = false, Order = 3)]
        public List<Flag> Flags { get; set; } = [];

        [YamlMember(Alias = "stack_size", ApplyNamingConventions = false, Order = 4)]
        public byte StackSize { get; set; }

        [YamlMember(Alias = "item_type", ApplyNamingConventions = false, Order = 5)]
        public ItemType ItemType { get; set; }

        [YamlMember(Alias = "resource_id", ApplyNamingConventions = false, Order = 6)]
        public int ResourceId { get; set; }

        [YamlMember(Alias = "valid_targets", ApplyNamingConventions = false, Order = 7)]
        public List<ValidTarget> ValidTargets { get; set; } = [];

        [YamlMember(Alias = "icon_bytes", ApplyNamingConventions = false, Order = 20)]
        public string IconBytes { get; set; }
    }
}
