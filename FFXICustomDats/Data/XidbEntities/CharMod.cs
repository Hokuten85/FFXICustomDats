using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class CharMod
{
    public uint Charid { get; set; }

    public ushort Modid { get; set; }

    public short Value { get; set; }
}
