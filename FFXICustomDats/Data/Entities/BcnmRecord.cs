using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class BcnmRecord
{
    public ushort BcnmId { get; set; }

    public byte ZoneId { get; set; }

    public string Name { get; set; } = null!;

    public string? FastestName { get; set; }

    public byte FastestPartySize { get; set; }

    public uint? FastestTime { get; set; }
}
