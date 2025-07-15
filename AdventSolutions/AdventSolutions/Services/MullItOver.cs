using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventSolutions.Services
{
    public static class MullItOver
    {
        public static long CalculateMultiplicationSum(string input)
        {
            long totalSum = 0;

           
            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

           
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
               
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);

                totalSum += (long)x * y;
            }

            return totalSum;
        }
        public static long CalculateEnabledMultiplicationSum(string input)
        {
            long totalSum = 0;
            bool mulEnabled = true; 

           
            string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            string doPattern = @"do\(\)";
            string dontPattern = @"don't\(\)";

           
            var instructions = new List<(int index, string type, Match match)>();

            foreach (Match match in Regex.Matches(input, mulPattern))
            {
                instructions.Add((match.Index, "mul", match));
            }
            foreach (Match match in Regex.Matches(input, doPattern))
            {
                instructions.Add((match.Index, "do", match));
            }
            foreach (Match match in Regex.Matches(input, dontPattern))
            {
                instructions.Add((match.Index, "don't", match));
            }

           
            instructions = instructions.OrderBy(i => i.index).ToList();

            foreach (var instruction in instructions)
            {
                switch (instruction.type)
                {
                    case "do":
                        mulEnabled = true;
                        break;
                    case "don't":
                        mulEnabled = false;
                        break;
                    case "mul":
                        if (mulEnabled)
                        {
                            int x = int.Parse(instruction.match.Groups[1].Value);
                            int y = int.Parse(instruction.match.Groups[2].Value);
                            totalSum += (long)x * y;
                        }
                        break;
                }
            }

            return totalSum;
        }
    }
}
