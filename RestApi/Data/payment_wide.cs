using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class payment_wide
{
    public int member_no { get; set; }

    public string lastname { get; set; } = null!;

    public string firstname { get; set; } = null!;

    public string? middleinitial { get; set; }

    public string street { get; set; } = null!;

    public string city { get; set; } = null!;

    public string state_prov { get; set; } = null!;

    public string mail_code { get; set; } = null!;

    public string? phone_no { get; set; }

    public DateTime expr_dt { get; set; }

    public string member_code { get; set; } = null!;

    public int region_no { get; set; }

    public string region_name { get; set; } = null!;

    public int payment_no { get; set; }

    public DateTime payment_dt { get; set; }

    public decimal payment_amt { get; set; }

    public string payment_code { get; set; } = null!;
}
