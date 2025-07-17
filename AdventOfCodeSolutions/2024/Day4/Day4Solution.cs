using System.Numerics;

namespace AdventOfCodeSolutions._2024.Day4
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownRight,
        DownLeft,
    }
    

    public class Day4Solution
    {
        public static void Solution(string path)
        {
            var result = Solve(path);
            Console.WriteLine($"Part 1 result: {result.X}");
            Console.WriteLine($"Part 2 result: {result.Y}");
        }
        
        private static Vector2 Solve(string path)
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
            var result = new Vector2(amountOfXmas, amountOfCrossMas);
            return result;
        }
        
        private static bool SolveCrossMasPuzzle(string goal, string[] lines, Vector2 position)
        {
            if (goal.Length != 3) throw new IndexOutOfRangeException();
            
            var currentLine = (int)position.X;
            var currentChar = (int)position.Y;
            if (lines[currentLine][currentChar] != goal[goal.Length / 2]) return false;
            
            int amount = 0;
            var availableDirections = new List<Direction> { Direction.UpLeft, Direction.UpRight };
            
          
                foreach (var direction in availableDirections.ToList())
                {
                    Vector2 nextPosition;
                    Vector2 oppositePosition;
                    switch (direction)
                    {
                        case Direction.UpLeft:
                            nextPosition = new Vector2(currentLine - 1, currentChar - 1);
                            oppositePosition = new Vector2(currentLine + 1, currentChar + 1);
                            break;
                        case Direction.UpRight:
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
            var availableDirections = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.UpLeft, Direction.UpRight, Direction.DownRight, Direction.DownLeft };
            for (int k = 1; k < goal.Length && availableDirections.Count >= 1; k++)
            {
                foreach (var direction in availableDirections.ToList())
                {
                    Vector2 nextPosition;
                    switch (direction)
                    {
                        case Direction.Up:
                            nextPosition = new Vector2(currentLine - k, currentChar);
                            break;
                        case Direction.Down:
                            nextPosition = new Vector2(currentLine + k, currentChar);
                            break;
                        case Direction.Left:
                            nextPosition = new Vector2(currentLine, currentChar - k);
                            break;
                        case Direction.Right:
                            nextPosition = new Vector2(currentLine, currentChar + k);
                            break;
                        case Direction.UpLeft:
                            nextPosition = new Vector2(currentLine - k, currentChar - k);
                            break;
                        case Direction.DownRight:
                            nextPosition = new Vector2(currentLine + k, currentChar + k);
                            break;
                        case Direction.DownLeft:
                            nextPosition = new Vector2(currentLine + k, currentChar - k);
                            break;
                        case Direction.UpRight:
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