using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class FishingContestEntry
{
    public uint Charid { get; set; }

    public byte Mjob { get; set; }

    public byte Sjob { get; set; }

    public byte Mlevel { get; set; }

    public byte Slevel { get; set; }

    public byte Race { get; set; }

    public byte Allegiance { get; set; }

    public byte Fishrank { get; set; }

    public uint Score { get; set; }

    public uint Submittime { get; set; }

    public byte Contestrank { get; set; }

    public byte Share { get; set; }

    public byte Claimed { get; set; }
}
