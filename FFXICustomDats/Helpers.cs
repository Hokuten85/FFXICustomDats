using FFXICustomDats.YamlModels.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXICustomDats
{
    public static class Helpers
    {
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
    }
}
