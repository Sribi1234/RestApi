using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class region
{
    public int region_no { get; set; }

    public string region_name { get; set; } = null!;

    public string street { get; set; } = null!;

    public string city { get; set; } = null!;

    public string state_prov { get; set; } = null!;

    public string country { get; set; } = null!;

    public string mail_code { get; set; } = null!;

    public string phone_no { get; set; } = null!;

    public string region_code { get; set; } = null!;

    public virtual ICollection<corporation> corporations { get; set; } = new List<corporation>();

    public virtual ICollection<member> members { get; set; } = new List<member>();

    public virtual ICollection<provider> providers { get; set; } = new List<provider>();
}
