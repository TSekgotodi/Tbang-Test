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
                
                return true;
            }

           
            int firstDiff = levels[1] - levels[0];
            if (firstDiff == 0 || Math.Abs(firstDiff) > 3)
            {
                return false; 
            }

            bool isIncreasing = firstDiff > 0;

            
            for (int i = 1; i < levels.Count; i++)
            {
                int currentLevel = levels[i];
                int previousLevel = levels[i - 1];
                int diff = currentLevel - previousLevel;

               
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                {
                    return false;
                }

                
                if (isIncreasing)
                {
                    if (diff <= 0) 
                    {
                        return false;
                    }
                }
                else 
                {
                    if (diff >= 0) 
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
           
            if (IsReportStrictlySafe(originalLevels))
            {
                return true;
            }

           
            for (int i = 0; i < originalLevels.Count; i++)
            {
                List<int> modifiedLevels = new List<int>(originalLevels);
                modifiedLevels.RemoveAt(i); 

                if (IsReportStrictlySafe(modifiedLevels))
                {
                    return true; 
                }
            }

            return false; 
        }

       
        private static bool IsReportStrictlySafe(List<int> levels)
        {
            if (levels.Count <= 1)
            {
               
                return true;
            }

         
            int firstDiff = levels[1] - levels[0];
            if (firstDiff == 0 || Math.Abs(firstDiff) > 3)
            {
                return false; 
            }

            bool isIncreasing = firstDiff > 0;

           
            for (int i = 1; i < levels.Count; i++)
            {
                int currentLevel = levels[i];
                int previousLevel = levels[i - 1];
                int diff = currentLevel - previousLevel;

               
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                {
                    return false;
                }

               
                if (isIncreasing)
                {
                    if (diff <= 0) 
                    {
                        return false;
                    }
                }
                else 
                {
                    if (diff >= 0) 
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
