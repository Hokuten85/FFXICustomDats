using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class MobSpawnSet
{
    public short Zoneid { get; set; }

    public sbyte Spawnsetid { get; set; }

    public sbyte Maxspawns { get; set; }
}
