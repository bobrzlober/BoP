using System;
using System.Collections.Generic;

static List<TResult> Map<T, TResult>(
    IEnumerable<T> source, Func<T, TResult> transform)
{
    var result = new List<TResult>();
    foreach(var item in source)
    {
        result.Add(transform(item));
    }
    return result;
}
var numbers = new List<int> {1,2,3,4,5};
var doubled = Map(numbers, x=> x*2);
Console.WriteLine(string.Join(", ", doubled));
var asString = Map(numbers, x => $"num_{x}");
Console.WriteLine(string.Join(", ", asString));
