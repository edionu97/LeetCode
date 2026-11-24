public class MinStack
{
    private Node? head;

    public MinStack()
    {
        head = null;
    }

    public void Push(int value)
    {
        Node node = new(value)
        {
            Next = head,
            MinValue = value
        };

        if (node.Next is not null)
        {
            node.MinValue = Math.Min(value, node.Next.MinValue);
        }

        head = node;
    }

    public void Pop() => head = head?.Next;

    public int Top() => head?.Value ?? 0;

    public int GetMin() => head?.MinValue ?? int.MinValue;

    private class Node(int value)
    {
        public int MinValue { get; set; }

        public int Value => value;

        public Node? Next { get; set; }
    }
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(value);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */