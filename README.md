# SpellCastSolver
Solves the SpellCast game activity in Discord.

![example](https://i.imgur.com/7CjTE28.png)

## Installation
Download the latest executable for your operating system from the releases and open it.

## Usage
- Type the input letters in the text box on the left. The order is from left to right, top to bottom. Don't add any spaces or separator.s
- Press the 'Import' button and observe the letters in the grid.
- Click a cell to toggle whether it has a gem added to it.
- Scroll on a cell to control the amount of points the cell gives. There are 4 possible states:
  - Normal (black)
  - 2x (yellow)
  - 3x (yellow)
  - 2x multiplier on the whole word (magenta)
- Press the + and - buttons underneath the grid to change the number of gems available. 
If you have 3 or more gems, then the algorithm will attempt to swap cells in order to obtain better results.
I don't recommend having 6 or more gems since it takes a long time to compute.
- Press the 'Solve' button to find the optimal words for the problem.
- Observe the listed results. Each entry has the word, the number of gems obtained (magenta), and the number of points obtained (yellow).
- Click on a result to toggle a visualization of the path in the grid.

## Nuget
A class library version of the algorithm is available on ~~nuget~~. You can use that to make your own applications which solve SpellCast problems.

## Used libraries
- [osu!framework](https://github.com/ppy/osu-framework)
- [TrieNet2](https://github.com/OliBomby/trienet)
