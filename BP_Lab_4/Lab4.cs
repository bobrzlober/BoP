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
        insertionCount++;
        Node newNode = new Node();
        newNode.Value = value;
        newNode.Priority = priority;
        newNode.InsertionOrder = insertionCount;
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

    public Node Dequeue(DequeueMode mode)
    {
        if (head == null)
        {
            Console.WriteLine("the List is empty");
            return null;
        }
        else if (mode == DequeueMode.Highest)
        {
            Node best = head;
            Node current = head;
            while (current != null)
            {
                if (current.Priority > best.Priority)
                    best = current;
                current = current.Next;
            }
            RemoveNode(best);
            return best;
        }
        else if (mode == DequeueMode.Lowest)
        {
            Node lowest = head;
            Node current = head;
            while (current != null)
            {
                if (current.Priority > lowest.Priority)
                    lowest = current;
                current = current.Next;
            }
            RemoveNode(lowest);
            return lowest;
        }
        else if (mode == DequeueMode.Oldest)
        {
            Node oldest = head;
            Node current = head;
            while (current != null)
            {
                if (current.InsertionOrder < oldest.InsertionOrder)
                    oldest = current;
                current = current.Next;
            }
            RemoveNode(oldest);
            return oldest;
        }
        else if (mode == DequeueMode.Newest)
        {
            Node newest = head;
            Node current = head;
            while (current != null)
            {
                if (current.InsertionOrder > newest.InsertionOrder)
                    newest = current;
                current = current.Next;
            }
            RemoveNode(newest);
            return newest;
        }
        return null;
    }
}