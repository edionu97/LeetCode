public class Solution 
{
    private static int[] Dx = {-1, 1, 0, 0};
    private static int[] Dy = {0, 0, -1, 1};

    public IList<string> FindWords(char[][] board, string[] words) 
    {
        // create trie from words to know all the matches between a letter
        var trie = Trie.Create();
        foreach(var word in words)
        {
            trie.AddWord(word);
        }

        HashSet<string> rezWords = new();

        int m = board.Length, n = board[0].Length;
        for(var i = 0; i < m; ++i)
        {
            for(var j = 0; j < n; ++j)
            {
                if(!trie.Letters.ContainsKey(board[i][j]))
                {
                    continue;
                }

                var b = board[i][j];

                board[i][j] = '.';
                GetAllWordsStartingFrom(i, j, trie.Letters[b]);
                board[i][j] = b;
            }
        }
        
        return rezWords.ToList();

        void GetAllWordsStartingFrom (int i, int j, Trie root)
        {
            if(!string.IsNullOrEmpty(root.InnerWord))
            {
                rezWords.Add(root.InnerWord);
            }
            
            for(var k = 0; k < 4; ++k)
            {
                var x = i + Dx[k];
                var y = j + Dy[k];

                // stay in board
                if(!(x >= 0 && y >= 0 && x < m && y < n))
                {
                    continue;
                }

                // perform only valid moves
                if(!root.Letters.ContainsKey(board[x][y]))
                {
                    continue;
                }

                var aux = board[x][y];

                board[x][y] = '.';
                GetAllWordsStartingFrom(x, y, root.Letters[aux]);
                board[x][y] = aux;
            }
        }   
    }

    private class Trie
    {
        private Trie Root { get; set; }

        public string InnerWord { get; private set; } 

        public Dictionary<char, Trie> Letters { get; } = new();

        public static Trie Create()
        {
            var trie = new Trie();
            trie.Root = trie;
            return trie;
        }

        private Trie()
        {
        }

        public void AddWord(string word)
        {
            InsertWord(0, Root);

            // recursive method for building the trie 
            void InsertWord(int index, Trie root)
            {
                // handle the case when the word is fully matched
                if(index == word.Length)
                {
                    root.InnerWord = word;
                    return;
                }

                // get the character at current index
                var chr = word[index];

                // ensure we're adding the character in node's Letters
                if(!root.Letters.ContainsKey(chr))
                {
                    root.Letters[chr] = new Trie();
                }

                InsertWord(index + 1, root.Letters[chr]);
            }
        }
    }
}