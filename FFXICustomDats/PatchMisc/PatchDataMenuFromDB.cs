using FFXICustomDats.Data;
using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using FFXICustomDats.YamlModels.SharedAttributes;
using Microsoft.Extensions.Configuration;
using System.Linq;
using static FFXICustomDats.YamlModels.DataMenu.Attributes.MagicTypeHelpers;
using static FFXICustomDats.YamlModels.SharedAttributes.SkillTypeHelpers;
using Ability = FFXICustomDats.YamlModels.DataMenu.Ability;

namespace FFXICustomDats
{
    public class PatchDataMenuFromDB(IConfiguration config, XidbContext context)
    {
        private readonly XidbContext _context = context;
        private readonly IConfiguration _config = config;

        public void UpdateSpells(List<Spell> entries)
        {
            var spellIds = entries.Select(i => i.Index);
            var spellList = _context.SpellLists.Where(x => spellIds.Contains(x.Spellid)).ToList();

            foreach (var spell in entries)
            {
                var dbSpell = spellList.FirstOrDefault(x => x.Spellid == spell.Index);
                if (dbSpell != null)
                {
                    UpdateSpell(spell, dbSpell);
                }
            }
        }

        private static void UpdateSpell(Spell spell, SpellList dbSpell)
        {
            if (!MagicTypeHelpers.IsEqual(spell.MagicType.Value, dbSpell.Group))
            {
                spell.MagicType = MagicTypeHelpers.Map.GetValueOrDefault((SPELLGROUP)dbSpell.Group);
            }

            if (spell.Element != (Element)dbSpell.Element)
            {
                spell.Element = (Element)dbSpell.Element;
            }

            if (!ValidTargetHelpers.IsEqual(spell.ValidTargets, dbSpell.ValidTargets))
            {
                spell.ValidTargets = Helpers.DBValueToYamlList(ValidTargetHelpers.Map, dbSpell.ValidTargets);
            }

            if (!SkillTypeHelpers.IsEqual(spell.SkillType, dbSpell.Skill))
            {
                spell.SkillType = SkillTypeHelpers.Map.GetValueOrDefault((SKILL_TYPE)dbSpell.Skill);
            }

            if (spell.MpCost != dbSpell.MpCost && spell.MagicType != MagicType.Ninjutsu)
            {
                spell.MpCost = dbSpell.MpCost;
            }

            if (spell.CastTime != (dbSpell.CastTime * 4 / 1000))
            {
                spell.CastTime = (dbSpell.CastTime * 4 / 1000);
            }

            if (spell.RecastTime != (dbSpell.RecastTime * 4 / 1000))
            {
                spell.RecastTime = (dbSpell.RecastTime * 4 / 1000);
            }

            if (!JobHelpers.IsEqual(spell.LevelRequired, dbSpell.Jobs))
            {
                spell.LevelRequired = JobHelpers.DBByteArrayToYamlDict(dbSpell.Jobs);
            }
        }
    }
}
