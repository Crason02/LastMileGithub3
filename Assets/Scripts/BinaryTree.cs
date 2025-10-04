using System;

public class Node<T>
{
    public T Value;
    public Node<T> Left;
    public Node<T> Right;

    public Node(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

public class BinaryTree<T> where T : IComparable<T>
{
    public Node<T> Root;

    public void Insert(T value)
    {
        Root = InsertRec(Root, value);
    }

    private Node<T> InsertRec(Node<T> root, T value)
    {
        if (root == null)
            return new Node<T>(value);

        if (value.CompareTo(root.Value) < 0)
            root.Left = InsertRec(root.Left, value);
        else if (value.CompareTo(root.Value) > 0)
            root.Right = InsertRec(root.Right, value);

        return root;
    }

    public void InOrder()
    {
        InOrderRec(Root);
        Console.WriteLine();
    }

    private void InOrderRec(Node<T> root)
    {
        if (root != null)
        {
            InOrderRec(root.Left);
            Console.WriteLine(root.Value);
            InOrderRec(root.Right);
        }
    }
}

class Program
{
    static void Main()
    {
        BinaryTree<string> tree = new BinaryTree<string>();

        tree.Insert("Hello world");
        tree.Insert("Apple pie");
        tree.Insert("Zebra crossing");
        tree.Insert("Banana split");

        Console.WriteLine("Inorder traversal of sentences:");
        tree.InOrder(); 
        // Output (alphabetical order):
        // Apple pie
        // Banana split
        // Hello world
        // Zebra crossing
    }
}