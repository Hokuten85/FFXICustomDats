using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class MobResistance
{
    public ushort ResistId { get; set; }

    public string? Name { get; set; }

    public short SlashSdt { get; set; }

    public short PierceSdt { get; set; }

    public short H2hSdt { get; set; }

    public short ImpactSdt { get; set; }

    public short MagicalSdt { get; set; }

    public short FireSdt { get; set; }

    public short IceSdt { get; set; }

    public short WindSdt { get; set; }

    public short EarthSdt { get; set; }

    public short LightningSdt { get; set; }

    public short WaterSdt { get; set; }

    public short LightSdt { get; set; }

    public short DarkSdt { get; set; }

    public short FireResRank { get; set; }

    public short IceResRank { get; set; }

    public short WindResRank { get; set; }

    public short EarthResRank { get; set; }

    public short LightningResRank { get; set; }

    public short WaterResRank { get; set; }

    public short LightResRank { get; set; }

    public short DarkResRank { get; set; }

    public short ParalyzeResRank { get; set; }

    public short BindResRank { get; set; }

    public short SilenceResRank { get; set; }

    public short SlowResRank { get; set; }

    public short PoisonResRank { get; set; }

    public short LightSleepResRank { get; set; }

    public short DarkSleepResRank { get; set; }

    public short BlindResRank { get; set; }
}
