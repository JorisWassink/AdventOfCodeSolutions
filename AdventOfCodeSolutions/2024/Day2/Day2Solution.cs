using System.Numerics;

namespace AdventOfCodeSolutions._2024.Day2
{
    internal class Day2Solution
    {
        public static void Solution(string path)
        {
            var result = Solve(path);
            Console.WriteLine($"Part 1 result: {result.X}");
            Console.WriteLine($"Part 2 result: {result.Y}");
        }
        
        private static Vector2 Solve(string path)
        {
            var part1Result = 0;
            var part2Result = 0;
            
            try
            {
                var sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) continue;

                    var parts = line.Split().Select(s => int.Parse(s)).ToList();
                    if (IsSafe(parts))
                        part1Result++;
                    if(IsSafeWithDampener(parts))
                        part2Result++;
                }
                
                var result = new Vector2(part1Result, part2Result);
                return result;
                
            }
            catch(Exception e)
            {
                //just in case stuff goes wrong
                Console.WriteLine("Exception: " + e.Message);
            }
            
            return Vector2.Zero;
        }
        
        

        private static bool IsSafe(List<int> parts)
        {
            var increasing = false;

            //counting
            for (var i = 1; i < parts.Count; i++)
            {
                var part = parts[i];
                var previous = parts[i - 1];
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

        private static bool IsSafeWithDampener(List<int> parts)
        {
            var increasing = false;
            
            //counting
            for (var i = 0; i < parts.Count - 1; i++)
            {
                var part = parts[i];
                var next = parts[i + 1];
                var b = next > part;
                
                //setting the initial value
                if (i == 0)
                    increasing = b;
                
                var diff = Math.Abs(part - next);

                if ((b != increasing) ||(diff is < 1 or > 3))
                {
                    bool safe = false;
                    
                    var removedI = parts.Where((_, idx) => idx != i).ToList();
                    if (IsSafe(removedI)) return true;

                    // Try removing i+1
                    var removedNext = parts.Where((_, idx) => idx != i + 1).ToList();
                    if (IsSafe(removedNext)) return true;
                    
                    return safe;
                }
            }
            return true;
        }
    }
}