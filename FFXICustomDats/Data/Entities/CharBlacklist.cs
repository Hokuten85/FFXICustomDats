using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class CharBlacklist
{
    public uint CharidOwner { get; set; }

    public uint CharidTarget { get; set; }
}
