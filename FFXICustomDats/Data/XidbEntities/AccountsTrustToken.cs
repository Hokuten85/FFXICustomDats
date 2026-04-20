using System;
using System.Collections.Generic;

namespace FFXICustomDats.Data.XidbEntities;

public partial class AccountsTrustToken
{
    public string Token { get; set; } = null!;

    public uint Accid { get; set; }

    public DateTime Expires { get; set; }

    public virtual Account Acc { get; set; } = null!;
}
