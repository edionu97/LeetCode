public class Solution 
{
    public IList<string> FullJustify(string[] words, int maxWidth) 
    {
        var strings = new List<string>();

        var i = 0;
        while(i < words.Length)
        {
            // get number of words to take from position i
            var noWords = GetNoOfWords(words, maxWidth, i, out var characters);

            // compute the other spaces required, excepting those between words
            var spacesNo = maxWidth - characters;
            var extraWordSpaces = spacesNo / Math.Max((noWords - 1), 1);
            var spaceCorrection = spacesNo % Math.Max((noWords - 1), 1);

            var builder = new StringBuilder();

            // handle last line
            if(i + noWords >= words.Length || noWords == 1)
            {
                for(var j = i; j < i + noWords; ++j)
                {
                    if(j > i)
                    {
                        builder.Append(' ');
                    }
                    
                    builder.Append(words[j]);
                }

                builder.Append(' ', spacesNo);
            }
            else
            {
                for(var j = i; j < i + noWords; ++j)
                {
                    builder.Append(words[j]);

                    var padding = 1 + extraWordSpaces + (spaceCorrection-- > 0 ? 1 : 0);

                    if(j + 1 < i + noWords)
                    {
                        builder.Append(' ', padding);
                    }
                }
            }

            strings.Add(builder.ToString());
            i += noWords;
        }

        return strings;
    }

    private static int GetNoOfWords(
        string[] words,
        int maxWidth,
        int i,
        out int characters)
    {
        characters = words[i].Length;

        int current = i + 1, noWords = 1;
        while(current < words.Length && characters + words[current].Length + 1 <= maxWidth)
        {
            characters += words[current++].Length + 1; 
            ++noWords;
        }

        return noWords;
    }
}