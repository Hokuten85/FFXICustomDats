using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class AccountsBanned
{
    public uint Accid { get; set; }

    public DateTime Timebann { get; set; }

    public DateTime Timeunbann { get; set; }

    public string? Banncomment { get; set; }
}
