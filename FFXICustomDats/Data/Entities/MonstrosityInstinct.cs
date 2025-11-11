using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class MonstrosityInstinct
{
    public ushort MonstrosityInstinctId { get; set; }

    public ushort Cost { get; set; }

    public string? Name { get; set; }
}
