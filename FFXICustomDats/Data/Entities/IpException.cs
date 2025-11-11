using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class IpException
{
    public uint Accid { get; set; }

    public DateTime Exception { get; set; }

    public string? Comment { get; set; }
}
