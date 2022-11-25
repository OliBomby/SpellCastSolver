using System.Reflection;
using System.Text;
using TrieNet.Trie;

namespace SpellCastSolverLib;

public class Solver {
    private readonly string[] words;
    private readonly Trie<int> trie;

    public int WordCount => words.Length;

    public Solver() {
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
                var word = new StringBuilder(s.Letter.ToString(), capacity:15);
                var path = new Stack<(int, int)>();
                path.Push((i, j));

                foreach (var result in SolveNode(board, path, word, s.Points, s.Gem ? 1 : 0, s.Multiplier, allowSwaps ? board.Gems / 3 : 0)) {
                    yield return result;
                }

                if (!allowSwaps || board.Gems < 3) continue;

                foreach (char letter in Letters) {
                    word = new StringBuilder(letter.ToString(), capacity: 15);

                    foreach (var result in SolveNode(board, path, word, 0, s.Gem ? 1 : 0, 1, board.Gems / 3 - 1)) {
                        yield return result;
                    }
                }
            }
        }
    }

    private IEnumerable<SolveResult> SolveNode(BoardState board, Stack<(int, int)> path, StringBuilder word, int points, int gems, int multiplier, int swaps) {
        foreach ((int row, int col) in GetNeighbours(board, path)) {
            var s = board.Board[row, col];
            int newMultiplier = multiplier * s.Multiplier;
            int newPoints = points + s.Points * s.PointsMultiplier;
            int newGems = s.Gem ? gems + 1 : gems;

            foreach (var r in TryResult(s.Letter, newMultiplier, newPoints, newGems, swaps)) {
                yield return r;
            }

            if (swaps <= 0) continue;

            foreach (char letter in Letters) {
                foreach (var r in TryResult(letter, multiplier, points, newGems - 3, swaps - 1)) {
                    yield return r;
                }
            }

            IEnumerable<SolveResult> TryResult(char c, int m, int p, int g, int sw) {
                word.Append(c);
                path.Push((row, col));

                var any = false;
                if (word.Length < 4) {
                    any = true;
                } else {
                    // Check if any words start with this prefix
                    var matches = trie.Retrieve(word.ToString().ToLower());
                    foreach (int match in matches) {
                        string matchWord = words[match];
                        // Create result if the whole word is matched
                        if (word.Length == matchWord.Length) {
                            yield return new SolveResult(words[match],  word.Length > 5 ? p * m + 10 : p * m, g, path.ToArray());
                        } else {
                            any = true;
                        }
                    }
                }

                if (any) {
                    // Return any recursive results
                    foreach (var solveResult in SolveNode(board, path, word, p, g, m, sw)) {
                        yield return solveResult;
                    }
                }

                path.Pop();
                word.Remove(word.Length - 1, 1);
            }
        }
    }

    private IEnumerable<(int, int)> GetNeighbours(BoardState board, Stack<(int, int)> path) {
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