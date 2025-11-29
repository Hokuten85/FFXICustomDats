namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public enum Flag { Zero = 0, CanEquip, CanSendPOL, CanTradeNPC, CanUse, Ex, Flag01, Inscribable, Linkshell, MogGarden, MysteryBox, NoAuction, NoDelivery, NoSale, NoTradePC, Rare, Scroll, WallHanging };

    public static class FlagHelpers
    {
        public enum ITEM_FLAG
        {
            Zero = 0,
            ITEM_FLAG_WALLHANGING,
            ITEM_FLAG_01,
            ITEM_FLAG_MYSTERY_BOX, // Can be gained from Gobbie Mystery Box
            ITEM_FLAG_MOG_GARDEN, // Can use in Mog Garden
            ITEM_FLAG_MAIL2ACCOUNT, // CanSendPOL Polutils Value
            ITEM_FLAG_INSCRIBABLE,
            ITEM_FLAG_NOAUCTION,
            ITEM_FLAG_SCROLL,
            ITEM_FLAG_LINKSHELL, // Linkshell Polutils Value
            ITEM_FLAG_CANUSE,
            ITEM_FLAG_CANTRADENPC,
            ITEM_FLAG_CANEQUIP,
            ITEM_FLAG_NOSALE,
            ITEM_FLAG_NODELIVERY,
            ITEM_FLAG_EX, // NoTradePC Polutils Value
            ITEM_FLAG_RARE,
        };

        public static readonly Dictionary<ITEM_FLAG, Flag> FlagMap = new()
        {
            { ITEM_FLAG.Zero,                   Flag.Zero },
            { ITEM_FLAG.ITEM_FLAG_WALLHANGING,  Flag.WallHanging },
            { ITEM_FLAG.ITEM_FLAG_01,           Flag.Flag01 },
            { ITEM_FLAG.ITEM_FLAG_MYSTERY_BOX,  Flag.MysteryBox },
            { ITEM_FLAG.ITEM_FLAG_MOG_GARDEN,   Flag.MogGarden },
            { ITEM_FLAG.ITEM_FLAG_MAIL2ACCOUNT, Flag.CanSendPOL },
            { ITEM_FLAG.ITEM_FLAG_INSCRIBABLE,  Flag.Inscribable },
            { ITEM_FLAG.ITEM_FLAG_NOAUCTION,    Flag.NoAuction },
            { ITEM_FLAG.ITEM_FLAG_SCROLL,       Flag.Scroll },
            { ITEM_FLAG.ITEM_FLAG_LINKSHELL,    Flag.Linkshell },
            { ITEM_FLAG.ITEM_FLAG_CANUSE,       Flag.CanUse },
            { ITEM_FLAG.ITEM_FLAG_CANTRADENPC,  Flag.CanTradeNPC },
            { ITEM_FLAG.ITEM_FLAG_CANEQUIP,     Flag.CanEquip },
            { ITEM_FLAG.ITEM_FLAG_NOSALE,       Flag.NoSale },
            { ITEM_FLAG.ITEM_FLAG_NODELIVERY,   Flag.NoDelivery },
            { ITEM_FLAG.ITEM_FLAG_EX,           Flag.Ex },
            { ITEM_FLAG.ITEM_FLAG_RARE,         Flag.Rare }
        };

        public static bool IsEqual(List<Flag> flagList, ushort dbFlags)
        {
            var dbList = Helpers.DBFlagsToYamlFlags(FlagMap, dbFlags);

            return Helpers.AreEqual(flagList, dbList);
        }
    }
}

