using System;
using Lab4;

class Lab4_Test
{
    static void Main()
    {
        var q = new BiDirectionalPriorityQueue();

        q.Enqueue(10, 3);
        q.Enqueue(20, 1);
        q.Enqueue(30, 5);
        q.Enqueue(40, 2);
        q.Enqueue(50, 4);

        Console.WriteLine("peek:");
        Console.WriteLine($"Highest: {q.Peek(DequeueMode.Highest).Value} (expected 30)");
        Console.WriteLine($"Lowest:  {q.Peek(DequeueMode.Lowest).Value} (expected 20)");
        Console.WriteLine($"Oldest:  {q.Peek(DequeueMode.Oldest).Value} (expected 10)");
        Console.WriteLine($"Newest:  {q.Peek(DequeueMode.Newest).Value} (expected 50)");

        Console.WriteLine("\ndequeue:");
        Console.WriteLine($"Highest: {q.Dequeue(DequeueMode.Highest).Value} (expected 30)");
        Console.WriteLine($"Lowest:  {q.Dequeue(DequeueMode.Lowest).Value} (expected 20)");
        Console.WriteLine($"Oldest:  {q.Dequeue(DequeueMode.Oldest).Value} (expected 10)");
        Console.WriteLine($"Newest:  {q.Dequeue(DequeueMode.Newest).Value} (expected 50)");

        Console.WriteLine("\nleft in queue:");
        Console.WriteLine($"Value: {q.Peek(DequeueMode.Highest).Value} (expected 40)");
    }
}