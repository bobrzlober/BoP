using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static IEnumerable<string> RoundRobinGenerator(List<string> items)
    {
        int index = 0;
        while (true)
        {
            yield return items[index];
            index = (index + 1) % items.Count;
        } 
    }
    static void TimeoutIterator(IEnumerable<string> iterator, double seconds)
    {
        var stopwatch = Stopwatch.StartNew();
        var counts = new Dictionary<string, int>();
        int total = 0;
        foreach(string value in iterator)
        {
            if (stopwatch.Elapsed.TotalSeconds >= seconds)
            {
                break;
            }
            if(!counts.ContainsKey(value)) 
            {
            counts[value] = 0;
            }
            counts[value]++;
            total ++;
            Console.WriteLine($"  [{stopwatch.Elapsed.TotalSeconds:F2}s]  " + $"#{total,-4}  →  {value}  (seen {counts[value]}x)");
        }
    }
    static void Main()
    {
        var someWhat = new List<string> {"smth1", "smth2", "smth3", "smth4"};
        IEnumerable<string> generator = RoundRobinGenerator(someWhat);
        TimeoutIterator(generator, 0.1);
    }
}

