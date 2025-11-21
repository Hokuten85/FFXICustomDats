using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XiDatEntities;

public partial class ItemUsable
{
    public ushort ItemId { get; set; }
    public byte ActivationTime { get; set; }
    public uint Unknown1 { get; set; }
    public uint Unknown2 { get; set; }
    public uint Unknown3 { get; set; }

}
