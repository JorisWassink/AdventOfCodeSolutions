using System.Numerics;
using System.Text.RegularExpressions;

namespace Day3Solution
{
    using System.IO;

    internal static class Program
    {
        static void Main(string[] args)
        {
            Solution2.solution("input.txt");
        }
    }
    internal class Solution1
    {
        public static void solution1(string path)
        {
            try
            {
                var sr = new StreamReader(path);
                
                var line = sr.ReadToEnd();
                if (line == null) throw new FileNotFoundException();
                Console.WriteLine(CalculateMultiplications(line));
            }
            catch(Exception e)
            {
                //just in case stuff goes wrong
                Console.WriteLine("Exception: " + e.Message);
            }
        }


        private static int CalculateMultiplications(string input)
        {
            int totalValue = 0;
            var matches = GetGoodData(input);
            foreach (Match match in matches)
            {
                var numbers = GetNumbers(match);
                totalValue += Mul(numbers);
            }
            return totalValue;
        }

        private static MatchCollection GetGoodData(string input)
        {
            var rg = new Regex(@"mul\(\d*,\d*\)");
            var matches = rg.Matches(input);
            return matches;
        }

        private static Vector2 GetNumbers(Match match)
        {
            var parts = match.Value.Substring(4, match.Value.Length - 5).Split(',');
            var first = int.Parse(parts[0]);
            var second = int.Parse(parts[1]);
            return new Vector2(first, second);
        }

        private static int Mul(Vector2 numbers)
        {
            return (int)numbers.X * (int)numbers.Y;
        }
    }

    internal class Solution2
    {
        public static void solution(string path)
        {
            try
            {
                var sr = new StreamReader(path);
                
                var line = sr.ReadToEnd();
                if (line == null) throw new FileNotFoundException();
                Console.WriteLine(CalculateMultiplications(line));
            }
            catch(Exception e)
            {
                //just in case stuff goes wrong
                Console.WriteLine("Exception: " + e.Message);
            }
        }


        private static int CalculateMultiplications(string input)
        {
            var matchCollection = GetGoodData(input);
            return GetTotalValue(matchCollection);
        }

        private static MatchCollection GetGoodData(string input)
        {
            var rg = new Regex(@"(mul\(\d*,\d*\)|do\(\)|don\'t\(\))");
            var matches = rg.Matches(input);
            return matches;
        }

        private static int GetTotalValue(MatchCollection matches)
        {
            var totalValue = 0;
            var instructionEnabled = true;
            foreach (Match match in matches)
            {
                switch (match.Value)
                {
                    case "do()":
                        instructionEnabled = true;
                        break;
                    case "don't()":
                        instructionEnabled = false;
                        break;
                    default:
                        if (instructionEnabled)
                        {
                            var numbers = GetNumbers(match.Value);
                            totalValue += Mul(numbers);
                        }
                        break;
                }
            }
            return totalValue;
        }

        private static Vector2 GetNumbers(string match)
        {
            var parts = match.Substring(4, match.Length - 5).Split(',');
            var first = int.Parse(parts[0]);
            var second = int.Parse(parts[1]);
            return new Vector2(first, second);
        }

        private static int Mul(Vector2 numbers)
        {
            return (int)numbers.X * (int)numbers.Y;
        }
    }
}