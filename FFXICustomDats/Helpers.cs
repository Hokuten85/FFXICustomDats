using FFXICustomDats.YamlConverters;
using FFXICustomDats.YamlModels;
using FFXICustomDats.YamlModels.DataMenu;
using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using FFXICustomDats.YamlModels.SharedAttributes;
using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FFXICustomDats
{
    public static class Helpers
    {
        public static XIItems<T> DeserializeYaml<T>(string filePath) where T : Item
        {
            var (deserializer, input) = GetInputAndDeserializer(filePath);

            return deserializer.Deserialize<XIItems<T>>(input);
        }

        public static XIDataMenu DeserializeYaml(string filePath)
        {
            var (deserializer, input) = GetInputAndDeserializer(filePath);

            return deserializer.Deserialize<XIDataMenu>(input);
        }

        private static (IDeserializer, StringReader) GetInputAndDeserializer(string filePath)
        {
            FileStream fileStream = new(filePath, FileMode.Open);
            using var reader = new StreamReader(fileStream);

            return (
                new DeserializerBuilder()
                    .WithTypeConverter(new EntriesTypeConverter())
                    .WithTypeDiscriminatingNodeDeserializer((o) =>
                    {
                        IDictionary<string, Type> keyMappings = new Dictionary<string, Type>
                        {
                            { "magic_type", typeof(Spell) },
                            { "ability_type", typeof(Ability) }
                        };
                        o.AddUniqueKeyTypeDiscriminator<Entry>(keyMappings);
                    })
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build(),
                new StringReader(reader.ReadToEnd())
            );
        }

        public static String SerializeToYaml(object thing)
        {
            Console.WriteLine($"Serializing");

            var serializer = new SerializerBuilder()
                .WithTypeConverter(new EntriesTypeConverter())
                .Build();
            return serializer.Serialize(thing);
        }

        public static void WriteNewYamlFile(string yaml, string filePath)
        {
            Console.WriteLine($"Writing file {filePath}");
            if (!Path.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);
            }

            using StreamWriter writetext = new(filePath);
            writetext.Write(yaml);
            writetext.Flush();
            writetext.Close();
            writetext.Dispose();
        }

        public static void DeepCopy<T>(T original, T newItem) where T : class
        {
            foreach (var prop in original.GetType().GetProperties())
            {
                if (!(prop.GetValue(original) == prop.GetValue(newItem))
                    && prop.GetValue(newItem) != (prop.GetType().IsValueType ? Activator.CreateInstance(prop.GetType()) : null))
                {
                    prop.SetValue(original, prop.GetValue(newItem));
                }
            }
        }

        public static List<T> BitsToEnumList<T>(ushort bits) where T : Enum
        {
            return BitsToEnumList<T>((uint)bits);
        }

        public static List<T> BitsToEnumList<T>(uint bits) where T : Enum
        {
            var enumList = new List<T>();
            if (bits == 0)
            {
                // Enum "None" is a valid value. Enum "Zero" is my hack to ensure I return an empty list
                if (!Enum.IsDefined(typeof(T), "Zero") && Enum.TryParse(typeof(T), bits.ToString(), out var zeroEnum))
                {
                    enumList.Add((T)zeroEnum);
                }
            }
            else
            {
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    if ((bits & 1 << (int)value - 1) > 0)
                    {
                        enumList.Add((T)value);
                    }
                }
            }

            return enumList;
        }

        public static ushort ConvertEnumListToBit(List<Race> raceList)
        {
            return (ushort)raceList.Cast<int>().Aggregate(0, (total, next) => total | next);
        }

        public static bool AreEqual<T>(List<T> list1, List<T> list2) where T : Enum
        {
            return list1.All(list2.Contains) && list1.Count == list2.Count;
        }

        public static bool AreEqual<TKey, TValue>(Dictionary<TKey, TValue> dict1, Dictionary<TKey, TValue> dict2) where TKey : notnull where TValue : IEquatable<TValue>
        {
            if (dict1.Count != dict2.Count) return false;
            foreach (var kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out var value) || !value.Equals(kvp.Value))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<T> DBValueToYamlList<dbT, T>(Dictionary<dbT, T> enumMap, ushort dbValue) where T : Enum where dbT : Enum
        {
            List<T> yamlList = [.. Helpers.BitsToEnumList<dbT>(dbValue).Select(x => enumMap.TryGetValue(x, out var value) ? value : (T)Enum.Parse(typeof(T), 0.ToString())).Distinct()];

            if (Enum.IsDefined(typeof(T), "Zero") && Enum.TryParse(typeof(T), "Zero", out var zeroEnum))
            {
                yamlList.RemoveAll(x => x.Equals((T)zeroEnum));
            }
                
            return yamlList;
        }

        public static uint YamlListToDBValue<T, dbT>(Dictionary<T, dbT> enumMap, IEnumerable<T> yamlList) where T : Enum where dbT : Enum
        {
            return (uint)yamlList.Select(x => enumMap.TryGetValue(x, out var dbValue) ? Convert.ToInt32(dbValue) : 0).Aggregate(0, (total, next) => total | 1 << (next - 1));
        }
    }
}
