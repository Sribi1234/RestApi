using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class member
{
    public int member_no { get; set; }

    public string lastname { get; set; } = null!;

    public string firstname { get; set; } = null!;

    public string? middleinitial { get; set; }

    public string street { get; set; } = null!;

    public string city { get; set; } = null!;

    public string state_prov { get; set; } = null!;

    public string country { get; set; } = null!;

    public string mail_code { get; set; } = null!;

    public string? phone_no { get; set; }

    public byte[]? photograph { get; set; }

    public DateTime issue_dt { get; set; }

    public DateTime expr_dt { get; set; }

    public int region_no { get; set; }

    public int? corp_no { get; set; }

    public decimal? prev_balance { get; set; }

    public decimal? curr_balance { get; set; }

    public string member_code { get; set; } = null!;

    public virtual ICollection<charge> charges { get; set; } = new List<charge>();

    public virtual corporation? corp_noNavigation { get; set; }

    public virtual ICollection<payment> payments { get; set; } = new List<payment>();

    public virtual region region_noNavigation { get; set; } = null!;

    public virtual ICollection<statement> statements { get; set; } = new List<statement>();
}
