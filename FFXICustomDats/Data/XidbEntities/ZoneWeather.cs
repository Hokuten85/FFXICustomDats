using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class ZoneWeather
{
    public short Zone { get; set; }

    public byte[]? Weather { get; set; }
}
