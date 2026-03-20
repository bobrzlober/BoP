using System;
using System.Linq;
using Lab3;

class TestLab3
{
    static void Main()
    {
        Console.WriteLine("=== LRU ===");
        var lru = new Lab3<string, int>(
            fn: s => s.Length,
            policy: EvictionPolicy.LRU,
            customEviction: null);
        lru.Get("hi");
        lru.Get("hello");
        lru.Get("world");
        lru.Get("hi");
        lru.Get("test");

        Console.WriteLine("\n=== LFU ===");
        var lfu = new Lab3<string, int>(
            fn: s => s.Length,
            policy: EvictionPolicy.LFU,
            customEviction: null);
        lfu.Get("hi");
        lfu.Get("hello");
        lfu.Get("world");
        lfu.Get("hi");
        lfu.Get("hi");
        lfu.Get("test");

        Console.WriteLine("\n=== TTL ===");
        var ttl = new Lab3<string, int>(
            fn: s => s.Length,
            policy: EvictionPolicy.TTL,
            customEviction: null);
        ttl.Get("hi");
        ttl.Get("hi");
        System.Threading.Thread.Sleep(6000);
        ttl.Get("hi");

        Console.WriteLine("\n=== Custom ===");
        var custom = new Lab3<string, int>(
            fn: s => s.Length,
            policy: EvictionPolicy.Custom,
            customEviction: keys => keys.First());
        custom.Get("hi");
        custom.Get("hello");
        custom.Get("world");
        custom.Get("test");
    }
}