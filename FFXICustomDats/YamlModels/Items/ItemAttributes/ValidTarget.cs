namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public enum ValidTarget { Zero = 0, Corpse, Enemy, Object, PartyMember, SelfTarget, Ally, NPC };

    public partial class ValidTargetConversion
    {
        private enum TARGETTYPE
        {
            TARGET_NONE                    = 0x0000,
            TARGET_SELF                    = 0x0001,
            TARGET_PLAYER_PARTY            = 0x0002,
            TARGET_ENEMY                   = 0x0004,
            TARGET_PLAYER_ALLIANCE         = 0x0008,
            TARGET_PLAYER                  = 0x0010,
            TARGET_PLAYER_DEAD             = 0x0020,
            TARGET_NPC                     = 0x0040, // an npc is a mob that looks like an npc and fights on the side of the character
            TARGET_PLAYER_PARTY_PIANISSIMO = 0x0080,
            TARGET_PET                     = 0x0100,
            TARGET_PLAYER_PARTY_ENTRUST    = 0x0200,
            TARGET_IGNORE_BATTLEID         = 0x0400, // Can hit targets that do not have the same battle ID
            TARGET_ANY_ALLEGIANCE          = 0x0800, // Can hit targets from any allegiance simultaneously. To be used with other flags above and only makes sense for non-single-target skills
        };

        private readonly static Dictionary<TARGETTYPE, ValidTarget> ValidTargetDict = new()
        {
            //{ TARGETTYPE.TARGET_NONE, ValidTarget.WallHanging },
            { TARGETTYPE.TARGET_SELF, ValidTarget.SelfTarget },
            { TARGETTYPE.TARGET_PLAYER_PARTY, ValidTarget.PartyMember },
            { TARGETTYPE.TARGET_ENEMY, ValidTarget.Enemy },
            { TARGETTYPE.TARGET_PLAYER_ALLIANCE, ValidTarget.Ally },
            //{ TARGETTYPE.TARGET_PLAYER, ValidTarget.Inscribable },
            { TARGETTYPE.TARGET_PLAYER_DEAD, ValidTarget.Corpse },
            //{ TARGETTYPE.TARGET_NPC, ValidTarget.Object },
            { TARGETTYPE.TARGET_NPC, ValidTarget.NPC },
            //{ TARGETTYPE.TARGET_PLAYER_PARTY_PIANISSIMO, ValidTarget.Linkshell },
            //{ TARGETTYPE.TARGET_PET, ValidTarget.CanUse },
            //{ TARGETTYPE.TARGET_PLAYER_PARTY_ENTRUST, ValidTarget.CanTradeNpc },
            //{ TARGETTYPE.TARGET_IGNORE_BATTLEID, ValidTarget.CanEquip },
            //{ TARGETTYPE.TARGET_ANY_ALLEGIANCE, ValidTarget.NoSale },
        };

        //public static List<ValidTarget> ConvertBitValidTargetsToYaml(ushort validTargets, bool vaidTargetObject)
        //{
        //    var validTargetList = new List<ValidTarget>();
        //    foreach (var target in ValidTargetDict.Keys)
        //    {
        //        if ((validTargets & (ushort)target) > 0)
        //        {
        //            if (target == TARGETTYPE.TARGET_NPC && vaidTargetObject)
        //            {
        //                validTargetList.Add(ValidTarget.Object);
        //            }
        //            else if (ValidTargetDict.TryGetValue(target, out ValidTarget validTarget))
        //            {
        //                validTargetList.Add(validTarget);
        //            }
        //        }
        //    }

        //    return validTargetList;
        //}
    }  
}
