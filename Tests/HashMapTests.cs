using Implementations;

namespace Tests;

public class HashMapTests
{
    [Fact]
    public void Get_ThreeElements_10Capacity()
    {
        var hashMap = new HashMap<string, int>(capacity: 10);
        
        hashMap.Add("Monday", 20);
        hashMap.Add("Wednesday", 23);
        hashMap.Add("Friday", 25);
        
        Assert.Equal(25, hashMap["Friday"]);
    }
    
    [Fact]
    public void Get_Collision()
    {
        var hashMap = new HashMap<string, int>(capacity: 2)
        {
            ["Monday"] = 20,
            ["Wednesday"] = 23,
            ["Friday"] = 25
        };

        Assert.Equal(20, hashMap["Monday"]);
        Assert.Equal(23, hashMap["Wednesday"]);
        Assert.Equal(25, hashMap["Friday"]);
    }
    
    [Fact]
    public void Get_Chars()
    {
        var hashMap = new HashMap<char, int>(capacity: 2)
        {
            ['M'] = 20,
            ['W'] = 23,
            ['F'] = 25
        };

        Assert.Equal(20, hashMap['M']);
        Assert.Equal(23, hashMap['W']);
        Assert.Equal(25, hashMap['F']);
    }
    
    [Fact]
    public void Get_Override()
    {
        var hashMap = new HashMap<char, int>(capacity: 2)
        {
            ['M'] = 20,
            ['M'] = 25
        };

        Assert.Equal(25, hashMap['M']);
    }
    
    [Fact]
    public void Contains()
    {
        var hashMap = new HashMap<string, double>
        {
            ["foo"] = 2.0
        };

        Assert.True(hashMap.Contains("foo"));
    }
    
    [Fact]
    public void Contains_NotFound()
    {
        var hashMap = new HashMap<string, double>();
        Assert.False(hashMap.Contains("bar"));
    }

    [Fact]
    public void Remove()
    {
        var hashMap = new HashMap<string, int>
        {
            ["Foo"] = 1,
            ["Bar"] = 2
        };

        Assert.True(hashMap.Remove("Foo"));
        Assert.False(hashMap.Contains("Foo"));
    }

    [Fact]
    public void Keys()
    {
        var hashMap = new HashMap<string, int>(capacity: 2)
        {
            ["Foo"] = 1,
            ["Bar"] = 2,
            ["Car"] = 3,
        };
        
        Assert.Equal(new[] { "Bar", "Car", "Foo" }, 
            hashMap.Keys().Order());
    }
    
    [Fact]
    public void Values()
    {
        var hashMap = new HashMap<string, int>(capacity: 2)
        {
            ["Foo"] = 1,
            ["Bar"] = 2,
            ["Car"] = 3,
        };
        
        Assert.Equal(new[] { 1, 2, 3 }, 
            hashMap.Values().Order());
    }
    
    [Fact]
    public void Foreach()
    {
        var hashMap = new HashMap<string, int>
        {
            ["Foo"] = 1,
            ["Bar"] = 2,
            ["Car"] = 3,
        };

        var result = 0;

        foreach (var pair in hashMap) 
            result += pair.Value;

        Assert.Equal(6, result);
    }
    
    [Fact]
    public void Sum()
    {
        var hashMap = new HashMap<string, int>
        {
            ["Foo"] = 1,
            ["Bar"] = 2,
            ["Car"] = 3,
        };

        var result = hashMap.Sum(pair => pair.Value);

        Assert.Equal(6, result);
    }
}