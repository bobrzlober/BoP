using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Lab3
{
    Dictionary<int, (int result, DateTime LastUsed)> cache = new Dictionary<int, (int result, DateTime LastUsed)>();

    public int Square(int n)
    {
        if (cache.ContainsKey(n))
        {
            Console.WriteLine($"беру з кешу!");
            cache[n] = (cache[n].result, DateTime.Now);
            return cache[n].result;
        }

        Console.WriteLine($"calculating {n} * {n}");
        int result = n * n;

        if (cache.Count >= 3)
        {
            var oldest = cache.OrderBy(x => x.Value.LastUsed).First().Key;
            cache.Remove(oldest);
            Console.WriteLine($"{oldest} has been removed from cache");
        }

        cache[n] = (result, DateTime.Now);
        return result;
    }
    static void Test()
    {
        var p = new Lab3();
        
        
    }
    
}
