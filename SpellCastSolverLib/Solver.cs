using System.Text;
using TrieNet.Trie;

namespace SpellCastSolverLib;

public class Solver {
    private readonly string[] words;
    private readonly Trie<int> trie;

    public Solver(string[] words) {
        this.words = words;
        trie = new Trie<int>();

        for (var i = 0; i < words.Length; i++) {
            trie.Add(words[i], i);
        }
    }

    public IEnumerable<SolveResult> Solve(BoardState board) {
        for (var i = 0; i < board.Rows; i++) {
            for (var j = 0; j < board.Cols; j++) {
                var s = board.Board[i, j];
                var word = new StringBuilder(s.Letter.ToString(), capacity:10);
                var path = new Stack<(int, int)>();
                path.Push((i, j));

                foreach (var result in SolveNode(board, path, word, s.Points, s.Gem ? 1 : 0, s.Multiplier)) {
                    yield return result;
                }
            }
        }
    }

    private IEnumerable<SolveResult> SolveNode(BoardState board, Stack<(int, int)> path, StringBuilder word, int points, int gems, int multiplier) {
        foreach ((int row, int col) in GetNeighbours(board, path)) {
            var s = board.Board[row, col];
            int m = multiplier * s.Multiplier;
            int p = points + s.Points * s.PointsMultiplier;
            int g = s.Gem ? gems + 1 : gems;

            word.Append(s.Letter);
            path.Push((row, col));

            // Check if any words start with this prefix
            var matches = trie.Retrieve(word.ToString().ToLower());
            var any = false;
            foreach (int match in matches) {
                // Create result if the whole word is matched
                if (word.Length == words[match].Length) {
                    yield return new SolveResult(words[match], p * m, g, path.ToArray());
                } else {
                    any = true;
                }
            }

            if (any) {
                // Return any recursive results
                foreach (var solveResult in SolveNode(board, path, word, p, g, m)) {
                    yield return solveResult;
                }
            }

            path.Pop();
            word.Remove(word.Length - 1, 1);
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
}