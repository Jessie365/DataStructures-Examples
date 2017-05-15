using System;

public class CircularQueue<T>
{
    private const int InitialCapacity = 16;
    private T[] circularArray;
    private int startIndex;
    private int endIndex;
    public int Count { get; private set; }

    public CircularQueue()
    {
        circularArray = new T[InitialCapacity];
    }

    public CircularQueue(int capacity)
    {
        circularArray = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (Count >= circularArray.Length)
        {
            // enlarge the array and copy all the elements in the new one
            var tempCircularArray = new T[circularArray.Length * 2];
            var currentArray = this.ToArray();
            for (int i = 0; i < currentArray.Length; i++)
            {
                tempCircularArray[i] = currentArray[i];
            }
            tempCircularArray[currentArray.Length] = element;
            startIndex = 0;
            endIndex = currentArray.Length + 1;
            circularArray = tempCircularArray;
        }
        else
        {
            circularArray[endIndex++] = element;
            if (endIndex >= circularArray.Length)
            {
                endIndex = 0;
            }
        }
        Count++;
    }

    public T Dequeue()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Stack is empty");
        }
        var returnedValue = circularArray[startIndex];
        circularArray[startIndex] = default(T);
        if (startIndex + 1 >= circularArray.Length)
        {
            startIndex = 0;
        }
        else
        {
            startIndex++;
        }
        Count--;
        return returnedValue;
    }

    public T[] ToArray()
    {
        var result = new T[Count];
        int currentPos = startIndex;
        for (int i = 0; i < Count; i++)
        {
            result[i] = circularArray[currentPos++];
            if (currentPos >= circularArray.Length)
            {
                currentPos = 0;
            }
        }
        return result;
    }
}


class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
