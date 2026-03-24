using System;
namespace Lab4;

class Node
{
    public double Value;
    public int Priority;
    public int InsertionOrder;
    public Node Next;
    public Node Prev;
}

public enum DequeueMode
{
    Highest,
    Lowest,
    Oldest,
    Newest,
}

class BiDirectionalPriorityQueue
{
    Node head = null;
    Node tail = null;
    int insertionCount = 0;
}