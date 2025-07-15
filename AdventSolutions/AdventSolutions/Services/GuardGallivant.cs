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
            int guardDirection = -1; // 0: Up, 1: Right, 2: Down, 3: Left

            // Parse grid and find initial guard position/direction
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = lines[r][c];
                    if (grid[r, c] == '^')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 0; // Up
                    }
                    else if (grid[r, c] == '>')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 1; // Right
                    }
                    else if (grid[r, c] == 'v')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 2; // Down
                    }
                    else if (grid[r, c] == '<')
                    {
                        guardRow = r;
                        guardCol = c;
                        guardDirection = 3; // Left
                    }
                }
            }

            // Define row and column changes for each direction
            // Index: 0=Up, 1=Right, 2=Down, 3=Left
            int[] dr = { -1, 0, 1, 0 }; // Change in row for each direction
            int[] dc = { 0, 1, 0, -1 }; // Change in column for each direction

            HashSet<(int r, int c)> visitedPositions = new HashSet<(int r, int c)>();

            // Simulation loop
            while (guardRow >= 0 && guardRow < rows && guardCol >= 0 && guardCol < cols)
            {
                visitedPositions.Add((guardRow, guardCol));

                int nextRow = guardRow + dr[guardDirection];
                int nextCol = guardCol + dc[guardDirection];

                // Check if there's an obstacle or boundary directly in front
                bool obstacleInFront = false;
                if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols)
                {
                    obstacleInFront = true; // Boundary is an obstacle
                }
                else if (grid[nextRow, nextCol] == '#')
                {
                    obstacleInFront = true; // Obstruction
                }

                if (obstacleInFront)
                {
                    // Turn right 90 degrees
                    guardDirection = (guardDirection + 1) % 4;
                }
                else
                {
                    // Take a step forward
                    guardRow = nextRow;
                    guardCol = nextCol;
                }
            }

            return visitedPositions.Count;
        }
    }
}
