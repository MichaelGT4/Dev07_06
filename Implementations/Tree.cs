namespace Implementations;

public class Tree<T>
{
    private TreeNode? _root;

    public Tree() =>
        _root = null;

    public Tree(T rootValue) =>
        _root = new TreeNode(rootValue, null);

    public int Count() => _root?.Count() ?? 0;

    public Tree<T> Add(T value) => 
        Add(new TreeNode(value, _root));

    private Tree<T> Add(TreeNode node)
    {
        if (_root == null)
        {
            _root = node;
            return this;
        }

        _root.Add(node);
        return this;
    }

    public Tree<T> Add(T value, Action<TreeNode> action)
    {
        var node = new TreeNode(value, _root);
        Add(node);
        action(node);
        return this;
    }

    public void TraversePreOrder(Action<T> action) => 
        _root?.TraversePreOrder(action);

    public void TraversePostOrder(Action<T> action) => 
        _root?.TraversePostOrder(action);

    public void TraverseLevelOrder(Action<T> action) => 
        _root?.TraverseLevelOrder(action);

    public class TreeNode
    {
        private T Value { get; set; }
        private TreeNode? Parent { get; set; }
        private IList<TreeNode> Children { get; set; }

        public TreeNode(T value, TreeNode? parent)
        {
            Value = value;
            Parent = parent;
            Children = new List<TreeNode>();
        }

        public int Count() =>
            Children.Sum(node => node.Count()) + 1;

        public TreeNode Add(T value) =>
            Add(new TreeNode(value, this));

        public TreeNode Add(TreeNode node)
        {
            Children.Add(node);
            return this;
        }

        public TreeNode Add(T value, Action<TreeNode> action)
        {
            var node = new TreeNode(value, this);
            Add(node);
            action(node);
            return this;
        }

        public void TraversePreOrder(Action<T> action)
        {
            action(Value);

            foreach (var child in Children) 
                child.TraversePreOrder(action);
        }

        private void TraverseNodesPreOrder(Action<TreeNode> action)
        {
            action(this);

            foreach (var child in Children) 
                child.TraverseNodesPreOrder(action);
        }

        public void TraversePostOrder(Action<T> action)
        {
            foreach (var child in Children) 
                child.TraversePostOrder(action);
            
            action(Value);
        }

        public void TraverseLevelOrder(Action<T> action)
        {
            var levels = new Dictionary<int, List<TreeNode>>();
            
            TraverseNodesPreOrder(node =>
            {
                var level = node.Level();
                if (!levels.ContainsKey(level))
                    levels[level] = new List<TreeNode>();
                
                levels[level].Add(node);
            });

            foreach (var level in levels.Keys)
            foreach (var node in levels[level])
                action(node.Value);
        }

        public int Level()
        {
            if (Parent == null)
                return 0;
            
            return Parent.Level() + 1;
        }
    }
}