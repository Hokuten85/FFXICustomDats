using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public enum ItemType { None = 0, Armor, BettingSlip, Book, Flowerpot, Furnishing, Item, Linkshell, LotteryTicket, Mannequin, Plant, QuestItem, RacingForm, Reflector, SoulPlate, UsableItem, Weapon, Crystal, Fish };

    public partial class ItemTypeConversion
    {
        private enum ITEM_TYPE
        {
            ITEM_BASIC = 0x00,
            ITEM_GENERAL = 0x01,
            ITEM_USABLE = 0x02,
            ITEM_PUPPET = 0x04,
            ITEM_EQUIPMENT = 0x08,
            ITEM_WEAPON = 0x10,
            ITEM_CURRENCY = 0x20,
            ITEM_FURNISHING = 0x40,
            ITEM_LINKSHELL = 0x80,
        };
    }  
}
