using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XiDatEntities;

public partial class ItemFurnishing
{
    public ushort ItemId { get; set; }
    public byte StorageSlots { get; set; }
    public byte Element { get; set; }
    public uint Unknown3 { get; set; }
}
