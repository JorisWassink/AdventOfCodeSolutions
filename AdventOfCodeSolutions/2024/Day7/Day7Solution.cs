using System.Numerics;

namespace AdventOfCodeSolutions._2024.Day7
{
    internal class Day7Solution
    {

        public static void Solution(string path)
        {
            var result = Solve(path);
            Console.WriteLine($"Part 1 result: {result.X}");
            Console.WriteLine($"Part 2 result: {result.Y}");
        }

   
        private static Vector2 Solve(string path)
        {
            var sr = new StreamReader(path);
            long total = 0;
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var l = line.Split(':');
                var goal = Int64.Parse(l[0]);
                var numbers = l[1].Split(' ').ToList();
                numbers.RemoveAt(0);
                int[] numberArray = new int[numbers.Count];
                
                for (var i = 0; i < numbers.Count; i++)
                {
                    numberArray[i] = int.TryParse(numbers[i], out var r) ? r : 0;
                }


                total += Solve(goal, numberArray);
            }
            var result = new Vector2(total, 0);
            return result;
        }

        private static long Solve(long result, int[] numbers)
        {
            //Console.WriteLine();
            var currentResult = 0;
            var operations = Enumerable.Repeat(1, numbers.Length - 1).ToArray();
            var total = 1 << operations.Length;
            for (var i = 0; i < total; i++)
            {
                var tempArray = new int[operations.Length];
                
                for (int j = operations.Length - 1; j >= 0; j--)
                {
                    tempArray[j] = (i >> j) & 1; // fill with 0s and 1s based on bitmask
                    //Console.Write(tempArray[j] + ", ");
                }
                //Console.WriteLine();

                var tempResult = Calculate(numbers, tempArray);
                //Console.WriteLine(tempResult);
                if (tempResult == result)
                {
                    return result;
                }                
            }
            
            return 0;
        }

        private static int Calculate(int[] numbers, int[] operations)
        {
            var result = numbers[0];
            //Console.Write(numbers[0]);

            for (int i = 1; i < numbers.Length; i++)
            {
                var number = numbers[i];
                var operation = operations[i - 1];
                switch (operation)
                {
                    case 0:
                        result += number;
                        //Console.Write("+");
                        break;
                    case 1:
                        result *= number;
                        //Console.Write("*");
                        break;
                    default:
                        throw new Exception("Invalid operation");
                }
                //Console.Write(numbers[i]);

            }
            //Console.Write(" = {0} \n", result);
            return result;
        }
    }
}