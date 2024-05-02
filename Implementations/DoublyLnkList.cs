using System.Collections;

namespace Implementations;

public class DoublyLnkList<T> : ILnkList<T>
{
    public static DoublyLnkList<T> From(params T[] values)
    {
        var list = new DoublyLnkList<T>();

        foreach (var value in values)
            list.Append(value);

        return list;
    }

    private DoublyLnkNode? _head;
    private DoublyLnkNode? _last;

    public int Count { get; private set; }

    public DoublyLnkList()
    {
        _head = null;
        _last = null;
    }

    public void Prepend(T value)
    {
        if (_head == null)
        {
            _head = new DoublyLnkNode(value);
            _last = _head;
            Count++;
            return;
        }

        _head = new DoublyLnkNode(value, _head);

        Count++;
    }

    // O(1)
    public void Append(T value)
    {
        if (_head == null)
        {
            _head = new DoublyLnkNode(value);
            _last = _head;
            Count++;
            return;
        }

        _last = new DoublyLnkNode(value, next: null, previous: _last);
        Count++;
    }

    public T this[int index] => Get(index);

    public T Get(int index)
    {
        // Ω(1)
        if (_head == null)
            throw new IndexOutOfRangeException();

        // Ω(1)
        if (index < 0)
            throw new IndexOutOfRangeException();

        // O(1)
        if (index == Count - 1 && _last != null)
            return _last.Value;

        var currentIndex = 0;

        foreach (var value in this)
        {
            if (currentIndex == index)
                return value;
            currentIndex++;
        }

        throw new IndexOutOfRangeException();
    }

    public IEnumerable<T> ToReversedEnumerable()
    {
        var result = new List<T>();
        var current = _last;
        while (current != null)
        {
            result.Add(current.Value);
            current = current.Previous;
        }

        return result;
    }

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

            if (_head != null)
                _head.Previous = null;

            Count--;
            return true;
        }

        if (index == Count - 1 && _last != null)
        {
            _last = _last.Previous;
            
            if (_last != null)
                _last.Next = null;

            return true;
        }

        var currentIndex = 0;
        var current = _head;

        // Ω(1)
        // O(n - 1)
        while (current != null)
        {
            if (currentIndex == index)
            {
                if (current.Next != null)
                    current.Next.Previous = current.Previous;

                if (current.Previous != null)
                    current.Previous.Next = current.Next;

                Count--;
                return true;
            }

            currentIndex++;
            current = current.Next;
        }

        return false;
    }

    public bool Remove(T value)
    {
        if (_head == null)
            return false;

        if (_head.ValueEquals(value))
        {
            _head = _head.Next;

            if (_head != null)
                _head.Previous = null;

            Count--;
            return true;
        }
        
        if (_last != null && _last.ValueEquals(value))
        {
            _last = _last.Previous;
            
            if (_last != null)
                _last.Next = null;

            return true;
        }

        var current = _head;
        while (current != null)
        {
            if (current.ValueEquals(value))
            {
                if (current.Previous != null)
                    current.Previous.Next = current.Next;

                if (current.Next != null)
                    current.Next.Previous = current.Previous;

                Count--;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator() =>
        new NodeEnumerator(_head);

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public T Last()
    {
        if (_last == null)
            throw new InvalidOperationException();

        return _last.Value;
    }

    private class DoublyLnkNode
    {
        public T Value { get; }
        public DoublyLnkNode? Next { get; set; }
        public DoublyLnkNode? Previous { get; set; }

        public DoublyLnkNode(T value,
            DoublyLnkNode? next = null,
            DoublyLnkNode? previous = null)
        {
            Value = value;
            Next = next;
            Previous = previous;

            if (next != null)
                next.Previous = this;

            if (previous != null)
                previous.Next = this;
        }

        public bool NextValueEquals(T value) =>
            Next != null && Next.ValueEquals(value);

        public bool ValueEquals(T value) =>
            Value != null && Value.Equals(value);

        public void AddAfter(T value) =>
            Next = new DoublyLnkNode(value, next: null, previous: this);
    }

    private class NodeEnumerator : IEnumerator<T>
    {
        private readonly DoublyLnkNode _head;
        private DoublyLnkNode _currentNode;

        public NodeEnumerator(DoublyLnkNode node)
        {
            _head = node;
            _currentNode = new DoublyLnkNode(default, node);
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
            _currentNode = new DoublyLnkNode(default, _head);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}