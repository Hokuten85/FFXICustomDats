using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.Entities;

public partial class ItemDescription
{
    public ushort Itemid { get; set; }

    public string Name { get; set; } = null!;

    public string ArticleType { get; set; } = null!;

    public string SingularName { get; set; } = null!;

    public string PluralName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ItemType { get; set; } = null!;

    public int ResourceId { get; set; }

    public byte[] IconBytes { get; set; } = null!;

    public bool NoTradePc { get; set; }

    public int Unknown1 { get; set; }

    public int Unknown2 { get; set; }

    public int Unknown3 { get; set; }

    public string DatFile { get; set; } = null!;
}
