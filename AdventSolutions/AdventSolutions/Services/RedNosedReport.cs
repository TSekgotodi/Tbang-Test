using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventSolutions.Services
{
    public static class RedNosedReport
    {
        public static int CountSafeReports(string input)
        {
            int safeReports = 0;
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                List<int> levels = line.Split(' ')
                                       .Select(int.Parse)
                                       .ToList();

                if (IsReportSafe(levels))
                {
                    safeReports++;
                }
            }

            return safeReports;
        }
        private static bool IsReportSafe(List<int> levels)
        {
            if (levels.Count <= 1)
            {
                // A report with 0 or 1 level is considered safe as there are no adjacent pairs to violate rules.
                return true;
            }

            // Determine initial trend
            int firstDiff = levels[1] - levels[0];
            if (firstDiff == 0 || Math.Abs(firstDiff) > 3)
            {
                return false; // Violates difference constraint or is static
            }

            bool isIncreasing = firstDiff > 0;

            // Check remaining adjacent levels
            for (int i = 1; i < levels.Count; i++)
            {
                int currentLevel = levels[i];
                int previousLevel = levels[i - 1];
                int diff = currentLevel - previousLevel;

                // Check difference constraint
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                {
                    return false;
                }

                // Check monotonicity
                if (isIncreasing)
                {
                    if (diff <= 0) // Should be increasing
                    {
                        return false;
                    }
                }
                else // isDecreasing
                {
                    if (diff >= 0) // Should be decreasing
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static int CountSafeReportsWithDampener(string input)
        {
            int safeReports = 0;
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                List<int> levels = line.Split(' ')
                                       .Select(int.Parse)
                                       .ToList();

                if (IsReportSafeWithDampener(levels))
                {
                    safeReports++;
                }
            }

            return safeReports;
        }

        private static bool IsReportSafeWithDampener(List<int> originalLevels)
        {
            // First, check if the original report is safe without any removal
            if (IsReportStrictlySafe(originalLevels))
            {
                return true;
            }

            // If not, try removing one level at a time and check if the resulting report is safe
            for (int i = 0; i < originalLevels.Count; i++)
            {
                List<int> modifiedLevels = new List<int>(originalLevels);
                modifiedLevels.RemoveAt(i); // Remove the i-th element

                if (IsReportStrictlySafe(modifiedLevels))
                {
                    return true; // Found a way to make it safe by removing one level
                }
            }

            return false; // No way to make it safe
        }

        // This is the original IsReportSafe logic, renamed for clarity
        private static bool IsReportStrictlySafe(List<int> levels)
        {
            if (levels.Count <= 1)
            {
                // A report with 0 or 1 level is considered safe as there are no adjacent pairs to violate rules.
                return true;
            }

            // Determine initial trend
            int firstDiff = levels[1] - levels[0];
            if (firstDiff == 0 || Math.Abs(firstDiff) > 3)
            {
                return false; // Violates difference constraint or is static
            }

            bool isIncreasing = firstDiff > 0;

            // Check remaining adjacent levels
            for (int i = 1; i < levels.Count; i++)
            {
                int currentLevel = levels[i];
                int previousLevel = levels[i - 1];
                int diff = currentLevel - previousLevel;

                // Check difference constraint
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                {
                    return false;
                }

                // Check monotonicity
                if (isIncreasing)
                {
                    if (diff <= 0) // Should be increasing
                    {
                        return false;
                    }
                }
                else // isDecreasing
                {
                    if (diff >= 0) // Should be decreasing
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
