using FFXICustomDats.YamlModels;
using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.DataMenu.Attributes;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using FFXICustomDats.YamlModels.SharedAttributes;
using System.Text.RegularExpressions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FFXICustomDats.YamlConverters
{
    public class EntriesTypeConverter : IYamlTypeConverter
    {
        public EntriesTypeConverter() { }
        public bool Accepts(Type type)
        {
            return type == typeof(Entries);
        }

        private static Entries ParseSequence(IParser parser, ObjectDeserializer rootDeserializer)
        {
            var entries = new Entries();

            while (!parser.Accept<SequenceEnd>(out _))
            {

                var entry = rootDeserializer.Invoke(typeof(Entry));
                if (entry != null)
                {
                    if (entry.GetType() == typeof(Ability))
                    {
                        entries.AbilityList.Add((Ability)entry);
                    }
                    else if (entry.GetType() == typeof(Spell))
                    {
                        entries.SpellList.Add((Spell)entry);
                    }
                }
            }
            // Consume the mapping end token
            parser.MoveNext();
            return entries;
        }

        private static Entries ParseScalar(Scalar scalar)
        {
            var entries = new Entries();
            if (scalar.Value.GetType() == typeof(String))
            {
                entries.String = scalar.Value;
            }
            return entries;
        }

        public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
        {
            if (parser.TryConsume<SequenceStart>(out var sequence))
            {
                return ParseSequence(parser, rootDeserializer); // We're parsing a YAML array
            }

            if (parser.TryConsume<Scalar>(out var scalar))
            {
                return ParseScalar(scalar);
            }

            return new Entries();
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
        {
            if (!string.IsNullOrWhiteSpace((value as Entries)?.String))
            {
                emitter.Emit(new Scalar(AnchorName.Empty, TagName.Empty, (value as Entries)?.String ?? string.Empty, ScalarStyle.Plain, isPlainImplicit: true, isQuotedImplicit: false));
            }
            else if ((value as Entries)?.SpellList?.Count > 0)
            {
                emitter.Emit(new SequenceStart(AnchorName.Empty, TagName.Empty, isImplicit: true, SequenceStyle.Block));

                var spellList = (value as Entries)?.SpellList ?? [];

                foreach (var entry in spellList)
                {
                    serializer.Invoke(entry);
                }

                emitter.Emit(new SequenceEnd());
            }
            else if (((value as Entries)?.AbilityList?.Count > 0))
            {
                emitter.Emit(new SequenceStart(AnchorName.Empty, TagName.Empty, isImplicit: true, SequenceStyle.Block));

                var abilityList = (value as Entries)?.AbilityList ?? [];

                foreach (var ability in abilityList)
                {
                    serializer.Invoke(ability);
                }

                emitter.Emit(new SequenceEnd());
            }
        }
    }

    //public class UnknownsTypeConverter : IYamlTypeConverter
    //{
    //    public UnknownsTypeConverter() { }
    //    public bool Accepts(Type type)
    //    {
    //        return type == typeof(Unknowns);
    //    }

    //    public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    //    {
    //        return rootDeserializer.Invoke(type);
    //    }

    //    public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
    //    {
    //        if (!string.IsNullOrWhiteSpace((value as Unknowns)?.String))
    //        {
    //            emitter.Emit(new Scalar(AnchorName.Empty, TagName.Empty, $"{(value as Unknowns)?.String}", ScalarStyle.Plain, isPlainImplicit: true, isQuotedImplicit: false));
    //        }
    //        else if (value is Unknowns { Double: not null })
    //        {
    //            emitter.Emit(new Scalar(AnchorName.Empty, TagName.Empty, $"{(value as Unknowns)?.Double}", ScalarStyle.Plain, isPlainImplicit: true, isQuotedImplicit: false));
    //        }
    //    }
    //}
}
