using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventSolutions.Services
{
    public static class CeresSearch
    {
        public static int CountXMASOccurrences(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            char[,] grid = new char[lines.Length, lines[0].Length];

            // Populate the grid
            for (int r = 0; r < lines.Length; r++)
            {
                for (int c = 0; c < lines[r].Length; c++)
                {
                    grid[r, c] = lines[r][c];
                }
            }

            string targetWord = "XMAS";
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int occurrences = 0;

            // Define the 8 directions (row_change, col_change)
            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 }; // Row changes: Up, Up-Right, Up-Left, Left, Right, Down, Down-Right, Down-Left
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 }; // Column changes

            // Iterate through every cell in the grid
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // For each cell, check all 8 directions
                    for (int d = 0; d < 8; d++)
                    {
                        int currentRow = r;
                        int currentCol = c;
                        bool match = true;

                        // Try to match the target word in the current direction
                        for (int k = 0; k < targetWord.Length; k++)
                        {
                            // Check boundary conditions
                            if (currentRow < 0 || currentRow >= rows || currentCol < 0 || currentCol >= cols)
                            {
                                match = false;
                                break;
                            }

                            // Check if the character matches
                            if (grid[currentRow, currentCol] != targetWord[k])
                            {
                                match = false;
                                break;
                            }

                            // Move to the next position in the current direction
                            currentRow += dr[d];
                            currentCol += dc[d];
                        }

                        if (match)
                        {
                            occurrences++;
                        }
                    }
                }
            }

            return occurrences;
        }

        public static int CountXMASShapes(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            char[,] grid = new char[lines.Length, lines[0].Length];

            // Populate the grid
            for (int r = 0; r < lines.Length; r++)
            {
                for (int c = 0; c < lines[r].Length; c++)
                {
                    grid[r, c] = lines[r][c];
                }
            }

            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int occurrences = 0;

            // Iterate through all possible center points for the 'A'
            // The 'A' must have at least one character around it in all 8 directions,
            // so its row and column must be at least 1 and less than rows-1 / cols-1
            for (int r = 1; r < rows - 1; r++)
            {
                for (int c = 1; c < cols - 1; c++)
                {
                    // Check if the current cell is 'A' (the center of the X-MAS)
                    if (grid[r, c] == 'A')
                    {
                        // Check Diagonal 1: Top-Left to Bottom-Right (M-A-S or S-A-M)
                        bool diagonal1MAS = (grid[r - 1, c - 1] == 'M' && grid[r + 1, c + 1] == 'S');
                        bool diagonal1SAM = (grid[r - 1, c - 1] == 'S' && grid[r + 1, c + 1] == 'M');

                        // Check Diagonal 2: Top-Right to Bottom-Left (M-A-S or S-A-M)
                        bool diagonal2MAS = (grid[r - 1, c + 1] == 'M' && grid[r + 1, c - 1] == 'S');
                        bool diagonal2SAM = (grid[r - 1, c + 1] == 'S' && grid[r + 1, c - 1] == 'M');

                        // An X-MAS is formed if both diagonals have a valid MAS/SAM sequence
                        if ((diagonal1MAS || diagonal1SAM) && (diagonal2MAS || diagonal2SAM))
                        {
                            occurrences++;
                        }
                    }
                }
            }

            return occurrences;
        }
    }
}
