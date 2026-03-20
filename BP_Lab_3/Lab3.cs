using System;
using System.Collections.Generic;
using System.Linq;
public enum EvictionPolicy
{
    LRU,
    LFU,
    TTL,
    Custom,
}
public class Lab3<TKey, TValue> where TKey : notnull
{
    Dictionary<TKey, (TValue result, DateTime LastUsed, int hits)> cache = new Dictionary<TKey, (TValue result, DateTime LastUsed, int hits)>();
    Func<IEnumerable<Tkey>, Tkey>? _customEviction;
    Func<TKey, TValue>_fn;
    EvictionPolicy _policy;
    public Lab3
        (
        Func<TKey, TValue>fn, 
        EvictionPolicy policy,
        Func<IEnumerable<TKey>, TKey>? customEviction
        )
    {
        _policy = policy;
        _fn = fn;
        _customEviction = customEviction;
    }

    public TValue Get(TKey n)
    {
        if (cache.ContainsKey(n))
        {
            if(_policy == EvictionPolicy.TTL)
            {
                var age = DateTime.Now - cache[n].LastUsed;
                if(age.TotalSeconds >= 5)
                {
                    cache.Remove(n);
                    Console.WriteLine($"{n} removed from the cache due to expiration");
                }
            }
            if(cache.ContainsKey(n))
            {
                Console.WriteLine($"taking {n} from cache");
                cache[n] = (cache[n].result, DateTime.Now, cache[n].hits ++);
                return cache[n].result; 
            }

        }

        Console.WriteLine($"calculating {fn}");
        TValue result = _fn(n);

        if (cache.Count >= 3)
        {
            TKey keyToRemove;
            if(_policy == EvictionPolicy.LRU)
            {
                keyToRemove = cache.OrderBy(x => x.Value.LastUsed). First().Key;
            } 
            else if(_policy == EvictionPolicy.LFU)
            {
                keyToRemove = cache.OrderBy(x => x.Value.hits). First().Key;
            } 
            else if (_policy == EvictionPolicy.Custom)
            {
                keyToRemove = _customEviction!(cache.Keys);
            } else
            {
                keyToRemove = cache.Keys.First();
            }
            cache.Remove(keyToRemove);
            Console.WriteLine($"{n} was removed from the cache");
        }

        cache[n] = (result, DateTime.Now, 0);
        return result;
    }
}