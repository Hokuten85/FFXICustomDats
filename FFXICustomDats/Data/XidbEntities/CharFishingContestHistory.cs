using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class CharFishingContestHistory
{
    public uint Charid { get; set; }

    public ushort ContestRank1 { get; set; }

    public ushort ContestRank2 { get; set; }

    public ushort ContestRank3 { get; set; }

    public ushort ContestRank4 { get; set; }
}
