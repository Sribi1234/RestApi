using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class statement
{
    public int statement_no { get; set; }

    public int member_no { get; set; }

    public DateTime statement_dt { get; set; }

    public DateTime due_dt { get; set; }

    public decimal statement_amt { get; set; }

    public string statement_code { get; set; } = null!;

    public virtual member member_noNavigation { get; set; } = null!;
}
