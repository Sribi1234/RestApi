using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class statement_wide
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

    public int statement_no { get; set; }

    public DateTime statement_dt { get; set; }

    public DateTime due_dt { get; set; }

    public decimal statement_amt { get; set; }

    public string statement_code { get; set; } = null!;
}
