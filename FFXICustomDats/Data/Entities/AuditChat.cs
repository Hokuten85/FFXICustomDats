using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class AuditChat
{
    public int LineId { get; set; }

    public string Speaker { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? LsName { get; set; }

    public ushort? Zoneid { get; set; }

    public sbyte? Unity { get; set; }

    public string? Recipient { get; set; }

    public byte[]? Message { get; set; }

    public DateTime Datetime { get; set; }
}
