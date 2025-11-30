using static FFXICustomDats.YamlModels.Items.ItemAttributes.FlagHelpers;

namespace FFXICustomDats.YamlModels.Items.ItemAttributes
{
    public enum Job {
        Zero = 0,
        WAR, MNK, WHM, BLM, RDM, THF, PLD, DRK, BST, BRD, RNG, SAM, NIN, DRG, SMN, BLU, COR, PUP, DNC, SCH, GEO, RUN,
        All = 0x01 | 0x02 | 0x04 | 0x08 | 0x10 | 0x20 | 0x40 | 0x80 | 0x100 | 0x200 | 0x400 | 0x800 | 0x1000 | 0x2000 | 0x4000 | 0x8000 | 0x10000 | 0x20000 | 0x40000 | 0x80000 | 0x100000 | 0x200000
    };

    public static class JobHelpers
    {
        public enum JOBTYPE
        {
            JOB_NON = 0,
            JOB_WAR,
            JOB_MNK,
            JOB_WHM,
            JOB_BLM,
            JOB_RDM,
            JOB_THF,
            JOB_PLD,
            JOB_DRK,
            JOB_BST,
            JOB_BRD,
            JOB_RNG,
            JOB_SAM,
            JOB_NIN,
            JOB_DRG,
            JOB_SMN,
            JOB_BLU,
            JOB_COR,
            JOB_PUP,
            JOB_DNC,
            JOB_SCH,
            JOB_GEO,
            JOB_RUN,
            JOB_MON,
            JOB_ALL = 0x01 | 0x02 | 0x04 | 0x08 | 0x10 | 0x20 | 0x40 | 0x80 | 0x100 | 0x200 | 0x400 | 0x800 | 0x1000 | 0x2000 | 0x4000 | 0x8000 | 0x10000 | 0x20000 | 0x40000 | 0x80000 | 0x100000 | 0x200000
        };

        public readonly static Dictionary<JOBTYPE, Job> JobMap = new()
        {
            { JOBTYPE.JOB_WAR, Job.WAR },
            { JOBTYPE.JOB_MNK, Job.MNK },
            { JOBTYPE.JOB_WHM, Job.WHM },
            { JOBTYPE.JOB_BLM, Job.BLM },
            { JOBTYPE.JOB_RDM, Job.RDM },
            { JOBTYPE.JOB_THF, Job.THF },
            { JOBTYPE.JOB_PLD, Job.PLD },
            { JOBTYPE.JOB_DRK, Job.DRK },
            { JOBTYPE.JOB_BST, Job.BST },
            { JOBTYPE.JOB_BRD, Job.BRD },
            { JOBTYPE.JOB_RNG, Job.RNG },
            { JOBTYPE.JOB_SAM, Job.SAM },
            { JOBTYPE.JOB_NIN, Job.NIN },
            { JOBTYPE.JOB_DRG, Job.DRG },
            { JOBTYPE.JOB_SMN, Job.SMN },
            { JOBTYPE.JOB_BLU, Job.BLU },
            { JOBTYPE.JOB_COR, Job.COR },
            { JOBTYPE.JOB_PUP, Job.PUP },
            { JOBTYPE.JOB_DNC, Job.DNC },
            { JOBTYPE.JOB_SCH, Job.SCH },
            { JOBTYPE.JOB_GEO, Job.GEO },
            { JOBTYPE.JOB_RUN, Job.RUN },
            { JOBTYPE.JOB_ALL, Job.All },
        };

        public static Dictionary<Job, JOBTYPE> ReverseJobMap()
        {
            return JobMap.ToDictionary(x => x.Value, y => y.Key);
        }

        public static bool IsEqual(List<Job> jobList, uint dbJobs)
        {
            var dbList = DBValueToYamlList(dbJobs);
            return Helpers.AreEqual(jobList, dbList);
        }

        public static List<JOBTYPE> JobTypeBitsToEnumList(uint bits)
        {
            var enumList = new List<JOBTYPE>();
            if (bits == (uint)JOBTYPE.JOB_ALL)
            {
                enumList.Add(JOBTYPE.JOB_ALL);
            }
            else
            {
                foreach (var value in Enum.GetValues<JOBTYPE>().Where(x => !x.Equals(JOBTYPE.JOB_ALL)))
                {
                    if ((bits & 1 << (int)value - 1) > 0)
                    {
                        enumList.Add(value);
                    }
                }
            }

            return enumList;
        }

        public static List<Job> DBValueToYamlList(uint dbValue)
        {
            return [.. JobTypeBitsToEnumList(dbValue).Select(x => JobMap.TryGetValue(x, out var value) ? value : Job.Zero).Distinct()];
        }

        public static uint YamlListToDBValue(List<Job> jobList)
        {
            if (jobList.Contains(Job.All))
            {
                return (uint)JOBTYPE.JOB_ALL;
            }
            return Helpers.YamlListToDBValue(ReverseJobMap(), jobList.Where(x => x != Job.All));
        }
    }
}
