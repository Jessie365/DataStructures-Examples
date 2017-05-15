using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private class ListNode
    {
        public T Value { get; private set; }
        public ListNode PrevNode { get; set; }
        public ListNode NextNode { get; set; }
        public ListNode(T value)
        {
            this.Value = value;
        }
    }

    private ListNode head;
    private ListNode tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        ListNode newNode = new ListNode(element);
        if (Count != 0)
        {
            head.PrevNode = newNode;
            newNode.NextNode = head;
        }
        else
        {
            tail = newNode;
        }        
        head = newNode;
        Count++;
    }

    public void AddLast(T element)
    {
        ListNode newNode = new ListNode(element);
        if (Count != 0)
        {
            tail.NextNode = newNode;
            newNode.PrevNode = tail;
        }
        else
        {
            head = newNode;
        }
        tail = newNode;
        Count++;
    }

    public T RemoveFirst()
    {
        T returnValue;
        if (Count == 0)
        {
            throw new InvalidOperationException("List is empty");
        }
        else if (Count == 1)
        {
            returnValue = head.Value;
            head = tail = null;
        }
        else
        {
            returnValue = head.Value;
            head = head.NextNode;
            head.PrevNode = null;
        }
        Count--;
        return returnValue;
    }

    public T RemoveLast()
    {
        T returnValue;
        if (Count == 0)
        {
            throw new InvalidOperationException("List is empty");
        }
        else if (Count == 1)
        {
            returnValue = tail.Value;
            tail = head = null;
        }
        else
        {
            returnValue = tail.Value;
            tail = tail.PrevNode;
            tail.NextNode = null;
        }
        Count--;
        return returnValue;
    }

    public void ForEach(Action<T> action)
    {
        var currNode = head;
        while (currNode != null)
        {
            action.Invoke(currNode.Value);
            currNode = currNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currNode = head;
        while (currNode != null)
        {
            yield return currNode.Value;
            currNode = currNode.NextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        int i = 0;
        T[] arr = new T[Count];
        var currNode = head;
        while (currNode != null)
        {
            arr[i++] = currNode.Value;
            currNode = currNode.NextNode;
        }
        return arr;
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
