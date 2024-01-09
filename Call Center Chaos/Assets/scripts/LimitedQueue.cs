using System;
using System.Collections.Generic;

public class LimitedQueue<T>
{
    private readonly Queue<T> queue = new Queue<T>();
    private readonly int maxSize;

    public LimitedQueue(int maxSize)
    {
        if (maxSize <= 0)
        {
            throw new ArgumentException("Max size must be greater than 0");
        }

        this.maxSize = maxSize;
    }

    public void Enqueue(T item)
    {
        queue.Enqueue(item);

        while (queue.Count > maxSize)
        {
            queue.Dequeue();
        }
    }
    
    public T Dequeue()
    {
        return queue.Dequeue();
    }

    public int Count
    {
        get { return queue.Count; }
    }

    public bool Contains(T _item)
    {
        return queue.Contains(_item);
    }

    // You can add other methods or properties as needed
}
