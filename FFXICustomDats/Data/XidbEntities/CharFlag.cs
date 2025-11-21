using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class CharFlag
{
    public uint Charid { get; set; }

    public short Disconnecting { get; set; }

    public short GmModeEnabled { get; set; }

    public short GmHiddenEnabled { get; set; }

    public bool Muted { get; set; }
}
