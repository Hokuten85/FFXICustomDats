using static FFXICustomDats.YamlModels.SharedAttributes.SkillTypeHelpers;

namespace FFXICustomDats.YamlModels.SharedAttributes
{
    public enum ValidTarget { Zero = 0, Corpse, Enemy, Object, PartyMember, SelfTarget, Player, Ally, NPC };

    public static class ValidTargetHelpers
    {
        public enum TARGETTYPE
        {
            TARGET_NONE = 0,
            TARGET_SELF,
            TARGET_PLAYER_PARTY,
            TARGET_ENEMY,
            TARGET_PLAYER_ALLIANCE,
            TARGET_PLAYER,
            TARGET_PLAYER_DEAD,
            TARGET_NPC,
            TARGET_PLAYER_PARTY_PIANISSIMO,
            TARGET_PET,
            TARGET_PLAYER_PARTY_ENTRUST,
            TARGET_IGNORE_BATTLEID,
            TARGET_ANY_ALLEGIANCE,
        };

        public readonly static Dictionary<TARGETTYPE, ValidTarget> Map = new()
        {
            { TARGETTYPE.TARGET_NONE,               ValidTarget.Zero },
            { TARGETTYPE.TARGET_SELF,               ValidTarget.SelfTarget },
            { TARGETTYPE.TARGET_PLAYER_PARTY,       ValidTarget.PartyMember },
            { TARGETTYPE.TARGET_ENEMY,              ValidTarget.Enemy },
            { TARGETTYPE.TARGET_PLAYER_ALLIANCE,    ValidTarget.Ally },
            { TARGETTYPE.TARGET_PLAYER_DEAD,        ValidTarget.Corpse },
            { TARGETTYPE.TARGET_NPC,                ValidTarget.NPC },
            { TARGETTYPE.TARGET_PLAYER,             ValidTarget.Player },
        };

        public static Dictionary<ValidTarget, TARGETTYPE> RMap()
        {
            return Map.ToDictionary(x => x.Value, y => y.Key);
        }

        public static bool IsEqual(List<ValidTarget> slotList, ushort dbValidTargets)
        {
            var dbList = Helpers.DBValueToYamlList(Map, dbValidTargets);
            return Helpers.AreEqual(slotList, dbList);
        }
    }

}
