using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class ZoneSetting
{
    public ushort Zoneid { get; set; }

    public ushort Zonetype { get; set; }

    public string Zoneip { get; set; } = null!;

    public ushort Zoneport { get; set; }

    public string Name { get; set; } = null!;

    public ushort MusicDay { get; set; }

    public ushort MusicNight { get; set; }

    public ushort Battlesolo { get; set; }

    public ushort Battlemulti { get; set; }

    public byte Restriction { get; set; }

    public float Tax { get; set; }

    public ushort Misc { get; set; }
}
