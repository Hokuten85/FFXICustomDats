using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class CharVar
{
    public uint Charid { get; set; }

    public string Varname { get; set; } = null!;

    public int Value { get; set; }

    public int Expiry { get; set; }
}
