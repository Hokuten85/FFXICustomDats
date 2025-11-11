using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class AuditVendor
{
    public uint Id { get; set; }

    public ushort Itemid { get; set; }

    public uint Quantity { get; set; }

    public uint Seller { get; set; }

    public string? SellerName { get; set; }

    public uint Baseprice { get; set; }

    public uint Totalprice { get; set; }

    public uint Date { get; set; }
}
