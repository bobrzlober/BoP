using System.Collections.Generic;

namespace FirstLabLib;

public static class RoundRobinGenerator
{
    public static IEnumerable<string> Generate(List<string> items)
    {
        int index = 0;
        while (true)
        {
            yield return items[index];
            index = (index + 1) % items.Count;
        }
    }
}