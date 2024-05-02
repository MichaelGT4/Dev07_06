using System.Collections;

namespace Implementations;

public class HashMap<TKey, TValue> : IEnumerable<(TKey Key, TValue Value)> where TKey : notnull
{
    private readonly int _capacity;
    private readonly Bucket[] _buckets;

    public HashMap(int capacity = 10)
    {
        _capacity = capacity;
        _buckets = new Bucket[_capacity];
        for (var i = 0; i < _buckets.Length; i++)
            _buckets[i] = new Bucket();
    }

    public TValue this[TKey key]
    {
        get => Get(key);
        set => Add(key, value);
    }

    public IEnumerable<TKey> Keys() => 
        _buckets.SelectMany(bucket => bucket.Keys());

    // Best(1) | Worst(n) | O(1)
    private TValue Get(TKey key)
    {
        var index = Hash(key);
        var bucket = _buckets[index];
        return bucket.Get(key);
    }
    
    // O(1)
    public void Add(TKey key, TValue value)
    {
        var index = Hash(key);
        _buckets[index].Add(key, value);
    }

    private int Hash(TKey key) => 
        Math.Abs(key.GetHashCode()) % _capacity;

    public bool Contains(TKey key)
    {
        var bucket = _buckets[Hash(key)];
        return bucket.Contains(key);
    }

    public bool Remove(TKey key)
    {
        var bucket = _buckets[Hash(key)];
        return bucket.Remove(key);
    }

    public IEnumerable<TValue> Values() => 
        _buckets.SelectMany(bucket => bucket.Values());

    public IEnumerator<(TKey Key, TValue Value)> GetEnumerator() => 
        AsEnumerable().GetEnumerator();

    // O(n)
    private IEnumerable<(TKey key, TValue Value)> AsEnumerable()
    {
        foreach (var bucket in _buckets)
        foreach (var pair in bucket)
            yield return pair;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class Bucket : IEnumerable<(TKey Key, TValue Value)>
    {
        private readonly LinkedList<(TKey Key, TValue Value)> _pairs;

        public Bucket() => 
            _pairs = new LinkedList<(TKey, TValue)>();

        public TValue Get(TKey key) =>
            // O(n)
            _pairs.First(pair => pair.Key.Equals(key)).Value;

        public void Add(TKey key, TValue value)
        {
            // Best(1)
            // O(n)
            // O(n / capacity)
            if (Contains(key))
                Remove(key);
            
            // O(1)
            _pairs.AddLast((key, value));
        }

        public bool Contains(TKey key) => 
            _pairs.Any(pair => pair.Key.Equals(key));

        public bool Remove(TKey key)
        {
            var value = Get(key);
            return _pairs.Remove((key, value));
        }

        public IEnumerable<TKey> Keys() => 
            _pairs.Select(pair => pair.Key);

        public IEnumerable<TValue> Values() => 
            _pairs.Select(pair => pair.Value);

        public IEnumerator<(TKey Key, TValue Value)> GetEnumerator() => 
            _pairs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}