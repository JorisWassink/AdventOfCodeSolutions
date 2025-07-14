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
            int total = 0;
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
                        Console.WriteLine(middle);
                        total += middle;
                    }
                }
            }

            Console.WriteLine(total);
        }

        private static bool IsSafe(List<int> updates, HashSet<Vector2> rules)
        {
            if (updates.Count <= 0) return false;
            
            Console.WriteLine();
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