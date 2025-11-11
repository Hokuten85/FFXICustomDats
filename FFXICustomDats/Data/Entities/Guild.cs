using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class Guild
{
    public byte Id { get; set; }

    public string PointsName { get; set; } = null!;
}
