using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.SharedAttributes;
using YamlDotNet.Serialization;

namespace FFXICustomDats.YamlModels.DataMenu
{
    public class Entries
    {
        public List<Spell> SpellList = [];
        public List<Ability> AbilityList = [];
        public string String;

        public static implicit operator Entries(List<Spell> SpellList) => new() { SpellList = SpellList };
        public static implicit operator Entries(List<Ability> AbilityList) => new() { AbilityList = AbilityList };
        public static implicit operator Entries(string String) => new() { String = String };
    }
}
