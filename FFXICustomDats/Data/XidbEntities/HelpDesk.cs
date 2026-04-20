using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class HelpDesk
{
    public uint Id { get; set; }

    public uint Charid { get; set; }

    public string Message { get; set; } = null!;

    public string? Response { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? RespondedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
