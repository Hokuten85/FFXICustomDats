using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.SharedAttributes;
using static FFXICustomDats.YamlModels.Items.ItemAttributes.FlagHelpers;
using static FFXICustomDats.YamlModels.SharedAttributes.SkillTypeHelpers;

namespace FFXICustomDats.YamlModels.DataMenu.Attributes
{
    public enum MagicType 
    { 
        None,
        BardSong,
        BlackMagic,
        BlueMagic,
        Ninjutsu,
        SummonerPact,
        WhiteMagic,
        Geomancy,
        TrustMagic = 8
    };

    public static class MagicTypeHelpers
    {
        public enum SPELLGROUP
        {
            SPELLGROUP_NONE = 0,
            SPELLGROUP_SONG = 1,
            SPELLGROUP_BLACK = 2,
            SPELLGROUP_BLUE = 3,
            SPELLGROUP_NINJUTSU = 4,
            SPELLGROUP_SUMMONING = 5,
            SPELLGROUP_WHITE = 6,
            SPELLGROUP_GEOMANCY = 7,
            SPELLGROUP_TRUST = 8
        };

        public static readonly Dictionary<SPELLGROUP, MagicType> Map = new()
        {
            { SPELLGROUP.SPELLGROUP_NONE,       MagicType.None },
            { SPELLGROUP.SPELLGROUP_WHITE,      MagicType.WhiteMagic },
            { SPELLGROUP.SPELLGROUP_BLACK,      MagicType.BlackMagic },
            { SPELLGROUP.SPELLGROUP_SUMMONING,  MagicType.SummonerPact },
            { SPELLGROUP.SPELLGROUP_NINJUTSU,   MagicType.Ninjutsu },
            { SPELLGROUP.SPELLGROUP_SONG,       MagicType.BardSong },
            { SPELLGROUP.SPELLGROUP_BLUE,       MagicType.BlueMagic },
            { SPELLGROUP.SPELLGROUP_GEOMANCY,   MagicType.Geomancy },
            { SPELLGROUP.SPELLGROUP_TRUST,      MagicType.TrustMagic },
        };

        public static Dictionary<MagicType, SPELLGROUP> RMap()
        {
            return Map.ToDictionary(x => x.Value, y => y.Key);
        }

        public static bool IsEqual(MagicType yamlMagicType, ushort dbSpellGroup)
        {
            return Map.TryGetValue((SPELLGROUP)dbSpellGroup, out var magicType) && magicType == yamlMagicType;
        }
    }
}
