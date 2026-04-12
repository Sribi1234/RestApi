using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class charge
{
    public int charge_no { get; set; }

    public int member_no { get; set; }

    public int provider_no { get; set; }

    public int category_no { get; set; }

    public DateTime charge_dt { get; set; }

    public decimal charge_amt { get; set; }

    public int statement_no { get; set; }

    public string charge_code { get; set; } = null!;

    public virtual category category_noNavigation { get; set; } = null!;

    public virtual member member_noNavigation { get; set; } = null!;

    public virtual provider provider_noNavigation { get; set; } = null!;
}
