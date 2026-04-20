using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class MobSpawnPoint
{
    public int Mobid { get; set; }

    public sbyte Spawnslotid { get; set; }

    public string? Mobname { get; set; }

    public string? PolutilsName { get; set; }

    public uint Groupid { get; set; }

    public byte MinLevel { get; set; }

    public byte MaxLevel { get; set; }

    public float PosX { get; set; }

    public float PosY { get; set; }

    public float PosZ { get; set; }

    public byte PosRot { get; set; }
}
