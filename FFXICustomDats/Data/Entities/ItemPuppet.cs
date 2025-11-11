using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class ItemPuppet
{
    public ushort Itemid { get; set; }

    public string Name { get; set; } = null!;

    public byte Slot { get; set; }

    public uint Element { get; set; }
}
