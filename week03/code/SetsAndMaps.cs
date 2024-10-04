using System.Globalization;
using System.Text.Json;

public static class SetsAndMaps
{
    private static object census;

    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        // Create a Hashset to store the wrds
        HashSet<string> seen = new HashSet<string>();
        // This will store the symmetric pair
        List<string> result = new List<string>();

        foreach (string word in words)
        {
            // Reverse the word
            string reverse = new string(new Char[] { word[1], word[0] });
            // Check if the reverse is aready in the set
            if (seen.Contains(reverse))
            {
                // if reverse is in the pair add the symmetric pair
                result.Add($"{reverse} & {word}");
            }
            else
            {
                // Add the word to the set
                seen.Add(word);
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
            // TODO Problem 2 - ADD YOUR CODE HERE

            // Initialize the dictionary to hold the degree summary
            var degrees = new Dictionary<string, int>();

            // Read each line from the file
            foreach(var line in File.ReadLines(filename))
            {
                // Splitting lines by commas to get the column 
                var fields = line.Split(",");

                // Ensure the line has at least 4 column to avoid index out range errors
                if(fields.Length < 4)
                continue;

                // Get the degree from the 4th column (index 3)
                var degree = fields[3].Trim();

                // Update the dictionary: Increment count if degree exist, otherwise add new entry 
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }

        // Redturn degree summary
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE

        // step 2: Normalize the words (convert to lowercase and remove the space)
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // step2: if the length are different, they cannot be anagram 
        if(word1.Length != word2.Length)
        {
            return false;
        }

        // Step 3: Create dictionaries to count the frequency of each letter
        var letterCount1 = new Dictionary<char, int>();
        var letterCount2 = new Dictionary<char, int>();

        // Step 4: Populate the dictionaries with letter frequencies 
        foreach (var letter in word1)
        {
            if (letterCount1.ContainsKey(letter))
            {
                letterCount1[letter]++;
            }
            else
            {
                letterCount1[letter] = 1;
            }
        }

        foreach(var letter in word2)
        {
            if (letterCount2.ContainsKey(letter))
            {
                letterCount2[letter]++;
            }
            else
            {
                letterCount2[letter] = 1;
            }
        }

        // step 5: Compare the two dictionaries
        return DictionaryEquals(letterCount1, letterCount2);
    }

    // Helper method to compare two dictionaries 
    private static bool DictionaryEquals(Dictionary<char, int> dict1, Dictionary<char, int> dict2)
    {
        if (dict1.Count != dict2.Count)
        {
            return false;
        }

        foreach (var kvp in dict1)
        {
            if (!dict2.ContainsKey(kvp.Key) || dict2[kvp.Key] != kvp.Value)
            {
                return false;
            }
        }

        return true;
        }
    


    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        // Create a list to hold the formatted earthquaks information
        var earthquakeSummaries = new List<string>();

        foreach(var feature in featureCollection.Features)
        {
            var magnitude = feature.Properties.Mag;
            var place = feature.Properties.Place;

            // Only include earthquakes with both a magnitude and place defined 
            if (magnitude.HasValue && !string.IsNullOrEmpty(place))
            {
                // Format the earthquake information into a string
                earthquakeSummaries.Add($"{place} - Mag {magnitude.Value}");
            }
        }
        return earthquakeSummaries.ToArray();
    }
}