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
            JOB_NON = 0x000000,
            JOB_WAR = 0x000001,
            JOB_MNK = 0x000002,
            JOB_WHM = 0x000004,
            JOB_BLM = 0x000008,
            JOB_RDM = 0x000010,
            JOB_THF = 0x000020,
            JOB_PLD = 0x000040,
            JOB_DRK = 0x000080,
            JOB_BST = 0x000100,
            JOB_BRD = 0x000200,
            JOB_RNG = 0x000400,
            JOB_SAM = 0x000800,
            JOB_NIN = 0x001000,
            JOB_DRG = 0x002000,
            JOB_SMN = 0x004000,
            JOB_BLU = 0x008000,
            JOB_COR = 0x010000,
            JOB_PUP = 0x020000,
            JOB_DNC = 0x040000,
            JOB_SCH = 0x080000,
            JOB_GEO = 0x100000,
            JOB_RUN = 0x200000,
            JOB_MON = 0x400000,
            JOB_ALL = 0x01 | 0x02 | 0x04 | 0x08 | 0x10 | 0x20 | 0x40 | 0x80 | 0x100 | 0x200 | 0x400 | 0x800 | 0x1000 | 0x2000 | 0x4000 | 0x8000 | 0x10000 | 0x20000 | 0x40000 | 0x80000 | 0x100000 | 0x200000
        };

        public readonly static Dictionary<JOBTYPE, Job> JobMap = new()
        {
            //{ JOBTYPE.JOB_NON, Job.HumeMale },                      
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
            //{ JOBTYPE.JOB_MON, Job.All },
            { JOBTYPE.JOB_ALL, Job.All },
        };

        public static bool IsEqual(List<Job> jobList, uint dbJobs)
        {
            var dbList = DBFlagsToYamlFlags(dbJobs);
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

        public static List<Job> DBFlagsToYamlFlags(uint dbValue)
        {
            return [.. JobTypeBitsToEnumList(dbValue).Select(x => JobMap.TryGetValue(x, out var value) ? value : Job.Zero).Distinct()];
        }
    }
}
