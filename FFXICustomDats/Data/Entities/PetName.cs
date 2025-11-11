using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class PetName
{
    public ushort Id { get; set; }

    public string Name { get; set; } = null!;
}
