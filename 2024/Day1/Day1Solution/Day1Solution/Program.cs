namespace Day1Solution
{
    using System.IO;
    using System.Collections.Generic;

    internal static class Program
    {
        static void Main(string[] args)
        {
            Solution.Part1Solution("input.txt");
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
    }
}