namespace Implementations;

public interface ILnkList<T> : IEnumerable<T>
{
    void Prepend(T value);
    void Append(T value);
    T this[int index] { get; }
    T Get(int index);
    int Count { get; }
    bool RemoveAt(int index);
    bool Remove(T value);
}