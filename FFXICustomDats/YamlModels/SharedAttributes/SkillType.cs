using static FFXICustomDats.YamlModels.SharedAttributes.JobHelpers;

namespace FFXICustomDats.YamlModels.SharedAttributes
{
    public enum SkillType 
    {
        None = 0x00,

        HandToHand = 0x01,
        Dagger = 0x02,
        Sword = 0x03,
        GreatSword = 0x04,
        Axe = 0x05,
        GreatAxe = 0x06,
        Scythe = 0x07,
        PoleArm = 0x08,
        Katana = 0x09,
        GreatKatana = 0x0a,
        Club = 0x0b,
        Staff = 0x0c,
        AutomatonMelee = 0x16,
        AutomatonRange = 0x17,
        AutomatonMagic = 0x18,
        Ranged = 0x19,
        Marksmanship = 0x1a,
        Thrown = 0x1b,
        DivineMagic = 0x20,
        HealingMagic = 0x21,
        EnhancingMagic = 0x22,
        EnfeeblingMagic = 0x23,
        ElementalMagic = 0x24,
        DarkMagic = 0x25,
        SummoningMagic = 0x26,
        Ninjutsu = 0x27,
        Singing = 0x28,
        StringInstrument = 0x29,
        WindInstrument = 0x2a,
        BlueMagic = 0x2b,
        Geomancy = 0x2c,
        Handbell = 0x2d,
        Fishing = 0x30,

        Woodworking = 0x31,
        Smithing = 0x32,
        Goldsmithing = 0x33,
        Clothcraft = 0x34,
        Leathercraft = 0x35,
        Bonecraft = 0x36,
        Alchemy = 0x37,
        Cooking = 0x38,

        Special = 0xff,
    };

    public static class SkillTypeHelpers
    {
        public enum SKILL_TYPE
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

        public readonly static Dictionary<SKILL_TYPE, SkillType> SkillTypeMap = new()
        {
            { SKILL_TYPE.SKILL_AXE,                 SkillType.Axe },
            { SKILL_TYPE.SKILL_CLUB,                SkillType.Club },
            { SKILL_TYPE.SKILL_DAGGER,              SkillType.Dagger },
            { SKILL_TYPE.SKILL_FISHING,             SkillType.Fishing },
            { SKILL_TYPE.SKILL_GREAT_AXE,           SkillType.GreatAxe },
            { SKILL_TYPE.SKILL_GREAT_KATANA,        SkillType.GreatKatana },
            { SKILL_TYPE.SKILL_GREAT_SWORD,         SkillType.GreatSword },
            { SKILL_TYPE.SKILL_HANDBELL,            SkillType.Handbell },
            { SKILL_TYPE.SKILL_HAND_TO_HAND,        SkillType.HandToHand },
            { SKILL_TYPE.SKILL_KATANA,              SkillType.Katana },
            { SKILL_TYPE.SKILL_MARKSMANSHIP,        SkillType.Marksmanship },
            { SKILL_TYPE.SKILL_NONE,                SkillType.None },
            { SKILL_TYPE.SKILL_POLEARM,             SkillType.PoleArm },
            { SKILL_TYPE.SKILL_ARCHERY,             SkillType.Ranged },
            { SKILL_TYPE.SKILL_SCYTHE,              SkillType.Scythe },
            { SKILL_TYPE.SKILL_SPECIAL,             SkillType.Special },
            { SKILL_TYPE.SKILL_STAFF,               SkillType.Staff },
            { SKILL_TYPE.SKILL_STRING_INSTRUMENT,   SkillType.StringInstrument },
            { SKILL_TYPE.SKILL_SWORD,               SkillType.Sword },
            { SKILL_TYPE.SKILL_THROWING,            SkillType.Thrown },
            { SKILL_TYPE.SKILL_BLUE_MAGIC,          SkillType.BlueMagic },
            { SKILL_TYPE.SKILL_DARK_MAGIC,          SkillType.DarkMagic },
            { SKILL_TYPE.SKILL_DIVINE_MAGIC,        SkillType.DivineMagic },
            { SKILL_TYPE.SKILL_ELEMENTAL_MAGIC,     SkillType.ElementalMagic },
            { SKILL_TYPE.SKILL_ENFEEBLING_MAGIC,    SkillType.EnfeeblingMagic },
            { SKILL_TYPE.SKILL_ENHANCING_MAGIC,     SkillType.EnhancingMagic },
            { SKILL_TYPE.SKILL_GEOMANCY,            SkillType.Geomancy },
            { SKILL_TYPE.SKILL_HEALING_MAGIC,       SkillType.HealingMagic },
            { SKILL_TYPE.SKILL_NINJUTSU,            SkillType.Ninjutsu },
            { SKILL_TYPE.SKILL_SINGING,             SkillType.Singing },
            { SKILL_TYPE.SKILL_SUMMONING_MAGIC,     SkillType.SummoningMagic },
    
        };

        public static Dictionary<SkillType, SKILL_TYPE> ReverseSkillTypeMap()
        {
            return SkillTypeMap.ToDictionary(x => x.Value, y => y.Key);
        }

        public static bool IsEqual(SkillType yamlSkill, ushort dbSkill)
        {
            return SkillTypeMap.TryGetValue((SKILL_TYPE)dbSkill, out var skillType) && skillType == yamlSkill;
        }
    }
}
