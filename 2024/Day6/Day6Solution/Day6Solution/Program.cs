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
        public static List<List<FloorTypes>> map = new List<List<FloorTypes>>();
        public static void solution(string path)
        {
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
            
            var startDirection = new Vector2(-1, 0);
            var amountToWalk = GetAllLoopOptions(currentPosition, startDirection);
            
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

        private static int GetAllLoopOptions(Vector2 startPosition, Vector2 startDirection)
        {
            var stack = new Stack<(Vector2 position, Vector2 direction)>();
            var visited = new HashSet<(Vector2, Vector2)>();
            var totalAmount = 0;
            stack.Push((startPosition, startDirection));
            while (stack.Count > 0)
            {
                var (currentPosition, currentDirection) = stack.Pop();

                if (visited.Contains((currentPosition, currentDirection)))
                {
                    continue;
                }

                // 5. Mark the current state as visited.
                visited.Add((currentPosition, currentDirection));
                
                var posToCheck = currentPosition + currentDirection;
                if (IsOutOfBounds(posToCheck))
                    break;
                
                //check position
                var floorTypeAtPos = map[(int)posToCheck.X][(int)posToCheck.Y];
                
                switch (floorTypeAtPos)
                {
                    case FloorTypes.HaveNotWalked:
                        var passed = new HashSet<(Vector2, Vector2)>();
                        var isLoop = IsLoopDFS(startPosition, new Vector2(-1,0),posToCheck, passed);
                        if (isLoop) totalAmount += 1;
                        map[(int)posToCheck.X][(int)posToCheck.Y] = FloorTypes.HaveWalked;
                        stack.Push((posToCheck, currentDirection));
                        break;
                    case FloorTypes.HaveWalked:
                        stack.Push((posToCheck, currentDirection));
                        break;
                    case FloorTypes.Obstacle:
                        //rotate the vector2 90 degrees
                        currentDirection = new Vector2(currentDirection.Y, -currentDirection.X);
                        stack.Push((currentPosition, currentDirection));
                        break;
                    default:
                        Console.WriteLine($"already been here!");
                        break;
                }
                
            }
            return totalAmount;
        }
        
        
        private static bool IsLoopDFS(Vector2 position, Vector2 direction, Vector2 obstacle, HashSet<(Vector2, Vector2)> visited)
        {
            var isVisited = visited.Contains((position, direction));
            if (isVisited)
                return true; // cycle found

            visited.Add((position, direction));
            
            var next = position + direction;
            
            if (IsOutOfBounds(next))
                return false;

            if (!(IsObstacle(next) || next == (obstacle)))
                return IsLoopDFS(next, direction, obstacle, visited);
            
            var newDirection = new Vector2(direction.Y, -direction.X);
            return IsLoopDFS(position, newDirection, obstacle, visited);
            
        }

        private static bool IsObstacle(Vector2 position)
        {
            return map[(int)position.X][(int)position.Y] == FloorTypes.Obstacle;
        }

        private static bool IsOutOfBounds(Vector2 position)
        {
            return position.X < 0 || position.X > map.Count - 1 || position.Y < 0 || position.Y >= map[0].Count;
        }
    }
}