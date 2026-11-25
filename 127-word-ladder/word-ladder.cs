public class Solution 
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    { 
        wordList.Add(beginWord);
        
        // group all the words by their simillarities 
        // ex: abc and cbc will be in the same group but abc and bdc not
        var adjacentWordsGroup = GetAdjacentWords(wordList, out var wordPatterns); 
        
        // end word is not in the list => we violate the second condition
        if(!wordPatterns.ContainsKey(endWord))
        {
            return 0;
        }

        Queue<WordInfo> words = new() { new(beginWord, 1) }; 
        HashSet<string> processed = new() { beginWord };
        while(words.Any())
        {
            var (currentWord, moves) = words.Dequeue();

            // stop when we've reached the end
            if(currentWord == endWord)
            {
                return moves;
            }

            // navigate in moves graph
            foreach(var pattern in wordPatterns[currentWord])
            {   
                // skip not valid moves or missing moves
                if(!adjacentWordsGroup.ContainsKey(pattern))
                {
                    continue;
                }

                // put in queue the new words
                foreach(var adjacentWord in adjacentWordsGroup[pattern])
                {
                    if(processed.Contains(adjacentWord))
                    {
                        continue;
                    }

                    processed.Add(adjacentWord);
                    words.Enqueue(new(adjacentWord, moves + 1));
                }
            }
        }

        return 0;
    }

    private static Dictionary<string, List<string>> GetAdjacentWords(IList<string> words, out Dictionary<string, ISet<string>> wordPatterns)
    {   
        // for each word memorise its pattern
        wordPatterns = new Dictionary<string, ISet<string>>();

        Dictionary<string, List<string>> letterMap = new();

        // figure out which are the valid moves
        foreach(var word in words)
        {
            // ensure we have a key in dictionary
            if(!wordPatterns.ContainsKey(word))
            {
                wordPatterns[word] = new HashSet<string>();
            }

            // generate the patter for each word remaining after removing one letter
            foreach(var letterPattern in GetPatternOfAllSubstringsWithoutALetter(word))
            {
                // ensure there is always a key
                if(!letterMap.ContainsKey(letterPattern))
                {
                    letterMap[letterPattern] = new List<string>();
                }

                wordPatterns[word].Add(letterPattern);

                // put the word in the specific letter map
                letterMap[letterPattern].Add(word);
            }
        }

        return letterMap;

        IEnumerable<string> GetPatternOfAllSubstringsWithoutALetter(string word)
        {
            var letters = string.Join(string.Empty, word.Select((c, i) => $"{c}{i}"));

            // remove letter one by one and map words to the remaining letters
            for(var i = 1; i <= word.Length; ++i)
            {
                var lettersKey = $"{letters[0..((i - 1) * 2)]}{letters[(i * 2)..]}";

                yield return lettersKey;
            }
        }
    }

    

    private record WordInfo(string Word, int Moves);
}

internal static class QueueExtensions
{
    public static void Add<T>(this Queue<T> queue, T val)
    {
        queue.Enqueue(val);
    }
}   