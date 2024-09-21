public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start

        // Intitialize an array with given length.
        double[] multiples = new double[length];

        // filling the array of multiples of the number
        for(int i = 0; i < length; i++){
            multiples[i] = number * (i + 1);
        }

        // return the array containing the multiple
        return multiples; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // put the amount within a range of list size
        amount = amount % data.Count;

        // get the last amount of element and the remaining part of the list.

        // last amount element
        List<int> lastPart = data.GetRange(data.Count - amount, amount); 
        // element before the amount
        List<int> firstPart = data.GetRange(0, data.Count - amount);

        // clear and reassemble the original list
        data.Clear();
        // add the rotational last part
        data.AddRange(lastPart);
        // then add the rotational first part
        data.AddRange(firstPart);
    }
}
