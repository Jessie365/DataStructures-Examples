using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private LinkedList<KeyValue<TKey, TValue>>[] slots;
    public int Count { get; private set; }
    
    public int Capacity
    {
        get
        {
            return slots.Length;
        }
    }

    public const float LoadFactor = 0.75f;

    public HashTable(int capacity = 16)
    {
        slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        Count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        // grow if needed
        GrowIfNeeded();
        var item = new KeyValue<TKey, TValue>(key, value);
        int calculatedSlotIndex = CalculateSlotNumber(key);
        // if the slot is empty
        if (slots[calculatedSlotIndex] == null)
        {
            slots[calculatedSlotIndex] = new LinkedList<KeyValue<TKey, TValue>>();
            slots[calculatedSlotIndex].AddFirst(item);
        }
        // if the slot is already occupied
        else
        {
            // iterate all the elements in the current slot
            var currentElement = slots[calculatedSlotIndex].First;
            while (currentElement != null)
            {
                // throw an exception on duplicated key
                if (item.Key.Equals(currentElement.Value.Key))
                {
                    throw new ArgumentException("Duplicate keys exists!");
                }
                currentElement = currentElement.Next;
            }
            slots[calculatedSlotIndex].AddLast(item);
        }
        Count++;
    }

    private void GrowIfNeeded()
    {
        if ((float)(Count + 1) / Capacity > 0.65f)
        {
            Grow();
        }
    }

    private void Grow()
    {
        var hashTable = new HashTable<TKey, TValue>(Capacity * 2);
        foreach (var element in this)
        {
            hashTable.Add(element.Key, element.Value);
        }
        this.slots = hashTable.slots;
        this.Count = hashTable.Count;
    }

    private int CalculateSlotNumber(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % Capacity;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        var element = Find(key);
        if (element != null)
        {
            element.Value = value;
            return false;
        }
        this.Add(key, value);
        return true;
    }

    public TValue Get(TKey key)
    {
        var element = Find(key);
        if (element == null)
        {
            throw new KeyNotFoundException($"Key: {key} not found");
        }
        return element.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return this.Get(key);
        }
        set
        {
            this.AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var element = Find(key);
        if (element != null)
        {
            value = element.Value;
            return true;
        }
        else
        {
            value = default(TValue);
            return false;
        }
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int calculatedSlotIndex = CalculateSlotNumber(key);
        if (slots[calculatedSlotIndex] != null)
        {
            foreach (var item in slots[calculatedSlotIndex])
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        var element = Find(key);
        return element != null;
    }

    public bool Remove(TKey key)
    {
        int calculatedSlotIndex = CalculateSlotNumber(key);
        if (slots[calculatedSlotIndex] != null)
        {
            foreach (var item in slots[calculatedSlotIndex])
            {
                if (item.Key.Equals(key))
                {
                    slots[calculatedSlotIndex].Remove(item);
                    Count--;
                    return true;
                }
            }
        }
        return false;
    }

    public void Clear()
    {
        slots = new LinkedList<KeyValue<TKey, TValue>>[Capacity];
        Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            return this.Select(el => el.Key);
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            return this.Select(el => el.Value);
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var slot in slots)
        {
            if (slot != null)
            {
                foreach (var item in slot)
                {
                    yield return item;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
