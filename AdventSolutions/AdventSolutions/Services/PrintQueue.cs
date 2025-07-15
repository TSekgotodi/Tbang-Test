using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventSolutions.Services
{
    public static class PrintQueue
    {
        public static long Solve(string input)
        {
            var sections = input.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

           
            List<(int before, int after)> rules = new List<(int before, int after)>();
            foreach (var ruleLine in sections[0].Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = ruleLine.Split('|');
                rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }

           
            List<List<int>> updates = new List<List<int>>();
            foreach (var updateLine in sections[1].Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                updates.Add(updateLine.Split(',')
                                      .Select(int.Parse)
                                      .ToList());
            }

            long totalMiddlePagesSum = 0;

            foreach (var updateList in updates)
            {
                
                if (!IsValidOrder(updateList, rules))
                {
                    List<int> sortedUpdate = TopologicalSort(updateList, rules);
                    int middlePageIndex = sortedUpdate.Count / 2;
                    totalMiddlePagesSum += sortedUpdate[middlePageIndex];
                }
            }

            return totalMiddlePagesSum;
        }

       
        private static bool IsValidOrder(List<int> updateList, List<(int before, int after)> allRules)
        {
            Dictionary<int, int> pagePositions = new Dictionary<int, int>();
            for (int i = 0; i < updateList.Count; i++)
            {
                pagePositions[updateList[i]] = i;
            }

            foreach (var rule in allRules)
            {
                int beforePage = rule.before;
                int afterPage = rule.after;

                bool beforePageExists = pagePositions.TryGetValue(beforePage, out int beforePageIndex);
                bool afterPageExists = pagePositions.TryGetValue(afterPage, out int afterPageIndex);

                if (beforePageExists && afterPageExists)
                {
                    if (beforePageIndex >= afterPageIndex)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
       
        private static List<int> TopologicalSort(List<int> pagesInUpdate, List<(int before, int after)> allRules)
        {
            
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            Dictionary<int, int> inDegree = new Dictionary<int, int>();

           
            foreach (int page in pagesInUpdate)
            {
                graph[page] = new List<int>(); 
                inDegree[page] = 0;
            }

            
            foreach (var rule in allRules)
            {
                int beforePage = rule.before;
                int afterPage = rule.after;

                
                if (pagesInUpdate.Contains(beforePage) && pagesInUpdate.Contains(afterPage))
                {
                    
                    graph[beforePage].Add(afterPage);
                    inDegree[afterPage]++;
                }
            }

           
            Queue<int> queue = new Queue<int>();
            foreach (int page in pagesInUpdate)
            {
                if (inDegree[page] == 0)
                {
                    queue.Enqueue(page);
                }
            }

           
            List<int> sortedOrder = new List<int>();
            while (queue.Count > 0)
            {
                int currentPage = queue.Dequeue();
                sortedOrder.Add(currentPage);

               
                foreach (int neighbor in graph[currentPage])
                {
                    inDegree[neighbor]--; 
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor); 
                    }
                }
            }

            

            return sortedOrder;
        }
    }
}
