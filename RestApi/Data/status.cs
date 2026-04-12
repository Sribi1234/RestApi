using System;
using System.Collections.Generic;

namespace RestApi.Data;

public partial class status
{
    public string status_code { get; set; } = null!;

    public string status_desc { get; set; } = null!;
}
