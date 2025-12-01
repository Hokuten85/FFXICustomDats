using FFXICustomDats.YamlModels;
using FFXICustomDats.YamlModels.DataMenu;
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
                    entries.EntryList.Add((Entry)entry);
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
            if (parser.TryConsume<SequenceStart>(out _))
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
            throw new NotImplementedException();
        }
    }
}
