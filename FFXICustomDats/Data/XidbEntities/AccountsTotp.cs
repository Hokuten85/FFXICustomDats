using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class AccountsTotp
{
    public uint Accid { get; set; }

    public string Secret { get; set; } = null!;

    public string RecoveryCode { get; set; } = null!;

    public bool Validated { get; set; }
}
