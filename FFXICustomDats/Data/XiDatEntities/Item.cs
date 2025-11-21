using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XiDatEntities;

public partial class Item
{
    public ushort ItemId { get; set; }
    public ushort Flags { get; set; }
    public byte StackSize { get; set; }
    public ushort ItemType { get; set; }
    public int ResourceId { get; set; }
    public ushort ValidTargets { get; set; }
    public string DatFile { get; set; } = null!;
    public byte[]? IconBytes { get; set; }
}
