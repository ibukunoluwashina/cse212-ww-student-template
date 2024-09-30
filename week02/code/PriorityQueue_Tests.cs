using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Adding multiple items to the priority queue and dequeuing.
    // Expected Result: The item with the highest priority should be dequeued first.
    // Defect(s) Found: 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item 1", 1);
        priorityQueue.Enqueue("Item 2", 3);
        priorityQueue.Enqueue("Item 3", 2);

        // Dequeue should return the item with the higest priority i.e item 2 with priority 3
        var dequeuedItem = priorityQueue.Dequeue();
        Assert.AreEqual("Item 2", dequeuedItem, "The item with the highest priority was not dequed first.");
    }

    [TestMethod]
    // Scenario: Enqueque one item and dequeque it. 
    // Expected Result: The only item in the queque should be dequequed
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item 1", 5);

        // Dequeue should return "Item 1" since its the only item
        var dequequedItem = priorityQueue.Dequeue();
        Assert.AreEqual("Item 1", dequequedItem, "");
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenerio: Attempt to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException should be thrown.
    // Defect (s) Found: 
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Dequeue();
    }
    
}