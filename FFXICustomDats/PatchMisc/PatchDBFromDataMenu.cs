using FFXICustomDats.Data;
using FFXICustomDats.Data.XidbEntities;
using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.SharedAttributes;
using Microsoft.Extensions.Configuration;
using static FFXICustomDats.YamlModels.DataMenu.Attributes.MagicTypeHelpers;
using static FFXICustomDats.YamlModels.SharedAttributes.SkillTypeHelpers;

namespace FFXICustomDats
{
    public class PatchDBFromDataMenu(IConfiguration config, XidbContext context)
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
                    UpdateSpellLists(spell, dbSpell);
                }
            }

            _context.SaveChanges();
        }

        private static void UpdateSpellLists(Spell spell, SpellList dbSpell)
        {
            if (!MagicTypeHelpers.IsEqual(spell.MagicType.Value, dbSpell.Group))
            {
                dbSpell.Group = (byte)MagicTypeHelpers.RMap().GetValueOrDefault(spell.MagicType.Value);
            }

            if (spell.Element != (Element)dbSpell.Element)
            {
                dbSpell.Element = (byte)spell.Element.GetValueOrDefault();
            }

            if (!ValidTargetHelpers.IsEqual(spell.ValidTargets, dbSpell.ValidTargets))
            {
                dbSpell.ValidTargets = (ushort)Helpers.YamlListToDBValue(ValidTargetHelpers.RMap(), spell.ValidTargets);
            }

            if (!SkillTypeHelpers.IsEqual(spell.SkillType, dbSpell.Skill))
            {
                dbSpell.Skill = (byte)SkillTypeHelpers.RMap().GetValueOrDefault(spell.SkillType);
            }

            if (spell.MpCost != dbSpell.MpCost && spell.MagicType != MagicType.Ninjutsu)
            {
                dbSpell.MpCost = (ushort)spell.MpCost;
            }

            if (spell.CastTime != (dbSpell.CastTime * 4 / 1000))
            {
                //spell.CastTime = (dbSpell.CastTime * 4 / 1000);

                dbSpell.CastTime = (ushort)(spell.CastTime * 1000 / 4);
            }

            if (spell.RecastTime != (dbSpell.RecastTime * 4 / 1000))
            {
                //spell.RecastTime = (dbSpell.RecastTime * 4 / 1000);

                dbSpell.RecastTime = (uint)(spell.RecastTime * 1000 / 4);
            }

            if (JobHelpers.IsEqual(spell.LevelRequired, dbSpell.Jobs))
            {
                dbSpell.Jobs = JobHelpers.YamlDictToDBByteArray(spell.LevelRequired);
            }
        }
    }
}
