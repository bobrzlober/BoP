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

    public void Enqueue(double value, int priority)
    {
        Node newNode = new Node();
        newNode.Value = value;
        newNode.Priority = priority;
        newNode.InsertionOrder = insertionCount;
        insertionCount++;
        newNode.Next = null;
        newNode.Prev = null;
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    private void RemoveNode(Node target)
    {
        if (target == head)
        {
            head = target.Next;
            if (head != null)
                head.Prev = null;
            else
                tail = null;
        }
        else if (target == tail)
        {
            tail = target.Prev;
            tail.Next = null;
        }
        else
        {
            target.Prev.Next = target.Next;
            target.Next.Prev = target.Prev;
        }
    }
}