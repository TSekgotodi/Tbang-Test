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

               
                if (_currentNumbers.Count == 1)
                {
                    if (_currentNumbers[0] == _targetTestValue)
                    {
                        totalCalibrationResult += _targetTestValue;
                    }
                    continue; 
                }

                
                if (CanSolve(0, _currentNumbers[0]))
                {
                    totalCalibrationResult += _targetTestValue;
                }
            }

            return totalCalibrationResult;
        }

       
        private static bool CanSolve(int currentIndex, long currentResult)
        {
           
            if (currentIndex == _currentNumbers.Count - 1)
            {
                return currentResult == _targetTestValue;
            }

            long nextNumber = _currentNumbers[currentIndex + 1];

            
            if (CanSolve(currentIndex + 1, currentResult + nextNumber))
            {
                return true;
            }

            
            if (CanSolve(currentIndex + 1, currentResult * nextNumber))
            {
                return true;
            }

           
            string currentStr = currentResult.ToString();
            string nextStr = nextNumber.ToString();
            string concatenatedStr = currentStr + nextStr;

           
            if (concatenatedStr.Length <= 19) 
            {
                if (long.TryParse(concatenatedStr, out long concatenatedNum))
                {
                    if (CanSolve(currentIndex + 1, concatenatedNum))
                    {
                        return true;
                    }
                }
            }

            
            return false;
        }
    }
}
