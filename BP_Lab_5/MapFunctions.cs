using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public static class MapFunctions
{
    public static async Task<List<TResult>> MapAsync<T, TResult>(
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

    public static async Task<List<TResult>> MapAsyncParallel<T, TResult>(
        IEnumerable<T> source,
        Func<T, Task<TResult>> transform)
    {
        List<Task<TResult>> tasks = new List<Task<TResult>>();
        foreach (var item in source)
        {
            tasks.Add(transform(item));
        }
        TResult[] results = await Task.WhenAll(tasks);
        return new List<TResult>(results);
    }

    public static void MapAsyncCallback<T, TResult>(
        IEnumerable<T> source,
        Func<T, Task<TResult>> transform,
        Action<List<TResult>> onComplete,
        Action<Exception>? onError = null)
    {
        Task.Run(async () =>
        {
            try
            {
                var result = new List<TResult>();
                foreach (var item in source)
                {
                    TResult transformed = await transform(item);
                    result.Add(transformed);
                }
                onComplete(result);
            }
            catch (Exception exc)
            {
                onError?.Invoke(exc);
            }
        });
    }

    public static async Task<List<TResult>> MapAsyncCancellable<T, TResult>(
        IEnumerable<T> source,
        Func<T, Task<TResult>> transform,
        CancellationToken cancellationToken)
    {
        var result = new List<TResult>();
        foreach (var item in source)
        {
            cancellationToken.ThrowIfCancellationRequested();
            TResult transformed = await transform(item);
            result.Add(transformed);
        }
        return result;
    }
}