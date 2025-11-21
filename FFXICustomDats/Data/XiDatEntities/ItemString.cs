using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XiDatEntities;

public partial class ItemString
{
    public ushort ItemId { get; set; }
    public string Name { get; set; }
    public ushort ArticleType { get; set; }
    public string SingularName { get; set; }
    public string PluralName { get; set; }
    public string Description { get; set; }
}
