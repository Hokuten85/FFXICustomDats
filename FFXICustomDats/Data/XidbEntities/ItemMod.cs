using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class ItemMod
{
    public ushort ItemId { get; set; }

    public ushort ModId { get; set; }

    public short Value { get; set; }
}
