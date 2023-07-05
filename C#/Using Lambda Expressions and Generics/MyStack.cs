using System;
using System.Collections.Generic;


public class MyStack<T>
{
    private T[] array;
    public int Count { get; private set; }

    public MyStack(int capacity)
    {
        array = new T[capacity];
        this.Count = 0;
    }

    public void Push(T val)
    {
        if (Count < array.Length) array[Count++] = val; 
        else throw new InvalidOperationException("The stack is out of capacity.");
    }

    public T Pop()
    {
        if (Count > 0) return array[--Count];
        else throw new InvalidOperationException("The stack is empty.");
    }

    public T Find(Func<T,bool> criteria)
    {
        if (criteria == null) throw new ArgumentNullException("No criteria provided");

        foreach (T item in array)
        {
            if (criteria(item) == true) return item; 
        }

        return default(T);
    }

    public T[] FindAll(Func<T,bool> criteria)
    {
        if (criteria == null) throw new ArgumentNullException("No criteria provided.");

        List<T> list = new List<T>();

        foreach(T item in array)
        {
           if (criteria(item) == true) list.Add(item);
        }

        if (list.Count > 0)
        {
            return list.ToArray();
        }
        else
        {
            return null;
        }
    }

    public int RemoveAll(Func<T,bool> criteria)
    {
        if (criteria == null) throw new ArgumentNullException("No criteria provided.");

        List<T> removedElements = new List<T>();

        for (int i = Count - 1; i >= 0; i--)
        {
            T item = array[i];
            if (criteria(item))
            {
                removedElements.Add(item);
                Array.Clear(array, i, 1);
                Count--;
            }
        }
        return removedElements.Count;
    }

    public T Max()
    {
        if (Count == 0) return default(T);

        T maxV = array[0];

        for (int i = 1; i < Count; i++)
        {
            if (Comparer<T>.Default.Compare(array[i], maxV) > 0)
            {
                maxV = array[i];
            }
        }
        return maxV;
    }


    public T Min()
    {
        if (Count == 0) return default(T);

        T min = array[0];

        for (int i = 1; i < Count; i++)
        {
            if (Comparer<T>.Default.Compare(array[i], min) < 0)
            {
                min = array[i];
            }
        }
        return min;
    }

}

