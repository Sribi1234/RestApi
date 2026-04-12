using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class payment
{
    public int payment_no { get; set; }

    public int member_no { get; set; }

    public DateTime payment_dt { get; set; }

    public decimal payment_amt { get; set; }

    public int? statement_no { get; set; }

    public string payment_code { get; set; } = null!;

    public virtual member member_noNavigation { get; set; } = null!;
}
