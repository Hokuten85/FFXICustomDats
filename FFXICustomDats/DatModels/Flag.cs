using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXICustomDats.DatModels
{
    public enum Flag { CanEquip, CanSendPol, CanTradeNpc, CanUse, Ex, Flag01, Inscribable, Linkshell, MogGarden, MysteryBox, NoAuction, NoDelivery, NoSale, NoTradePC, Rare, Scroll, WallHanging };

    public class FlagConversion
    {
        private enum ITEM_FLAG
        {
            ITEM_FLAG_WALLHANGING = 0x0001,
            ITEM_FLAG_01 = 0x0002,
            ITEM_FLAG_MYSTERY_BOX = 0x0004, // Can be gained from Gobbie Mystery Box
            ITEM_FLAG_MOG_GARDEN = 0x0008, // Can use in Mog Garden
            ITEM_FLAG_MAIL2ACCOUNT = 0x0010, // CanSendPOL Polutils Value
            ITEM_FLAG_INSCRIBABLE = 0x0020,
            ITEM_FLAG_NOAUCTION = 0x0040,
            ITEM_FLAG_SCROLL = 0x0080,
            ITEM_FLAG_LINKSHELL = 0x0100, // Linkshell Polutils Value
            ITEM_FLAG_CANUSE = 0x0200,
            ITEM_FLAG_CANTRADENPC = 0x0400,
            ITEM_FLAG_CANEQUIP = 0x0800,
            ITEM_FLAG_NOSALE = 0x1000,
            ITEM_FLAG_NODELIVERY = 0x2000,
            ITEM_FLAG_EX = 0x4000, // NoTradePC Polutils Value
            ITEM_FLAG_RARE = 0x8000,
        };

        private readonly static Dictionary<ITEM_FLAG, Flag> FlagDict = new()
        {
            { ITEM_FLAG.ITEM_FLAG_WALLHANGING, Flag.WallHanging },
            { ITEM_FLAG.ITEM_FLAG_01, Flag.Flag01 },
            { ITEM_FLAG.ITEM_FLAG_MYSTERY_BOX, Flag.MysteryBox },
            { ITEM_FLAG.ITEM_FLAG_MOG_GARDEN, Flag.MogGarden },
            { ITEM_FLAG.ITEM_FLAG_MAIL2ACCOUNT, Flag.CanSendPol },
            { ITEM_FLAG.ITEM_FLAG_INSCRIBABLE, Flag.Inscribable },
            { ITEM_FLAG.ITEM_FLAG_NOAUCTION, Flag.NoAuction },
            { ITEM_FLAG.ITEM_FLAG_SCROLL, Flag.Scroll },
            { ITEM_FLAG.ITEM_FLAG_LINKSHELL, Flag.Linkshell },
            { ITEM_FLAG.ITEM_FLAG_CANUSE, Flag.CanUse },
            { ITEM_FLAG.ITEM_FLAG_CANTRADENPC, Flag.CanTradeNpc },
            { ITEM_FLAG.ITEM_FLAG_CANEQUIP, Flag.CanEquip },
            { ITEM_FLAG.ITEM_FLAG_NOSALE, Flag.NoSale },
            { ITEM_FLAG.ITEM_FLAG_NODELIVERY, Flag.NoDelivery },
            { ITEM_FLAG.ITEM_FLAG_EX, Flag.Ex },
            { ITEM_FLAG.ITEM_FLAG_RARE, Flag.Rare },
        };

        private readonly static Dictionary<Flag, ITEM_FLAG> ReverseFlagDict = new()
        {
            { Flag.WallHanging, ITEM_FLAG.ITEM_FLAG_WALLHANGING },
            { Flag.Flag01     , ITEM_FLAG.ITEM_FLAG_01 },
            { Flag.MysteryBox , ITEM_FLAG.ITEM_FLAG_MYSTERY_BOX },
            { Flag.MogGarden  , ITEM_FLAG.ITEM_FLAG_MOG_GARDEN },
            { Flag.CanSendPol , ITEM_FLAG.ITEM_FLAG_MAIL2ACCOUNT },
            { Flag.Inscribable, ITEM_FLAG.ITEM_FLAG_INSCRIBABLE },
            { Flag.NoAuction  , ITEM_FLAG.ITEM_FLAG_NOAUCTION },
            { Flag.Scroll     , ITEM_FLAG.ITEM_FLAG_SCROLL },
            { Flag.Linkshell  , ITEM_FLAG.ITEM_FLAG_LINKSHELL },
            { Flag.CanUse     , ITEM_FLAG.ITEM_FLAG_CANUSE },
            { Flag.CanTradeNpc, ITEM_FLAG.ITEM_FLAG_CANTRADENPC },
            { Flag.CanEquip   , ITEM_FLAG.ITEM_FLAG_CANEQUIP },
            { Flag.NoSale     , ITEM_FLAG.ITEM_FLAG_NOSALE },
            { Flag.NoDelivery , ITEM_FLAG.ITEM_FLAG_NODELIVERY },
            { Flag.Ex         , ITEM_FLAG.ITEM_FLAG_EX },
            { Flag.Rare       , ITEM_FLAG.ITEM_FLAG_RARE },
        };

        public static List<Flag> ConvertBitFlagsToYaml(ushort flags, bool noTradePC)
        {
            var flagList = new List<Flag>();
            foreach (var itemFlag in FlagDict.Keys)
            {
                if ((flags & (ushort)itemFlag) > 0)
                {
                    if (itemFlag == ITEM_FLAG.ITEM_FLAG_EX && noTradePC)
                    {
                        flagList.Add(Flag.NoTradePC);
                    }
                    else if (FlagDict.TryGetValue(itemFlag, out Flag flag))
                    {
                        flagList.Add(flag);
                    }
                }
            }
            return flagList;
        }

        public static ushort ConvertYamlFlagsToBit(List<Flag> flags)
        {
            uint bitFlags = 0;
            foreach (var yamlFlag in flags)
            {
                if (ReverseFlagDict.TryGetValue(yamlFlag, out ITEM_FLAG bitValue))
                {
                    bitFlags = bitFlags | (uint)bitValue;
                }
            }
            return (ushort)bitFlags;
        }
    }
}

