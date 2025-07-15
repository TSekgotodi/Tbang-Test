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

            
            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 }; 
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 }; 

            
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    
                    for (int d = 0; d < 8; d++)
                    {
                        int currentRow = r;
                        int currentCol = c;
                        bool match = true;

                        
                        for (int k = 0; k < targetWord.Length; k++)
                        {
                            
                            if (currentRow < 0 || currentRow >= rows || currentCol < 0 || currentCol >= cols)
                            {
                                match = false;
                                break;
                            }

                            
                            if (grid[currentRow, currentCol] != targetWord[k])
                            {
                                match = false;
                                break;
                            }

                            
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

            
            for (int r = 1; r < rows - 1; r++)
            {
                for (int c = 1; c < cols - 1; c++)
                {
                    
                    if (grid[r, c] == 'A')
                    {
                       
                        bool diagonal1MAS = (grid[r - 1, c - 1] == 'M' && grid[r + 1, c + 1] == 'S');
                        bool diagonal1SAM = (grid[r - 1, c - 1] == 'S' && grid[r + 1, c + 1] == 'M');

                        
                        bool diagonal2MAS = (grid[r - 1, c + 1] == 'M' && grid[r + 1, c - 1] == 'S');
                        bool diagonal2SAM = (grid[r - 1, c + 1] == 'S' && grid[r + 1, c - 1] == 'M');

                       
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
