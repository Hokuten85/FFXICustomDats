using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class MobFamilyMod
{
    public ushort Familyid { get; set; }

    public ushort Modid { get; set; }

    public short Value { get; set; }

    public bool IsMobMod { get; set; }
}
