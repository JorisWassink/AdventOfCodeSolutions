using System.Numerics;

namespace Day6Solution
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    enum FloorTypes
    {
        HaveWalked,
        HaveNotWalked,
        Obstacle
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
            var map = new List<List<FloorTypes>>();
            var currentPosition = Vector2.Zero;
            var sr = new StreamReader(path);
            var lineCount = 0;
            
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var lineList = new List<FloorTypes>();
                
                if (line == null) continue;
                
                for (var i = 0; i < line.Length; i++)
                {
                    switch (line[i].ToString())
                    {
                        case ".":
                            lineList.Add(FloorTypes.HaveNotWalked);
                            break;
                        case "#":
                            lineList.Add(FloorTypes.Obstacle);
                            break;
                        case "^":
                            currentPosition = new Vector2(lineCount, i);
                            lineList.Add(FloorTypes.HaveWalked);
                            break;
                        default:
                            throw new Exception($"Unknown char: {line[i]}");
                    }
                }

                lineCount++;

                map.Add(lineList);
            }
            
            var amountToWalk = SimulateGuard(map, currentPosition);
            
            Console.WriteLine("Total amount: " + amountToWalk);
        }


        private static int SimulateGuard(List<List<FloorTypes>> map, Vector2 currentPosition)
        {
            Vector2 directionVector = new Vector2(-1, 0);
            var amountWalked = 1;
            while (true)
            {
                Vector2 posToCheck = currentPosition + directionVector;
                if (posToCheck.X < 0 || posToCheck.X > map.Count - 1 || posToCheck.Y < 0 || posToCheck.Y >= map[0].Count)
                    break;
                
                //check position
                var floorTypeAtPos = map[(int)posToCheck.X][(int)posToCheck.Y];
                
                switch (floorTypeAtPos)
                {
                    case FloorTypes.HaveNotWalked:
                        amountWalked++;
                        currentPosition = posToCheck;
                        map[(int)posToCheck.X][(int)posToCheck.Y] = FloorTypes.HaveWalked;
                        break;
                    case FloorTypes.HaveWalked:
                        currentPosition = posToCheck;
                        break;
                    case FloorTypes.Obstacle:
                        //rotate the vector2 90 degrees
                        directionVector.X *= -1;
                        (directionVector.X, directionVector.Y) = (directionVector.Y, directionVector.X);
                        break;
                    default:
                        Console.WriteLine($"already been here!");
                        break;
                }
            }
            return amountWalked;
        }
    }
}