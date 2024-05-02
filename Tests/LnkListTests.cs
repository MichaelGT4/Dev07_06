using Implementations;

namespace Tests;

public class LnkListTests : AbstractLnkListTests
{
    protected override ILnkList<T> CreateLinkedList<T>() => new LnkList<T>();
    protected override ILnkList<T> CreateLinkedList<T>(params T[] values) => LnkList<T>.From(values);
}