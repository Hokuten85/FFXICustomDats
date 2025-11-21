using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class AuditDbox
{
    public uint Id { get; set; }

    public ushort Itemid { get; set; }

    public uint Quantity { get; set; }

    public uint Sender { get; set; }

    public string? SenderName { get; set; }

    public uint Receiver { get; set; }

    public string? ReceiverName { get; set; }

    public uint Price { get; set; }

    public uint Date { get; set; }
}
