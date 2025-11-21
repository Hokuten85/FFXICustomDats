using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class AuditBazaar
{
    public uint Id { get; set; }

    public ushort Itemid { get; set; }

    public uint Quantity { get; set; }

    public uint Seller { get; set; }

    public string? SellerName { get; set; }

    public uint Purchaser { get; set; }

    public string? PurchaserName { get; set; }

    public uint Price { get; set; }

    public uint Date { get; set; }
}
