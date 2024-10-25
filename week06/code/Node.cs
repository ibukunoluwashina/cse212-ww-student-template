public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        // Check if the value is equal to the current node data
        if (value == Data)
        {
            //Value already exist, do not insert it
            return;
        }
        
        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2

        // Base case: if the current node data matches the value, return true.
        if(value == Data)
        {
            return true;
        }

        // If the value is less than the current node's data, search the left subtree.
        if (value < Data)
        {
            // If there is no left child, the value is not in the tree
            if (Left is null)
            {
                return false;
            }
            // Recur the right
            return Left.Contains(value);
        }
        else
        {
            // If the vaue is greater than the current node's data, search the right subtree
            // If there is no right child, the value is not in the tree
            if (Right is null)
            {
                return false;
            }
            // Recur on the right child.
            return Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4

        // Base case: if both left and right are null, this is a leaf node, return zero
        if (Left == null && Right == null)
        {
            // The hight of a single node tree is 1
            return 1;
        }

        // Recursively calculate the hight of the left subtree
        int leftHeight = Left != null ? Left.GetHeight() : 0;

        // Recursively calculate the height of the right subtree 
        int RightHeight = Right != null ? Right.GetHeight() : 0;
        return 1 + Math.Max(leftHeight, RightHeight); // Return the greater height of the two subtree plus 1 for the cureent node.
    }
}