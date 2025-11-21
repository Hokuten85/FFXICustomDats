using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XiDatEntities;

public partial class ItemWeapon
{
    public ushort ItemId { get; set; }
    public uint Damage { get; set; }
    public int Delay { get; set; }
    public int Dps { get; set; }
    public uint SkillType { get; set; }
    public int JugSize { get; set; }
    public uint Unknown1 { get; set; }

}
