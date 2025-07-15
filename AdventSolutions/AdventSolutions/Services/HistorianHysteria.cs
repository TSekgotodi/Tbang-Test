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

           
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
               
                string[] numbers = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length == 2)
                {
                    leftList.Add(int.Parse(numbers[0]));
                    rightList.Add(int.Parse(numbers[1]));
                }
            }

           
            leftList.Sort();
            rightList.Sort();

            long totalDistance = 0;

           
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

           
            string[] lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                
                string[] numbers = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length == 2)
                {
                    leftList.Add(int.Parse(numbers[0]));
                    rightList.Add(int.Parse(numbers[1]));
                }
            }

            long similarityScore = 0;

           
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

           
            foreach (int leftNum in leftList)
            {
                if (rightListCounts.ContainsKey(leftNum))
                {
                    similarityScore += (long)leftNum * rightListCounts[leftNum];
                }
               
            }

            return similarityScore;
        }
    }
}
