using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class category
{
    public int category_no { get; set; }

    public string category_desc { get; set; } = null!;

    public string category_code { get; set; } = null!;

    public virtual ICollection<charge> charges { get; set; } = new List<charge>();
}
