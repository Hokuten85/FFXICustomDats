using FFXICustomDats.Data;
using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using FFXICustomDats.YamlModels.SharedAttributes;
using Microsoft.Extensions.Configuration;
using static FFXICustomDats.YamlModels.DataMenu.Attributes.MagicTypeHelpers;
using static FFXICustomDats.YamlModels.SharedAttributes.SkillTypeHelpers;

namespace FFXICustomDats
{
    public class PatchDataMenuFromDB(IConfiguration config, XidbContext context)
    {
        private readonly XidbContext _context = context;
        private readonly IConfiguration _config = config;

        public void UpdateDataMenu(List<Entry> entries)
        {
            var spellIds = entries.Select(i => i.Id);
            var spellList = _context.SpellLists.Where(x => spellIds.Contains(x.Spellid)).ToList();

            foreach (var spell in entries)
            {
                var dbSpell = spellList.FirstOrDefault(x => x.Spellid == spell.Id);
                if (dbSpell != null)
                {
                    UpdateSpell(spell, dbSpell);
                }
            }
        }

        private static void UpdateSpell(Entry spell, SpellList dbSpell)
        {
            if (!MagicTypeHelpers.IsEqual(spell.MagicType, dbSpell.Group))
            {
                spell.MagicType = MagicTypeHelpers.MagicTypeMap.GetValueOrDefault((SPELLGROUP)dbSpell.Group);
            }

            if (spell.Element != (Element)dbSpell.Element)
            {
                spell.Element = (Element)dbSpell.Element;
            }

            if (!ValidTargetHelpers.IsEqual(spell.ValidTargets, dbSpell.ValidTargets))
            {
                spell.ValidTargets = Helpers.DBValueToYamlList(ValidTargetHelpers.ValidTargetMap, dbSpell.ValidTargets);
            }

            if (!SkillTypeHelpers.IsEqual(spell.SkillType, dbSpell.Skill))
            {
                spell.SkillType = SkillTypeHelpers.SkillTypeMap.GetValueOrDefault((SKILL_TYPE)dbSpell.Skill);
            }

            if (spell.MpCost != dbSpell.MpCost)
            {
                spell.MpCost = dbSpell.MpCost;
            }

            if (spell.CastTime != dbSpell.CastTime)
            {
                spell.CastTime = dbSpell.CastTime;
            }

            if (spell.RecastTime != dbSpell.RecastTime)
            {
                spell.RecastTime = dbSpell.RecastTime;
            }

            if (spell.LevelRequired != dbSpell.Jobs)
            {
                spell.LevelRequired = dbSpell.Jobs;
            }
        }
    }
}
