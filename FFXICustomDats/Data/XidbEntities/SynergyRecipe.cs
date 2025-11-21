using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class SynergyRecipe
{
    public uint Id { get; set; }

    public byte PrimarySkill { get; set; }

    public byte PrimaryRank { get; set; }

    public byte SecondarySkill { get; set; }

    public byte SecondaryRank { get; set; }

    public byte TertiarySkill { get; set; }

    public byte TertiaryRank { get; set; }

    public ushort CostFireFewell { get; set; }

    public ushort CostIceFewell { get; set; }

    public ushort CostWindFewell { get; set; }

    public ushort CostEarthFewell { get; set; }

    public ushort CostLightningFewell { get; set; }

    public ushort CostWaterFewell { get; set; }

    public ushort CostLightFewell { get; set; }

    public ushort CostDarkFewell { get; set; }

    public ushort Ingredient1 { get; set; }

    public ushort Ingredient2 { get; set; }

    public ushort Ingredient3 { get; set; }

    public ushort Ingredient4 { get; set; }

    public ushort Ingredient5 { get; set; }

    public ushort Ingredient6 { get; set; }

    public ushort Ingredient7 { get; set; }

    public ushort Ingredient8 { get; set; }

    public ushort Result { get; set; }

    public ushort ResultHq1 { get; set; }

    public ushort ResultHq2 { get; set; }

    public ushort ResultHq3 { get; set; }

    public byte ResultQty { get; set; }

    public byte ResultHq1qty { get; set; }

    public byte ResultHq2qty { get; set; }

    public byte ResultHq3qty { get; set; }

    public string ResultName { get; set; } = null!;
}
