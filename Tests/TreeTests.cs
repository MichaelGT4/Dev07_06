using Implementations;

namespace Tests;

public class TreeTests
{
    [Fact]
    public void Count()
    {
        var tree = new Tree<string>();
        
        Assert.Equal(0, tree.Count());
    }
    
    [Fact]
    public void Count_SingleNode()
    {
        var tree = new Tree<string>("SuperMarket");

        Assert.Equal(1, tree.Count());
    }
    
    [Fact]
    public void Count_WithLeaf()
    {
        var tree = new Tree<string>("SuperMarket");

        tree.Add("Vegetables");

        Assert.Equal(2, tree.Count());
    }
    
    [Fact]
    public void Count_SubTree()
    {
        var tree = new Tree<string>("SuperMarket");

        tree.Add("Vegetables", node => 
            node.Add("Tomato"));

        Assert.Equal(3, tree.Count());
    }
    
    [Fact]
    public void Count_ManyNodes()
    {
        var tree = new Tree<string>("SuperMarket");

        tree
            .Add("Vegetables", vegetables =>
            {
                vegetables.Add("Tomato");
                vegetables.Add("Lettuce");
            })
            .Add("PersonalCare", personalCare =>
            {
                personalCare.Add("Shampoo", shampoo => 
                    shampoo.Add("HeadAndShoulders"));
            });

        Assert.Equal(7, tree.Count());
    }

    [Fact]
    public void TraversePreOrder()
    {
        var tree = new Tree<string>("SuperMarket");

        tree
            .Add("Vegetables", vegetables =>
            {
                vegetables.Add("Tomato");
                vegetables.Add("Lettuce");
            });

        var result = new List<string>();

        tree.TraversePreOrder(value => result.Add(value));

        Assert.Equal(new[]
            {
                "SuperMarket", 
                "Vegetables", 
                "Tomato", 
                "Lettuce"
            },
            result);
    }
    
    [Fact]
    public void TraversePostOrder()
    {
        var tree = new Tree<string>("SuperMarket");

        tree
            .Add("Vegetables", vegetables =>
            {
                vegetables.Add("Tomato");
                vegetables.Add("Lettuce");
            })
            .Add("PersonalCare", personalCare =>
            {
                personalCare.Add("Shampoo", shampoo => 
                    shampoo.Add("HeadAndShoulders"));
            });

        var result = new List<string>();

        tree.TraversePostOrder(value => result.Add(value));

        Assert.Equal(new[]
            {
                "Tomato", 
                "Lettuce",
                "Vegetables",
                "HeadAndShoulders",
                "Shampoo",
                "PersonalCare",
                "SuperMarket", 
            },
            result);
    }
    
    [Fact]
    public void TraverseLevelOrder()
    {
        var tree = new Tree<string>("SuperMarket");

        tree
            .Add("Vegetables", vegetables =>
            {
                vegetables.Add("Tomato");
                vegetables.Add("Lettuce");
            })
            .Add("PersonalCare", personalCare =>
            {
                personalCare.Add("Shampoo", shampoo => 
                    shampoo.Add("HeadAndShoulders"));
            });

        var result = new List<string>();

        tree.TraverseLevelOrder(value => result.Add(value));

        Assert.Equal(new[]
            {
                "SuperMarket", 
                "Vegetables",
                "PersonalCare",
                "Tomato", 
                "Lettuce",
                "Shampoo",
                "HeadAndShoulders",
            },
            result);
    }

    [Fact]
    public void Level()
    {
        var tree = new Tree<string>("SuperMarket");

        tree.Add("Vegetable", node => 
            Assert.Equal(1, node.Level()));
    }
    
    [Fact]
    public void Level_2()
    {
        var tree = new Tree<string>("SuperMarket");

        tree.Add("Vegetable", vegetables => 
            vegetables.Add("Tomato", tomato => 
                Assert.Equal(2, tomato.Level())));
    }
}