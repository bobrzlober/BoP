using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstLabLib;

public static class TimeoutIterator
{
    public static void Run(IEnumerable<string> iterator, double seconds)
    {
        var stopwatch = Stopwatch.StartNew();
        var counts = new Dictionary<string, int>();
        int total = 0;

        foreach (string value in iterator)
        {
            if (stopwatch.Elapsed.TotalSeconds >= seconds)
                break;

            if (!counts.ContainsKey(value))
                counts[value] = 0;

            counts[value]++;
            total++;

            Console.WriteLine(
                $"  [{stopwatch.Elapsed.TotalSeconds:F4}s]  " +
                $"#{total,-4}  →  {value}  (seen {counts[value]}x)"
            );
        }
    }
}