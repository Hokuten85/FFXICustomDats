namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public enum SkillType { None = 0, Axe, Club, Dagger, Fishing, GreatAxe, GreatKatana, GreatSword, Handbell, HandToHand, Katana, Marksmanship, PoleArm, Ranged, Scythe, Special, Staff, StringInstrument, Sword, Thrown, WindInstrument };

    public static class SkillTypeHelpers
    {
        public enum SKILLTYPE
        {
            SKILL_NONE = 0,
            SKILL_HAND_TO_HAND = 1,
            SKILL_DAGGER = 2,
            SKILL_SWORD = 3,
            SKILL_GREAT_SWORD = 4,
            SKILL_AXE = 5,
            SKILL_GREAT_AXE = 6,
            SKILL_SCYTHE = 7,
            SKILL_POLEARM = 8,
            SKILL_KATANA = 9,
            SKILL_GREAT_KATANA = 10,
            SKILL_CLUB = 11,
            SKILL_STAFF = 12,
            // 13~21 unused
            SKILL_AUTOMATON_MELEE = 22,
            SKILL_AUTOMATON_RANGED = 23,
            SKILL_AUTOMATON_MAGIC = 24,
            SKILL_ARCHERY = 25,
            SKILL_MARKSMANSHIP = 26,
            SKILL_THROWING = 27,
            SKILL_GUARD = 28,
            SKILL_EVASION = 29,
            SKILL_SHIELD = 30,
            SKILL_PARRY = 31,
            SKILL_DIVINE_MAGIC = 32,
            SKILL_HEALING_MAGIC = 33,
            SKILL_ENHANCING_MAGIC = 34,
            SKILL_ENFEEBLING_MAGIC = 35,
            SKILL_ELEMENTAL_MAGIC = 36,
            SKILL_DARK_MAGIC = 37,
            SKILL_SUMMONING_MAGIC = 38,
            SKILL_NINJUTSU = 39,
            SKILL_SINGING = 40,
            SKILL_STRING_INSTRUMENT = 41,
            SKILL_WIND_INSTRUMENT = 42,
            SKILL_BLUE_MAGIC = 43,
            SKILL_GEOMANCY = 44,
            SKILL_HANDBELL = 45,
            // 46-47 unused
            SKILL_FISHING = 48,
            SKILL_WOODWORKING = 49,
            SKILL_SMITHING = 50,
            SKILL_GOLDSMITHING = 51,
            SKILL_CLOTHCRAFT = 52,
            SKILL_LEATHERCRAFT = 53,
            SKILL_BONECRAFT = 54,
            SKILL_ALCHEMY = 55,
            SKILL_COOKING = 56,
            SKILL_SYNERGY = 57,
            SKILL_RID = 58,
            SKILL_DIG = 59,
            SKILL_SPECIAL = 255,
        };

        public readonly static Dictionary<SKILLTYPE, SkillType> SkillTypeMap = new()
        {
            { SKILLTYPE.SKILL_AXE,               SkillType.Axe },
            { SKILLTYPE.SKILL_CLUB,              SkillType.Club },
            { SKILLTYPE.SKILL_DAGGER,            SkillType.Dagger },
            { SKILLTYPE.SKILL_FISHING,           SkillType.Fishing },
            { SKILLTYPE.SKILL_GREAT_AXE,         SkillType.GreatAxe },
            { SKILLTYPE.SKILL_GREAT_KATANA,      SkillType.GreatKatana },
            { SKILLTYPE.SKILL_GREAT_SWORD,       SkillType.GreatSword },
            { SKILLTYPE.SKILL_HANDBELL,          SkillType.Handbell },
            { SKILLTYPE.SKILL_HAND_TO_HAND,      SkillType.HandToHand },
            { SKILLTYPE.SKILL_KATANA,            SkillType.Katana },
            { SKILLTYPE.SKILL_MARKSMANSHIP,      SkillType.Marksmanship },
            { SKILLTYPE.SKILL_NONE,              SkillType.None },
            { SKILLTYPE.SKILL_POLEARM,           SkillType.PoleArm },
            { SKILLTYPE.SKILL_ARCHERY,           SkillType.Ranged },
            { SKILLTYPE.SKILL_SCYTHE,            SkillType.Scythe },
            { SKILLTYPE.SKILL_SPECIAL,           SkillType.Special },
            { SKILLTYPE.SKILL_STAFF,             SkillType.Staff },
            { SKILLTYPE.SKILL_STRING_INSTRUMENT, SkillType.StringInstrument },
            { SKILLTYPE.SKILL_SWORD,             SkillType.Sword },
            { SKILLTYPE.SKILL_THROWING,          SkillType.Thrown },
            { SKILLTYPE.SKILL_WIND_INSTRUMENT,   SkillType.WindInstrument },
        };

        public static bool IsEqual(SkillType yamlSkill, ushort dbSkill)
        {
            return SkillTypeMap.TryGetValue((SKILLTYPE)dbSkill, out var skillType) && skillType == yamlSkill;
        }
    }
}
