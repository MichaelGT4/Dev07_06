using System.Collections;

namespace Implementations;

public class LnkList<T> : ILnkList<T>, IEnumerable<T>
{
    public static LnkList<T> From(params T[] values)
    {
        var list = new LnkList<T>();
        foreach (var value in values) 
            list.Append(value);
        return list;
    }
    
    private LnkNode? _head;

    public int Count { get; private set; }

    public LnkList()
    {
        _head = null;
        Count = 0;
    }

    // O(1)
    public void Prepend(T value)
    {
        if (_head == null)
        {
            _head = new LnkNode(value);
            Count++;
            return;
        }

        _head = new LnkNode(value, _head);

        Count++;
    }


    // O(n)

    public void Append(T value)
    {
        if (_head == null)
        {
            _head = new LnkNode(value);
            Count++;
            return;
        }

        var current = _head;
        while (current.Next != null) 
            current = current.Next;
        
        current.Next = new LnkNode(value);
        Count++;
    }

    public T this[int index] => Get(index);

    // O(n)
    public T Get(int index)
    {
        // Ω(1)
        if (_head == null)
            throw new IndexOutOfRangeException();
        
        // Ω(1)
        if (index < 0)
            throw new IndexOutOfRangeException();
        
        var currentIndex = 0;
        var current = _head;
        while (current != null)
        {
            if (currentIndex == index)
                return current.Value;
            
            currentIndex++;
            current = current.Next;
        }

        throw new IndexOutOfRangeException();
    }
    
    // O(n)
    public bool RemoveAt(int index)
    {
        // Ω(1)
        if (index < 0)
            return false;
        
        // Ω(1)
        if (_head == null)
            return false;
        
        // Ω(1)
        if (index == 0)
        {
            _head = _head.Next;
            Count--;
            return true;
        }

        var currentIndex = 0;
        var current = _head;
        
        // Ω(1)
        // O(n - 1)
        while (current.Next != null)
        {
            if (currentIndex == index - 1)
            {
                var nextNode = current.Next;
                current.Next = nextNode.Next;
                Count--;
                return true;
            }

            currentIndex++;
            current = current.Next;
        }
        
        return false;
    }
    
    // O(n)
    public bool Remove(T value)
    {
        if (_head == null)
            return false;
        
        if (_head.ValueEquals(value))
        {
            _head = _head.Next;
            Count--;
            return true;
        }

        var current = _head;
        while (current.Next != null)
        {
            if (current.NextValueEquals(value))
            {
                current.Next = current.Next.Next;
                Count--;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator() => 
        new NodeEnumerator(_head);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class LnkNode
    {
        public T Value { get; }
        public LnkNode? Next { get; set; }

        public LnkNode(T value, LnkNode? next = null)
        {
            Value = value;
            Next = next;
        }

        public bool NextValueEquals(T value) => 
            Next != null && Next.ValueEquals(value);

        public bool ValueEquals(T value) => 
            Value != null && Value.Equals(value);
    }
    
    private class NodeEnumerator : IEnumerator<T>
    {
        private readonly LnkNode _head;
        private LnkNode _currentNode;

        public NodeEnumerator(LnkNode node)
        {
            _head = node;
            _currentNode = new LnkNode(default, node);
        }
 
        public T Current => _currentNode.Value;

        public bool MoveNext()
        {
            if (_currentNode.Next == null)
                return false;
            _currentNode = _currentNode.Next;
            return true;
        }

        public void Reset() => 
            _currentNode = new LnkNode(default, _head);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}