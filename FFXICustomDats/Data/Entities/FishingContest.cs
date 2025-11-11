using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class FishingContest
{
    public byte Status { get; set; }

    public byte Criteria { get; set; }

    public byte Measure { get; set; }

    public uint Fishid { get; set; }

    public uint Starttime { get; set; }

    public uint Nextstagetime { get; set; }
}
