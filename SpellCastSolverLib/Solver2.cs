using System.Reflection;
using System.Text;
using TrieNet.Trie;

namespace SpellCastSolverLib;

public class Solver2 {
    private readonly string[] words;
    private readonly Trie<int> trie;

    public int WordCount => words.Length;

    public Solver2() {
        var assembly = Assembly.GetExecutingAssembly();
        const string resourceName = @"SpellCastSolverLib.collins.txt";
        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        using StreamReader reader = new StreamReader(stream!);

        words = reader.ReadToEnd().Split("\n", StringSplitOptions.TrimEntries);
        trie = new Trie<int>();

        for (var i = 0; i < words.Length; i++) {
            trie.Add(words[i], i);
        }
    }

    public IEnumerable<SolveResult> Solve(BoardState board, bool allowSwaps = true) {
        for (var i = 0; i < board.Rows; i++) {
            for (var j = 0; j < board.Cols; j++) {
                var s = board.Board[i, j];
                var ch = char.ToLower(s.Letter);
                var word = new StringBuilder(ch.ToString(), capacity:15);
                var path = new Stack<(int, int)>();
                path.Push((i, j));

                foreach (var result in SolveNode(board, path, word, s.Points, s.Gem ? 1 : 0, s.Multiplier, allowSwaps ? board.Gems / 3 : 0)) {
                    yield return result;
                }

                if (!allowSwaps || board.Gems < 3) continue;

                foreach (char letter in Letters) {
                    if (letter == ch) continue;  // We already explored that

                    word = new StringBuilder(letter.ToString(), capacity: 15);

                    foreach (var result in SolveNode(board, path, word, 0, s.Gem ? 1 : 0, 1, board.Gems / 3 - 1)) {
                        yield return result;
                    }
                }
            }
        }
    }

    private IEnumerable<SolveResult> SolveNode(BoardState board, Stack<(int, int)> path, StringBuilder word, int points, int gems, int multiplier, int swaps) {
        // Get every next possible letter after word
        var matches = trie.Retrieve(word.ToString().ToLower());
        var any = false;
        var nextLetters = new bool[26];
        foreach (var match in matches) {
            var matchWord = words[match];
            if (word.Length == matchWord.Length) {
                any = true;
            } else {
                // Match is longer
                nextLetters[matchWord[word.Length] - 'a'] = true;
            }
        }

        // Create result if word is a word in the dictionary
        if (any && word.Length > 2)
            yield return new SolveResult(word.ToString(), points * multiplier, gems, path.ToArray());

        // Check if any neighbours have the next letters
        foreach ((int row, int col) in GetNeighbours(board, path)) {
            var s = board.Board[row, col];
            var ch = char.ToLower(s.Letter);
            int newGems = s.Gem ? gems + 1 : gems;

            if (nextLetters[ch - 'a']) {
                int newMultiplier = multiplier * s.Multiplier;
                int newPoints = points + s.Points * s.PointsMultiplier;

                foreach (var r in TryResult(ch, newMultiplier, newPoints, newGems, swaps)) {
                    yield return r;
                }
            }

            if (swaps <= 0) continue;

            for (int i = 0; i < 26; i++) {
                if (i == ch - 'a' || !nextLetters[i]) continue;

                foreach (var r in TryResult((char)('a' + i), multiplier, points, newGems - 3, swaps - 1)) {
                    yield return r;
                }
            }

            IEnumerable<SolveResult> TryResult(char c, int m, int p, int g, int sw) {
                word.Append(c);
                path.Push((row, col));

                // Return any recursive results
                foreach (var solveResult in SolveNode(board, path, word, p, g, m, sw)) {
                    yield return solveResult;
                }

                path.Pop();
                word.Remove(word.Length - 1, 1);
            }
        }
    }

    private static IEnumerable<(int, int)> GetNeighbours(BoardState board, Stack<(int, int)> path) {
        var end = path.Peek();

        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                var pos = (end.Item1 + i, end.Item2 + j);
                if (board.IsValid(pos) && !path.Contains(pos)) {
                    yield return pos;
                }
            }
        }
    }

    private static readonly char[] Letters = {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
    };
}