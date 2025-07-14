using System.Numerics;
using System.IO;
using System.Text.RegularExpressions;


namespace Day5Solution
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Solution.solution("input.txt");
        }
    }

    internal class Solution
    {
        private static readonly Regex ruleRegex = new Regex(@"\d*\|\d*");
        private static readonly Regex updateRegex = new Regex(@"(\d*\,)*\d*");
        public static void solution(string path)
        {
            var rules = new HashSet<Vector2>();
            var totalCorrectUpdateMiddles = 0;
            var totalIncorrectUpdateMiddles = 0;
            var sr = new StreamReader(path);
            while (sr.EndOfStream == false)
            {
                var line = sr.ReadLine();
                
                if (ruleRegex.IsMatch(line))
                    rules.Add(new Vector2(int.Parse(line.Split('|')[0]), int.Parse(line.Split('|')[1])));
                else if (updateRegex.IsMatch(line))
                {
                    var intList = GetIntList(line);
                    if (IsSafe(intList, rules))
                    {
                        var middle = intList[intList.Count / 2];
                        totalCorrectUpdateMiddles += middle;
                    }
                    else
                    {
                        var sortedUpdate = SortUpdate(intList, rules);
                        var middle = sortedUpdate[sortedUpdate.Count / 2];
                        totalIncorrectUpdateMiddles += middle;
                    }
                }
            }

            Console.WriteLine($"Solution Part 1: {totalCorrectUpdateMiddles}, Solution Part 2: {totalIncorrectUpdateMiddles}");
        }

        
        private static List<int> SortUpdate(List<int> updates, HashSet<Vector2> rules)
        {
            //imma bubble sort this :D
            while (true)
            {
                var hasSwapped = false;
                for (int i = 0; i < updates.Count - 1; i++)
                {
                    var pair = new Vector2(updates[i + 1], updates[i]);
                    if (ShouldSwap(pair, rules))
                    {
                        //swap em
                        (updates[i], updates[i + 1]) = (updates[i + 1], updates[i]);
                        hasSwapped = true;
                    }
                }
                if (!hasSwapped)
                    break;
            }
            return updates;
        }
        
        
        private static bool IsSafe(List<int> updates, HashSet<Vector2> rules)
        {
            if (updates.Count <= 0) return false;
            
            for (int i = 1; i < updates.Count; i++)
            {
                var pair = new Vector2(updates[i], updates[i - 1]);
                if (ShouldSwap(pair, rules))
                {
                    return false;
                }
            }
            
            return true;
        }

        private static List<int> GetIntList(string line)
        {
            var updates = new List<int>();
            var numbers  = line.Split(',');
            for (int i = 0; i < numbers.Length; i++)
            {
                updates.Add(int.TryParse(numbers[i], out var result) ? result : 0);
            }
            return updates;
        }
        private static bool ShouldSwap(Vector2 pair, HashSet<Vector2> rules)
        {
            if (rules.Contains(pair))
            {
                return true;
            }
            return false;
        }
    }
}