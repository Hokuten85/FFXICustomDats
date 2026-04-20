using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class MobSpawnSlot
{
    public short Zoneid { get; set; }

    public sbyte Spawnslotid { get; set; }

    public sbyte? Chance { get; set; }
}
