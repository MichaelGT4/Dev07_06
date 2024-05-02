using Implementations;

namespace Tests;

public abstract class AbstractLnkListTests
{
    protected abstract ILnkList<T> CreateLinkedList<T>();
    protected abstract ILnkList<T> CreateLinkedList<T>(params T[] values);

    [Fact]
    public void GetByIndex_UsingIndexer()
    {
        var ll = CreateLinkedList<string>();

        ll.Append("a");
        ll.Append("b");
        ll.Append("c");

        var result = ll[1];

        Assert.Equal("b", result);
    }

    [Fact]
    public void GetByIndex()
    {
        var ll = CreateLinkedList<string>();

        ll.Append("a");
        ll.Append("b");
        ll.Append("c");

        var result = ll.Get(1);

        Assert.Equal("b", result);
    }

    [Fact]
    public void GetByIndex_NotFound()
    {
        var ll = CreateLinkedList<int>();

        Assert.Throws<IndexOutOfRangeException>(
            () => ll.Get(1));
    }

    [Fact]
    public void GetByIndex_OutOfRange_Negative()
    {
        var ll = CreateLinkedList<int>();

        Assert.Throws<IndexOutOfRangeException>(
            () => ll.Get(-1));
    }

    [Fact]
    public void GetByIndex_OutOfRange_GreaterThanCount()
    {
        var ll = CreateLinkedList<int>();

        ll.Append(10);

        Assert.Throws<IndexOutOfRangeException>(
            () => ll.Get(2));
    }

    [Fact]
    public void Any_NotFound()
    {
        var ll = CreateLinkedList<char>();

        Assert.False(ll.Any(value => value == 'a'));
    }

    [Fact]
    public void Any_Exists()
    {
        var ll = CreateLinkedList<char>();

        ll.Append('a');
        ll.Append('b');
        ll.Append('c');

        Assert.True(ll.Any(value => value == 'b'));
    }

    [Fact]
    public void First_AfterPrepend()
    {
        var ll = CreateLinkedList<int>();

        ll.Prepend(30);

        Assert.Equal(30, ll.First());
    }

    [Fact]
    public void First_FromMany()
    {
        var ll = CreateLinkedList<int>();

        ll.Append(30);
        ll.Append(40);
        ll.Append(50);

        Assert.Equal(30, ll.First());
    }

    [Fact]
    public void First_Empty()
    {
        var ll = CreateLinkedList<int>();

        Assert.Throws<InvalidOperationException>(() => ll.First());
    }

    [Fact]
    public void Count()
    {
        var ll = CreateLinkedList<int>();

        Assert.Equal(0, ll.Count);
    }

    [Fact]
    public void Count_OneElement()
    {
        var ll = CreateLinkedList<int>();

        ll.Prepend(10);

        Assert.Equal(1, ll.Count);
    }

    [Fact]
    public void Count_ManyElement()
    {
        var ll = CreateLinkedList<int>();

        ll.Append(10);
        ll.Append(20);
        ll.Append(30);

        Assert.Equal(3, ll.Count);
    }
    
    [Fact]
    public void Count_AfterAppend()
    {
        var ll = CreateLinkedList<string>();
        ll.Append("a");
        
        Assert.Equal(1, ll.Count);
    }
    
    [Fact]
    public void Count_AfterPrepend()
    {
        var ll = CreateLinkedList<string>();
        ll.Prepend("a");
        
        Assert.Equal(1, ll.Count);
    }
    
    [Fact]
    public void Count_AfterRemoveAt()
    {
        var ll = CreateLinkedList<string>();
        ll.Prepend("a");
        ll.RemoveAt(0);
        
        Assert.Equal(0, ll.Count);
    }
    
    [Fact]
    public void Count_AfterRemoveAtMany()
    {
        var ll = CreateLinkedList<string>();
        ll.Append("a");
        ll.Append("b");
        ll.Append("c");
        ll.RemoveAt(1);
        
        Assert.Equal(2, ll.Count);
    }
    
    [Fact]
    public void Count_AfterRemove()
    {
        var ll = CreateLinkedList<string>();
        ll.Prepend("a");
        ll.Remove("a");
        
        Assert.Equal(0, ll.Count);
    }
    
    [Fact]
    public void Count_AfterRemoveMany()
    {
        var ll = CreateLinkedList<string>();
        ll.Append("a");
        ll.Append("b");
        ll.Append("c");
        ll.Remove("b");
        
        Assert.Equal(2, ll.Count);
    }

    [Fact]
    public void ToEnumerable_Empty()
    {
        var ll = CreateLinkedList<int>();

        Assert.Equal(Array.Empty<int>(), ll);
    }

    [Fact]
    public void ToEnumerable_OneElement()
    {
        var ll = CreateLinkedList<int>();

        ll.Append(1);

        Assert.Equal(new[] { 1 }, ll);
    }

    [Fact]
    public void ToEnumerable_ManyElement()
    {
        var ll = CreateLinkedList<int>();

        ll.Append(1);
        ll.Append(2);
        ll.Append(3);

        Assert.Equal(new[] { 1, 2, 3 }, ll);
    }

    [Fact]
    public void ToEnumerable_Prepend()
    {
        var ll = CreateLinkedList<int>();

        ll.Append(1);
        ll.Prepend(2);

        Assert.Equal(new[] { 2, 1 }, ll);
    }

    [Fact]
    public void RemoveAt_Empty()
    {
        var list = CreateLinkedList<string>();

        Assert.False(list.RemoveAt(0));
    }

    [Fact]
    public void RemoveAt_Many()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.True(list.RemoveAt(1));

        Assert.Equal(new[] { "a", "c" }, list);
    }

    [Fact]
    public void RemoveAt_Last()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.True(list.RemoveAt(2));

        Assert.Equal(new[] { "a", "b" }, list);
    }
    
    [Fact]
    public void RemoveAt_First()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.True(list.RemoveAt(0));

        Assert.Equal(new[] { "b", "c" }, list);
    }

    [Fact]
    public void RemoveAt_NotFound()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.False(list.RemoveAt(3));

        Assert.Equal(new[] { "a", "b", "c" }, list);
    }

    [Fact]
    public void RemoveAt_NegativeIndex()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.False(list.RemoveAt(-1));

        Assert.Equal(new[] { "a", "b", "c" }, list);
    }

    [Fact]
    public void Remove_Empty()
    {
        var list = CreateLinkedList<string>();

        Assert.False(list.Remove("a"));
    }

    [Fact]
    public void Remove_FromMany()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.True(list.Remove("b"));

        Assert.Equal(new[] { "a", "c" }, list);
    }

    [Fact]
    public void Remove_NotFound()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.False(list.Remove("d"));

        Assert.Equal(new[] { "a", "b", "c" }, list);
    }
    
    [Fact]
    public void Remove_Last()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.True(list.Remove("c"));

        Assert.Equal(new[] { "a", "b" }, list);
    }
    
    [Fact]
    public void Remove_First()
    {
        var list = CreateLinkedList<string>("a", "b", "c");

        Assert.True(list.Remove("a"));

        Assert.Equal(new[] { "b", "c" }, list);
    }

    [Fact]
    public void Enumerate()
    {
        var list = LnkList<char>.From('a', 'b', 'c');

        var result = "";
        
        foreach (var value in list) 
            result += value;

        Assert.Equal("abc", result);
    }
    
    [Fact]
    public void Enumerate_Where()
    {
        var list = LnkList<int>.From(1, 2, 3, 4);

        var result = list.Where(c => c < 3);

        Assert.Equal(new[] { 1, 2 }, result);
    }
}