using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class FishingBaitAffinity
{
    public ushort Baitid { get; set; }

    public ushort Fishid { get; set; }

    public ushort Power { get; set; }
}
