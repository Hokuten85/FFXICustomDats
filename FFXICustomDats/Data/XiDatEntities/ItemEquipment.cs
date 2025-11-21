using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XiDatEntities;

public partial class ItemEquipment
{
    public ushort ItemId { get; set; }
    public byte Level { get; set; }
    public ushort Slot { get; set; }
    public ushort Races { get; set; }
    public uint Jobs { get; set; }
    public byte SuperiorLevel { get; set; }
    public byte ShieldSize { get; set; }
    public byte MaxCharges { get; set; }
    public byte CastingTime { get; set; }
    public byte UseDelay { get; set; }
    public uint ReuseDelay { get; set; }
    public byte ILevel { get; set; }
    public uint Unknown1 { get; set; }
    public uint Unknown2 { get; set; }
    public uint Unknown3 { get; set; }
}
