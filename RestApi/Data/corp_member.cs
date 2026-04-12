using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class corp_member
{
    public int member_no { get; set; }

    public string lastname { get; set; } = null!;

    public string firstname { get; set; } = null!;

    public string? middleinitial { get; set; }

    public int corp_no { get; set; }

    public string corp_name { get; set; } = null!;

    public string street { get; set; } = null!;

    public string city { get; set; } = null!;

    public string state_prov { get; set; } = null!;

    public string mail_code { get; set; } = null!;

    public string phone_no { get; set; } = null!;

    public DateTime expr_dt { get; set; }

    public int region_no { get; set; }

    public string corp_code { get; set; } = null!;
}
