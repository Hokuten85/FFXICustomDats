using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class AutomatonAbility
{
    public ushort Abilityid { get; set; }

    public string Abilityname { get; set; } = null!;

    public ushort Reqframe { get; set; }

    public ushort Skilllevel { get; set; }
}
