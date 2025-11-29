using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class ItemPuppet
{
    public ushort ItemId { get; set; }

    public string Name { get; set; } = null!;

    public byte Slot { get; set; }

    public uint Element { get; set; }
}
