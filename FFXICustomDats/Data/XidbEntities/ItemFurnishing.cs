using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class ItemFurnishing
{
    public ushort ItemId { get; set; }

    public string Name { get; set; } = null!;

    public byte Storage { get; set; }

    public ushort Moghancement { get; set; }

    public byte Element { get; set; }

    public byte Aura { get; set; }

    public byte Placement { get; set; }

    public byte SizeX { get; set; }

    public byte SizeY { get; set; }

    public ushort Height { get; set; }
}
