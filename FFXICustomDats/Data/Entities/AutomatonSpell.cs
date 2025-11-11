using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class AutomatonSpell
{
    public ushort Spellid { get; set; }

    public ushort Skilllevel { get; set; }

    public byte Heads { get; set; }

    public ushort Enfeeble { get; set; }

    public ushort Immunity { get; set; }

    public uint Removes { get; set; }
}
