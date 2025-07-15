using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventSolutions.Services
{
    public static class GuardGallivant
    {
        public static int SimulateGuardPath(string input)
        {
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] grid = new char[rows, cols];

            int guardRow = -1;
            int guardCol = -1;
            int guardDirection = -1; 

            
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = lines[r][c];
                    if (grid[r, c] == '^')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 0; 
                    }
                    else if (grid[r, c] == '>')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 1; 
                    }
                    else if (grid[r, c] == 'v')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 2; 
                    }
                    else if (grid[r, c] == '<')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 3; 
                    }
                }
            }

           
            int[] dr = { -1, 0, 1, 0 }; 
            int[] dc = { 0, 1, 0, -1 }; 

            HashSet<(int r, int c)> visitedPositions = new HashSet<(int r, int c)>();

           
            while (guardRow >= 0 && guardRow < rows && guardCol >= 0 && guardCol < cols)
            {
                visitedPositions.Add((guardRow, guardCol));

                int nextRow = guardRow + dr[guardDirection];
                int nextCol = guardCol + dc[guardDirection];

               
                bool obstacleInFront = false;
                if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols)
                {
                    obstacleInFront = true; 
                }
                else if (grid[nextRow, nextCol] == '#')
                {
                    obstacleInFront = true; 
                }

                if (obstacleInFront)
                {
                    
                    guardDirection = (guardDirection + 1) % 4;
                }
                else
                {
                    
                    guardRow = nextRow;
                    guardCol = nextCol;
                }
            }

            return visitedPositions.Count;
        }
    }
}
