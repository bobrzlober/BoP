using System;
using System.Collections.Generic;
using System.Threading.Tasks;

static async Task<List<TResult>> MapAsync<T, TResult>(
    IEnumerable<T> source,
    Func<T, Task<TResult>> transform)
{   
    var result = new List<TResult>();
    foreach (var item in source)
    {
        TResult transformed = await transform(item);
        result.Add(transformed);
    }

    return result;
}
var numbers = new List<int> { 1, 2, 3, 4, 5 };
async Task<int> SlowDouble(int x)
{
    await Task.Delay(500);
    return x * 2;
}
var doubled = await MapAsync(numbers, SlowDouble);
Console.WriteLine(string.Join(", ", doubled));