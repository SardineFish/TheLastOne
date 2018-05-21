using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Utility
{
    public static void ForEach<T>(this IEnumerable<T> ts, Action<T> callback)
    {
        foreach (var item in ts)
            callback(item);
    }
}