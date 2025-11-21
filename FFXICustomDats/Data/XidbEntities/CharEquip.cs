using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class CharEquip
{
    public uint Charid { get; set; }

    public byte Slotid { get; set; }

    public byte Equipslotid { get; set; }

    public byte Containerid { get; set; }
}
