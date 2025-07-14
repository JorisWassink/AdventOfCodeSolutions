using System.Numerics;

namespace Day4Solution
{
    using System.IO;

    public enum direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_LEFT,
        UP_RIGHT,
        DOWN_RIGHT,
        DOWN_LEFT,
    }

    internal static class Program
    {
        static void Main(string[] args)
        {
            Solution.solution("input.txt");
        }
    }

    internal class Solution
    {
        public static void solution(string path)
        {
            var amountOfXmas = 0;
            var amountOfCrossMas = 0;
            
            var lines = File.ReadAllLines(path);
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var currentPosition = new Vector2(i, j);
                    var CanMakeCross = SolveCrossMasPuzzle("MAS", lines, currentPosition);
                    if (CanMakeCross)
                        amountOfCrossMas++;
                    
                    var amountOfXmasFromChar = SolveXmasPuzzle("XMAS", lines, currentPosition);
                    amountOfXmas += amountOfXmasFromChar;
                }
            }
            Console.WriteLine($"amount of Xmas' found: {amountOfXmas}");
            Console.WriteLine($"amount of Cross-Mas' found: {amountOfCrossMas}");
        }
        
        private static bool SolveCrossMasPuzzle(string goal, string[] lines, Vector2 position)
        {
            if (goal.Length != 3) throw new IndexOutOfRangeException();
            
            var currentLine = (int)position.X;
            var currentChar = (int)position.Y;
            if (lines[currentLine][currentChar] != goal[goal.Length / 2]) return false;
            
            int amount = 0;
            var availableDirections = new List<direction> { direction.UP_LEFT, direction.UP_RIGHT };
            
          
                foreach (var direction in availableDirections.ToList())
                {
                    Vector2 nextPosition;
                    Vector2 oppositePosition;
                    switch (direction)
                    {
                        case direction.UP_LEFT:
                            nextPosition = new Vector2(currentLine - 1, currentChar - 1);
                            oppositePosition = new Vector2(currentLine + 1, currentChar + 1);
                            break;
                        case direction.UP_RIGHT:
                            nextPosition = new Vector2(currentLine - 1, currentChar + 1);
                            oppositePosition = new Vector2(currentLine + 1, currentChar - 1);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (!(nextPosition.X >= 0 && nextPosition.Y >= 0 && nextPosition.X < lines.Length  &&
                          nextPosition.Y < lines.Length))
                    {
                        continue;
                    }
                    
                    if (!(oppositePosition.X >= 0 && oppositePosition.Y >= 0 && oppositePosition.X < lines.Length  &&
                          oppositePosition.Y < lines.Length))
                    {
                        continue;
                    }

                    var nextChar = lines[(int)nextPosition.X][(int)nextPosition.Y];
                    var oppositeChar = lines[(int)oppositePosition.X][(int)oppositePosition.Y];
                    
                    if ((nextChar == goal[0] && oppositeChar == goal[goal.Length - 1]) || ((oppositeChar == goal[0] && nextChar == goal[goal.Length - 1])))
                    {
                        amount += 1;
                    }
                }
            
            if (amount == 2)
                return true;
            return false;
        }

        private static int SolveXmasPuzzle(string goal, string[] lines, Vector2 position)
        {
            var currentLine = (int)position.X;
            var currentChar = (int)position.Y;
            if (lines[currentLine][currentChar] != goal[0]) return 0;
            int amount = 0;
            var availableDirections = new List<direction> { direction.UP, direction.DOWN, direction.LEFT, direction.RIGHT, direction.UP_LEFT, direction.UP_RIGHT, direction.DOWN_RIGHT, direction.DOWN_LEFT };
            for (int k = 1; k < goal.Length && availableDirections.Count >= 1; k++)
            {
                foreach (var direction in availableDirections.ToList())
                {
                    Vector2 nextPosition;
                    switch (direction)
                    {
                        case direction.UP:
                            nextPosition = new Vector2(currentLine - k, currentChar);
                            break;
                        case direction.DOWN:
                            nextPosition = new Vector2(currentLine + k, currentChar);
                            break;
                        case direction.LEFT:
                            nextPosition = new Vector2(currentLine, currentChar - k);
                            break;
                        case direction.RIGHT:
                            nextPosition = new Vector2(currentLine, currentChar + k);
                            break;
                        case direction.UP_LEFT:
                            nextPosition = new Vector2(currentLine - k, currentChar - k);
                            break;
                        case direction.DOWN_RIGHT:
                            nextPosition = new Vector2(currentLine + k, currentChar + k);
                            break;
                        case direction.DOWN_LEFT:
                            nextPosition = new Vector2(currentLine + k, currentChar - k);
                            break;
                        case direction.UP_RIGHT:
                            nextPosition = new Vector2(currentLine - k, currentChar + k);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (!(nextPosition.X >= 0 && nextPosition.Y >= 0 && nextPosition.X < lines.Length  &&
                          nextPosition.Y < lines.Length))
                    {
                        continue;
                    }

                    var nextChar = lines[(int)nextPosition.X][(int)nextPosition.Y]; 
                    if (nextChar != goal[k])
                    {
                        availableDirections.Remove(direction);
                        continue;
                    }

                    if (k == goal.Length - 1)
                    {
                        amount += 1;
                    }
                }
            }
            return amount;
        }
    }
}