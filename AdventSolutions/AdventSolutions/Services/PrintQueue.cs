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

            // Parse rules
            List<(int before, int after)> rules = new List<(int before, int after)>();
            foreach (var ruleLine in sections[0].Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = ruleLine.Split('|');
                rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }

            // Parse updates
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
                // Only process incorrectly ordered updates
                if (!IsValidOrder(updateList, rules))
                {
                    List<int> sortedUpdate = TopologicalSort(updateList, rules);

                    // For an update list of N pages, the middle page is at index N / 2 (integer division)
                    // The problem states that updates will always have an odd number of pages,
                    // so N/2 will correctly give the middle index.
                    int middlePageIndex = sortedUpdate.Count / 2;
                    totalMiddlePagesSum += sortedUpdate[middlePageIndex];
                }
            }

            return totalMiddlePagesSum;
        }

        // This function from Part 1 remains the same
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
        // New function for Topological Sort
        private static List<int> TopologicalSort(List<int> pagesInUpdate, List<(int before, int after)> allRules)
        {
            // 1. Build the graph and calculate in-degrees
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            Dictionary<int, int> inDegree = new Dictionary<int, int>();

            // Initialize in-degrees for all pages in the current update to 0
            foreach (int page in pagesInUpdate)
            {
                graph[page] = new List<int>(); // Initialize adjacency list for all pages
                inDegree[page] = 0;
            }

            // Apply relevant rules to build the graph and update in-degrees
            foreach (var rule in allRules)
            {
                int beforePage = rule.before;
                int afterPage = rule.after;

                // Only consider rules where both pages are present in the current update
                if (pagesInUpdate.Contains(beforePage) && pagesInUpdate.Contains(afterPage))
                {
                    // Add edge: beforePage -> afterPage
                    graph[beforePage].Add(afterPage);
                    inDegree[afterPage]++;
                }
            }

            // 2. Initialize queue with pages having in-degree 0
            Queue<int> queue = new Queue<int>();
            foreach (int page in pagesInUpdate)
            {
                if (inDegree[page] == 0)
                {
                    queue.Enqueue(page);
                }
            }

            // 3. Perform topological sort
            List<int> sortedOrder = new List<int>();
            while (queue.Count > 0)
            {
                int currentPage = queue.Dequeue();
                sortedOrder.Add(currentPage);

                // For each neighbor of the current page
                foreach (int neighbor in graph[currentPage])
                {
                    inDegree[neighbor]--; // Decrement neighbor's in-degree
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor); // If in-degree becomes 0, add to queue
                    }
                }
            }

            // Basic check for cycles (though problem implies valid sorts are always possible)
            // If sortedOrder.Count != pagesInUpdate.Count, there was a cycle.
            // For this problem, it's generally safe to assume no cycles are formed by the valid rules.

            return sortedOrder;
        }
    }
}
