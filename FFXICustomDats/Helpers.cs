using FFXICustomDats.YamlModels.Items;
using FFXICustomDats.YamlModels.Items.ItemAttributes;
using FFXICustomDats.YamlModels.Items.ItemTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FFXICustomDats
{
    public static class Helpers
    {
        public static FFXIItems<T> DeserializeYaml<T>(string filePath) where T : Item
        {
            FileStream fileStream = new(filePath, FileMode.Open);
            using var reader = new StreamReader(fileStream);

            var input = new StringReader(reader.ReadToEnd());
            var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();

            return deserializer.Deserialize<FFXIItems<T>>(input);
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

        public static List<Job> JobBitsToEnumList(uint bits)
        {
            var enumList = new List<Job>();
            if (bits == (uint)Job.All)
            {
                enumList.Add(Job.All);
            }
            else
            {
                foreach (var value in Enum.GetValues<Job>().Where(x => !x.Equals(Job.All)))
                {
                    if ((bits & 1 << (int)value - 1) > 0)
                    {
                        enumList.Add(value);
                    }
                }
            }

            return enumList;
        }

        public static ushort ConvertEnumListToBit<T>(List<T> enumList) where T : Enum
        {
            return (ushort)enumList.Aggregate(0, (total, next) => total | 1 << Convert.ToInt32(next) - 1);
        }

        public static ushort ConvertEnumListToBit(List<Job> jobList)
        {
            return (ushort)jobList.Aggregate(0, (total, next) =>
                total | (next == Job.All ? (int)next : 1 << (int)next - 1)
            );
        }

        public static ushort ConvertEnumListToBit(List<Race> raceList)
        {
            return (ushort)raceList.Cast<int>().Aggregate(0, (total, next) => total | next);
        }

        public static bool AreEqual<T>(List<T> list1, List<T> list2) where T : Enum
        {
            return list1.All(list2.Contains) && list1.Count == list2.Count;
        }

        public static List<T> DBFlagsToYamlFlags<dbT, T>(Dictionary<dbT, T> enumMap, ushort dbValue) where T : Enum where dbT : Enum
        {
            return [.. Helpers.BitsToEnumList<dbT>(dbValue).Select(x => enumMap.TryGetValue(x, out var value) ? value : (T)Enum.Parse(typeof(T), 0.ToString())).Distinct()];
        }
    }
}
