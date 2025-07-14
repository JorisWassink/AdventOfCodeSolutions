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
            Solution.solution("input.txt", "XMAS");
        }
    }

    internal class Solution
    {
        public static void solution(string path, string goal)
        {
            var score = 0;
            var lines = File.ReadAllLines(path);
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var currentPosition = new Vector2(i, j);
                    var canFormWord = SolveXmasPuzzle(goal, lines, currentPosition);
                    score += canFormWord;
                }
            }
            Console.WriteLine($"{score}");
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