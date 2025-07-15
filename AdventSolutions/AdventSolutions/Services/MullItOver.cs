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

            // The regular expression pattern to match "mul(X,Y)"
            // explanation:
            // mul\(          - Matches the literal string "mul("
            // (\d{1,3})      - Captures the first number (X), which must be 1 to 3 digits
            // ,              - Matches the comma separator
            // (\d{1,3})      - Captures the second number (Y), which must be 1 to 3 digits
            // \)             - Matches the literal closing parenthesis ")"
            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

            // Use Regex.Matches to find all occurrences of the pattern in the input string
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                // Group 1 is the first captured number (X)
                int x = int.Parse(match.Groups[1].Value);
                // Group 2 is the second captured number (Y)
                int y = int.Parse(match.Groups[2].Value);

                totalSum += (long)x * y;
            }

            return totalSum;
        }
        public static long CalculateEnabledMultiplicationSum(string input)
        {
            long totalSum = 0;
            bool mulEnabled = true; // Initially, mul instructions are enabled

            // Define regex patterns for all instruction types
            string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            string doPattern = @"do\(\)";
            string dontPattern = @"don't\(\)";

            // Collect all matches with their indices
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

            // Sort instructions by their starting index to process them in order
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
