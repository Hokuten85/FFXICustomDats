using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class Zoneline
{
    public uint Zonelineid { get; set; }

    public ushort FromZone { get; set; }

    public float FromPosX { get; set; }

    public float FromPosY { get; set; }

    public float FromPosZ { get; set; }

    public ushort ToZone { get; set; }

    public float ToPosX { get; set; }

    public float ToPosY { get; set; }

    public float ToPosZ { get; set; }

    public float ToScaleX { get; set; }

    public float ToScaleZ { get; set; }

    public float ToRotation { get; set; }
}
