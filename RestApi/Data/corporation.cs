using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class corporation
{
    public int corp_no { get; set; }

    public string corp_name { get; set; } = null!;

    public string street { get; set; } = null!;

    public string city { get; set; } = null!;

    public string state_prov { get; set; } = null!;

    public string country { get; set; } = null!;

    public string mail_code { get; set; } = null!;

    public string phone_no { get; set; } = null!;

    public DateTime expr_dt { get; set; }

    public int region_no { get; set; }

    public string corp_code { get; set; } = null!;

    public virtual ICollection<member> members { get; set; } = new List<member>();

    public virtual region region_noNavigation { get; set; } = null!;
}
