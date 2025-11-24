using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public enum Race
    { 
        Zero = 0,
        HumeMale = 0x01,
        HumeFemale = 0x02,
        ElvaanMale = 0x04,
        ElvaanFemale = 0x08,
        TarutaruMale = 0x10,
        TarutaruFemale = 0x20,
        Mithra = 0x40,
        Galka = 0x80,
        Hume = 0x01 | 0x02,
        Elvaan = 0x04 | 0x08,
        Tarutaru = 0x10 | 0x20,
        AnyFemale = 0x02 | 0x08 | 0x20 | 0x40,
        AnyMale = 0x01 | 0x04 | 0x10 | 0x80,
        All = 0x01 | 0x02 | 0x04 | 0x08 | 0x10 | 0x20 | 0x40 | 0x80
    };

    public partial class RaceConversion
    {
        public static List<Race> ConvertBitRacesToYaml(short races)
        {
            var raceList = new List<Race>();
            if (races == 0)
            {
                raceList.Add(Race.All);
            }
            else
            {
                raceList.Add((Race)races);
            }

            return raceList;
        }

        public static ushort ConvertYamlRaceToBit(List<Race> races)
        {
            return (ushort)races.Cast<int>().Aggregate(0, (total, next) => total | next);
        }
    }  
}
