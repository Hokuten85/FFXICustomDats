using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace FFXICustomDats.Data.XiDatEntities;

public partial class ItemPuppet
{
    public ushort ItemId { get; set; }
    public byte Slot { get; set; }
    public int Fire { get; set; }
    public int Ice { get; set; }
    public int Wind { get; set; }
    public int Earth { get; set; }
    public int Lightning { get; set; }
    public int Water { get; set; }
    public int Light { get; set; }
    public int Dark { get; set; }
    public uint Unknown1 { get; set; }

}
