using System.Collections;
using System.Formats.Asn1;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // TODO Problem 1
        // Create a new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty only tail will be affected
        else
        {
            // connect the node to the prev tail
            newNode.Prev = _tail; 
            // connect the tail to the new node
            _tail.Next = newNode;
            // Update the tail to point to the new node
            _tail = newNode;
        }
    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // TODO Problem 2
        if (_tail == _head)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the tail
        // will be affected.
        else if (_tail is not null)
        { 
            _tail.Prev!.Next = null; // Disconnect the second node from the first node
            _tail = _tail.Prev; // Update the tail to point to the second node
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        // TODO Problem 3
        Node? curr = _head;

        // Check the list to find the node containing value
        while (curr != null)
        {
            if (curr.Data == value)
            {
                // Case1: If the node to be removed is the head 
                if (curr == _head)
                {
                    // Reuse the removeHead function
                    RemoveHead();
                }
                // Case 2: If the node to be removed is the tail
                else if (curr == _tail)
                {
                    // Reuse the removeTail function
                    RemoveTail();
                }
                // Case 3: If the node is somewhere in the middle
                else
                {
                    // Connect the previous node to the next node
                    curr.Prev!.Next = curr.Next; 
                    if (curr.Next != null)
                    {
                        // Connect the next node to the previous node
                        curr.Next.Prev = curr.Prev;
                    }
                }

                return; // We can exit the function after removing the node
            }
            curr = curr.Next; // Go to the next node in the list
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // TODO Problem 4
        // Start at the end of the list
        Node? curr = _tail;
        // Move through the entire list
        while (curr != null)
        {
            // If the current node's data equals the oldValue
            if (curr.Data == oldValue)
            {
                // Replace the value with newValue
                curr.Data = newValue;
            }
            // Move to the next node in the list
            curr = curr.Prev;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        // TODO Problem 5
        // Create a temporary stack to hold the node
        Stack<int> stack = new Stack<int>();

        // Transverse the list and push the value onto the stack
        Node? curr = _head;
        while (curr != null)
        {
            stack.Push(curr.Data);
            curr = curr.Next;
        }
        // Yield value from the stack (this will be a reverse order)
        while (stack.Count > 0)
        {
            yield return stack.Pop();
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}