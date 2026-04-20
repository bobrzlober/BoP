using System;
using System.Collections.Generic;
using System.Threading.Tasks;
static async Task<List<TResult>> MapAsync <T, TResult>(
    IEnumerable<T> source,
    Func<T, Task<TResult>> transform)
{
    var result = new List<TResult>();
    foreach(var item in source)
    {
        TResult transformed = await transform(item);
        result.Add(transformed); 
    }
    return result;
}

static async Task<List<TResult>> MapAsyncParallel<T, TResult>(
    IEnumerable<T> source,
    Func<T, Task<TResult>> transform)
{   
    List<Task<TResult>> tasks = new List<Task<TResult>>();
        foreach(var item in source)
    {
        tasks.Add(transform(item));
    }
    TResult[] results = await Task.WhenAll(tasks);
    return new List<TResult>(results); 
}
static void MapAsyncCallback<T, TResult>(
    IEnumerable<T> source,
    Func<T, Task<TResult>> transform,
    Action<List<TResult>> OnComplete,
    Action<Exception>? OnError = null)
{
    Task.Run(async () =>
    {
        try
        {
            var result = new List<TResult>();
            foreach(var item in source)
            {
                TResult transformed = await transform(item);
                result.Add(transformed);
            }
            OnComplete(result);
        }
        catch (Exception exc)
        {
            OnError?.Invoke(exc);
        }
    });
}
static async Task<List<TResult>> MapAsyncCancellable<T, TResult>(
    IEnumerable<T> source,
    Func<T, Task<TResult>> transform,
    CancellationToken cancellationToken)
{
    var result = new List<TResult>();
    foreach(var item in source)
    {
        cancellationToken.ThrowIfCancellationRequested();
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
var sw = System.Diagnostics.Stopwatch.StartNew();

var seqenced = await MapAsync(numbers, SlowDouble);
Console.WriteLine($"Seqenced: {sw.ElapsedMilliseconds} ms");

sw.Restart();

var parallel = await MapAsyncParallel(numbers, SlowDouble);
Console.WriteLine($"Parallel: {sw.ElapsedMilliseconds} ms");

sw.Restart();

MapAsyncCallback(numbers, SlowDouble, OnComplete => Console.WriteLine($"Ready: {string.Join(",", OnComplete)}"), OnError => Console.WriteLine($"Error: {OnError.Message}"));
Console.WriteLine("this line should appear before the result");
await Task.Delay(5000);

var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(2));
try
{
    var cancelled = await MapAsyncCancellable(numbers, SlowDouble, cts.Token);
    Console.WriteLine($"Cancelation token result: {string.Join(", ", cancelled)}");
}
catch (OperationCanceledException)
{
    Console.WriteLine("Canceled");
}