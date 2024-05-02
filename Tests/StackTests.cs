namespace Tests;

public class StackTests
{
    [Fact]
    public void Pop()
    {
        // LIFO
        var s = new Stack<string>();
        
        s.Push("A");
        s.Push("B");

        var actual = s.Pop();
        
        Assert.Equal("B", actual);
        Assert.Equal("A", s.Peek());
    }

    [Fact]
    public void Pop_Empty()
    {
        var s = new Stack<int>();
        
        Assert.Throws<InvalidOperationException>(() => s.Pop());
    }

    [Fact]
    public void AsEnumerable()
    {
        var s = new Stack<int>();
        
        s.Push(1);
        s.Push(2);
        s.Push(3);
        
        Assert.Equal(new[] { 3, 2, 1}, s);
    }
}