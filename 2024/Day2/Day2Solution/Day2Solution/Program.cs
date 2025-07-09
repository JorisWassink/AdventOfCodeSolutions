namespace Day2Solution
{
    using System.IO;

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
            var safeAmount = 0;
            
            try
            {
                var sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) continue;
                    
                    var parts = line.Split();
                    if (IsSafe(parts))
                    {
                        safeAmount++;
                    }
                }
                Console.WriteLine(safeAmount);
                
            }
            catch(Exception e)
            {
                //just in case stuff goes wrong
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private static bool IsSafe(string[] parts)
        {
            var increasing = false;

            //counting
            for (var i = 1; i < parts.Length; i++)
            {
                var part = int.Parse(parts[i]);
                var previous = int.Parse(parts[i - 1]);
                var b = part > previous;
                
                //setting the initial value
                if (i == 1)
                    increasing = b;
                
                var diff = Math.Abs(previous - part);
                if ((b != increasing) || diff is < 1 or > 3)
                {
                    return false;
                }
            }
            return true;
        }
    }
}