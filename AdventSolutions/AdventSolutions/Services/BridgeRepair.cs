using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventSolutions.Services
{
    public static class BridgeRepair
    {
        private static List<long> _currentNumbers;
        private static long _targetTestValue;
        public static long Solve(string input)
        {
            long totalCalibrationResult = 0;
            string[] equations = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string equationLine in equations)
            {
                var parts = equationLine.Split(':');
                _targetTestValue = long.Parse(parts[0].Trim());
                _currentNumbers = parts[1].Trim()
                                         .Split(' ')
                                         .Select(long.Parse)
                                         .ToList();

                // If there's only one number, it must match the test value directly
                if (_currentNumbers.Count == 1)
                {
                    if (_currentNumbers[0] == _targetTestValue)
                    {
                        totalCalibrationResult += _targetTestValue;
                    }
                    continue; // Move to the next equation
                }

                // Start the recursive check from the first number (index 0)
                if (CanSolve(0, _currentNumbers[0]))
                {
                    totalCalibrationResult += _targetTestValue;
                }
            }

            return totalCalibrationResult;
        }

        /// <summary>
        /// Recursively checks if the remaining numbers can form the target test value
        /// using '+', '*', or '||' operators, evaluated left-to-right.
        /// </summary>
        /// <param name="currentIndex">The index of the number we just processed and whose result is in currentResult.</param>
        /// <param name="currentResult">The current accumulated result up to numbers[currentIndex].</param>
        /// <returns>True if a solution path is found, false otherwise.</returns>
        private static bool CanSolve(int currentIndex, long currentResult)
        {
            // Base case: If we've processed all numbers
            if (currentIndex == _currentNumbers.Count - 1)
            {
                return currentResult == _targetTestValue;
            }

            long nextNumber = _currentNumbers[currentIndex + 1];

            // Option 1: Try addition
            if (CanSolve(currentIndex + 1, currentResult + nextNumber))
            {
                return true;
            }

            // Option 2: Try multiplication
            // Check for potential overflow before multiplication for very large numbers
            // Although for AoC constraints, direct multiplication followed by check is usually fine.
            // For robustness, could check: if (currentResult != 0 && long.MaxValue / Math.Abs(currentResult) < Math.Abs(nextNumber)) { /* potential overflow */ }
            if (CanSolve(currentIndex + 1, currentResult * nextNumber))
            {
                return true;
            }

            // Option 3: Try concatenation
            string currentStr = currentResult.ToString();
            string nextStr = nextNumber.ToString();
            string concatenatedStr = currentStr + nextStr;

            // Check if the concatenated string is too long for a long, or if it represents a negative number (not applicable here)
            // A long can hold up to 18-19 digits. If the string is longer, it will definitely overflow.
            // Even if it's within length, long.TryParse is safest.
            if (concatenatedStr.Length <= 19) // Max digits for long is 19
            {
                if (long.TryParse(concatenatedStr, out long concatenatedNum))
                {
                    if (CanSolve(currentIndex + 1, concatenatedNum))
                    {
                        return true;
                    }
                }
            }

            // If none of the options work from this point, then no solution down this path
            return false;
        }
    }
}
