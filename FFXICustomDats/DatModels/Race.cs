using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXICustomDats.DatModels
{
    public enum Race { All, AnyFemale, AnyMale, Elvaan, ElvaanFemale, ElvaanMale, Galka, Hume, HumeFemale, HumeMale, Mithra, Tarutaru, TarutaruFemale, TarutaruMale };

    public partial class RaceConversion
    {
        private enum CharRace
        {
            HumeMale = 0x01,
            HumeFemale = 0x02,
            ElvaanMale = 0x04,
            ElvaanFemale = 0x08,
            TarutaruMale = 0x10,
            TarutaruFemale = 0x20,
            Mithra = 0x40,
            Galka = 0x80,
            AnyFemale = 0x02 | 0x08 | 0x20 | 0x40,
            AnyMale = 0x01 | 0x04 | 0x10 | 0x80,
            Elvaan = 0x04 | 0x08,
            Hume = 0x01 | 0x02,
            Tarutaru = 0x10 | 0x20,
            All = 0x01 | 0x02 | 0x04 | 0x08 | 0x10 | 0x20 | 0x40 | 0x80,
        };

        private readonly static Dictionary<CharRace, Race> RaceDict = new()
        {
            { CharRace.HumeMale, Race.HumeMale },
            { CharRace.HumeFemale, Race.HumeFemale },
            { CharRace.ElvaanMale, Race.ElvaanMale },
            { CharRace.ElvaanFemale, Race.ElvaanFemale },
            { CharRace.TarutaruMale, Race.TarutaruMale },
            { CharRace.TarutaruFemale, Race.TarutaruFemale },
            { CharRace.Mithra, Race.Mithra },
            { CharRace.Galka, Race.Galka },
            { CharRace.AnyFemale, Race.AnyFemale },
            { CharRace.AnyMale, Race.AnyMale },
            { CharRace.Elvaan, Race.Elvaan },
            { CharRace.Hume, Race.Hume },
            { CharRace.Tarutaru, Race.Tarutaru },
            { CharRace.All, Race.All },
        };

        public static List<Race> ConvertBitRacesToYaml(short races)
        {
            var raceList = new List<Race>();
            if (races == 0)
            {
                raceList.Add(Race.All);
            }
            else if (RaceDict.TryGetValue((CharRace)races, out Race yamlRace))
            {
                raceList.Add(yamlRace);
            }

            return raceList;
        }
    }  
}
