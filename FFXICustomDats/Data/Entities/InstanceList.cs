using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class InstanceList
{
    public ushort Instanceid { get; set; }

    public string InstanceName { get; set; } = null!;

    public ushort InstanceZone { get; set; }

    public ushort EntranceZone { get; set; }

    public byte TimeLimit { get; set; }

    public float StartX { get; set; }

    public float StartY { get; set; }

    public float StartZ { get; set; }

    public byte StartRot { get; set; }

    public ushort? MusicDay { get; set; }

    public ushort? MusicNight { get; set; }

    public ushort? Battlesolo { get; set; }

    public ushort? Battlemulti { get; set; }
}
