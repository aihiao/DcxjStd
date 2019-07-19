using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class CommonUtility
{
    public static bool IsNull(this object obj)
    {
        return obj == null;
    }

    public static bool NotNull(this object obj)
    {
        return obj != null;
    }
}
