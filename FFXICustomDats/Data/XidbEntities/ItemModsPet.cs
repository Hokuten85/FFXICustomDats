using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class ItemModsPet
{
    public ushort ItemId { get; set; }

    public ushort ModId { get; set; }

    public short Value { get; set; }

    public byte PetType { get; set; }
}
