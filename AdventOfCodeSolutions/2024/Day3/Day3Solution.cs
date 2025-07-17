using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCodeSolutions._2024.Day3
{
    public class Day3Solution
    {
        public static void Solution(string path)
        {
            var part1Result = Part1Solution(path);
            var part2Result = Part2Solution(path);
            Console.WriteLine($"Part 1 result: {part1Result}");
            Console.WriteLine($"Part 2 result: {part2Result}");
        }

        private static int Part1Solution(string path)
        {
            try
            {
                var sr = new StreamReader(path);

                var line = sr.ReadToEnd();
                if (line == null) throw new FileNotFoundException();
                var result = SolvePart2(line);
                return result;
            }
            catch (Exception e)
            {
                //just in case stuff goes wrong
                Console.WriteLine("Exception: " + e.Message);
            }
            return 0;
        }
        
        private static int Part2Solution(string path)
        {
            try
            {
                var sr = new StreamReader(path);
                
                var line = sr.ReadToEnd();
                if (line == null) throw new FileNotFoundException();
                var result = SolvePart2(line);
                return result;
            }
            catch(Exception e)
            {
                //just in case stuff goes wrong
                Console.WriteLine("Exception: " + e.Message);
            }
            return 0;
        }



        private static int SolvePart1(string input)
        {
            var totalValue = 0;
            var matches = GetMulOrSwitchMatches(input);
            foreach (Match match in matches)
            {
                var numbers = GetNumbers(match);
                totalValue += Mul(numbers);
            }

            return totalValue;
        }

        private static MatchCollection GetMulMatches(string input)
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


        
        

        private static int SolvePart2(string input)
        {
            var matchCollection = GetMulMatches(input);
            return GetTotalValue(matchCollection);
        }

        private static MatchCollection GetMulOrSwitchMatches(string input)
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

