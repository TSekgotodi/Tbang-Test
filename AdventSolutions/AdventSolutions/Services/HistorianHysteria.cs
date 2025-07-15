using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventSolutions.Services
{
    public static class HistorianHysteria
    {
        public static long CalculateTotalDistance(string input)
        {
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            // Split the input into lines
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                // Split each line by whitespace to get the two numbers
                string[] numbers = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length == 2)
                {
                    leftList.Add(int.Parse(numbers[0]));
                    rightList.Add(int.Parse(numbers[1]));
                }
            }

            // Sort both lists
            leftList.Sort();
            rightList.Sort();

            long totalDistance = 0;

            // Calculate the sum of absolute differences
            for (int i = 0; i < leftList.Count; i++)
            {
                totalDistance += Math.Abs(leftList[i] - rightList[i]);
            }

            return totalDistance;
        }

        public static long CalculateSimilarityScore(string input)
        {
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            // Split the input into lines
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                // Split each line by whitespace to get the two numbers
                string[] numbers = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length == 2)
                {
                    leftList.Add(int.Parse(numbers[0]));
                    rightList.Add(int.Parse(numbers[1]));
                }
            }

            long similarityScore = 0;

            // Use a dictionary to efficiently count occurrences in the right list
            // This avoids repeatedly iterating through the right list for each number in the left list.
            Dictionary<int, int> rightListCounts = new Dictionary<int, int>();
            foreach (int num in rightList)
            {
                if (rightListCounts.ContainsKey(num))
                {
                    rightListCounts[num]++;
                }
                else
                {
                    rightListCounts.Add(num, 1);
                }
            }

            // Calculate the similarity score
            foreach (int leftNum in leftList)
            {
                if (rightListCounts.ContainsKey(leftNum))
                {
                    similarityScore += (long)leftNum * rightListCounts[leftNum];
                }
                // If the number from the left list doesn't exist in the right list, its count is 0,
                // so leftNum * 0 contributes 0 to the similarity score, as per the problem description.
            }

            return similarityScore;
        }
    }
}
