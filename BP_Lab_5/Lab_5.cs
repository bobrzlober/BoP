using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

var numbers = new List<int> { 1, 2, 3, 4, 5 };

async Task<int> SlowDouble(int x)
{
    await Task.Delay(500);
    return x * 2;
}

var sw = System.Diagnostics.Stopwatch.StartNew();

var sequenced = await MapFunctions.MapAsync(numbers, SlowDouble);
Console.WriteLine($"Sequenced: {sw.ElapsedMilliseconds} ms");

sw.Restart();

var parallel = await MapFunctions.MapAsyncParallel(numbers, SlowDouble);
Console.WriteLine($"Parallel: {sw.ElapsedMilliseconds} ms");

sw.Restart();

MapFunctions.MapAsyncCallback(
    numbers,
    SlowDouble,
    result => Console.WriteLine($"Ready: {string.Join(",", result)}"),
    ex => Console.WriteLine($"Error: {ex.Message}"));

Console.WriteLine("this line should appear before the result");
await Task.Delay(5000);

var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromMilliseconds(1200));

try
{
    var cancelled = await MapFunctions.MapAsyncCancellable(numbers, SlowDouble, cts.Token);
    Console.WriteLine($"Cancellation result: {string.Join(", ", cancelled)}");
}
catch (OperationCanceledException)
{
    Console.WriteLine("Canceled");
}