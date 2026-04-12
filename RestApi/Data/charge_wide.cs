using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class charge_wide
{
    public int member_no { get; set; }

    public string lastname { get; set; } = null!;

    public string firstname { get; set; } = null!;

    public int region_no { get; set; }

    public string region_name { get; set; } = null!;

    public string provider_name { get; set; } = null!;

    public string category_desc { get; set; } = null!;

    public int charge_no { get; set; }

    public int provider_no { get; set; }

    public int category_no { get; set; }

    public DateTime charge_dt { get; set; }

    public decimal charge_amt { get; set; }

    public string charge_code { get; set; } = null!;
}
