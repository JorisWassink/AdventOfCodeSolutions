namespace Day1Solution
{
    using System.IO;
    using System.Collections.Generic;

    internal static class Program
    {
        static void Main(string[] args)
        {
            Solution.Part2Solution("input.txt");
        }
    }

    internal class Solution
    {
        public static void Part1Solution(string path)
        {
            var list1 = new List<int>();
            var list2 = new List<int>();
            var totalDifference = 0;
            
            try
            {
                var strings = File.ReadAllLines(path);
                foreach (var line in strings)
                {
                    var values = System.Text.RegularExpressions.Regex.Split(line,@"\s+");

                    list1.Add(int.Parse(values[0]));
                    list2.Add(int.Parse(values[1]));
                }
                
                list1.Sort();
                list2.Sort();

                if(list1.Count != list2.Count) throw new Exception("Invalid input.");

                
                for (var i = 0; i < list1.Count; i++)
                {
                    var diff = Math.Abs(list1[i] - list2[i]);
                    totalDifference += (diff);
                }
                
                Console.WriteLine($"Total difference: {totalDifference}");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        
        
        public static void Part2Solution(string path)
        {
            var list1 = new List<int>();
            var list2 = new List<int>();
            var dict = new Dictionary<int, int>();
            var similarityScore = 0;
            
            try
            {
                var strings = File.ReadAllLines(path);
                foreach (var line in strings)
                {
                    var values = System.Text.RegularExpressions.Regex.Split(line,@"\s+");

                    list1.Add(int.Parse(values[0]));
                    list2.Add(int.Parse(values[1]));
                }
                
                if(list1.Count != list2.Count) throw new Exception("Invalid input.");

                
                for (var i = 0; i < list1.Count; i++)
                {
                    if (dict.TryGetValue(list1[i], out int value))
                    {
                        similarityScore += (list1[i] * value);
                        continue;
                    }

                    var amount = 0;
                    for (var j = 0; j < list2.Count; j++)
                    {
                        if (list2[j] == list1[i])
                        {
                            amount++;
                        }
                    }
                    dict.Add(list1[i], amount);
                    similarityScore += (list1[i] * dict[list1[i]]);
                }
                
                Console.WriteLine($"score: {similarityScore}");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}