using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class BlueSpellMod
{
    public ushort SpellId { get; set; }

    public ushort Modid { get; set; }

    public short Value { get; set; }
}
