namespace Tests;

public class QueueTests
{
    [Fact]
    public void Dequeue()
    {
        var q = new Queue<string>();
        
        q.Enqueue("a");
        q.Enqueue("b");
        q.Enqueue("c");
        
        Assert.Equal("a", q.Dequeue());
        Assert.Equal("b",  q.Peek());
        Assert.Equal("b", q.Dequeue());
        Assert.Equal("c",  q.Peek());
    }
    
    [Fact]
    public void AsEnumerable()
    {
        var q = new Queue<string>();
        
        q.Enqueue("a");
        q.Enqueue("b");
        q.Enqueue("c");
        
        Assert.Equal(new[] { "a", "b", "c"}, q);
    }
}