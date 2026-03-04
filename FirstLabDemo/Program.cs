using System;
using System.Collections.Generic;
using FirstLabLib;
//standart usage
Console.WriteLine("example 1");
var items = new List<string> { "smth1", "smth2", "smth3", "smth4" };
var generator = RoundRobinGenerator.Generate(items);
TimeoutItecd ..rator.Run(generator, 0.1);
//example of usage: 2 element
Console.WriteLine("\nexample 2");
var twoItems = new List<string> { "smth1", "smth2" };
TimeoutIterator.Run(RoundRobinGenerator.Generate(twoItems), 0.05);
//example of usage: 1 element
Console.WriteLine("\nexample 3");
var oneItem = new List<string> { "smth1" };
TimeoutIterator.Run(RoundRobinGenerator.Generate(oneItem), 0.02);