using System.Numerics;

namespace AdventOfCodeSolutions._2024.Day6
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum FloorTypes
    {
        HaveWalked,
        HaveNotWalked,
        Obstacle
    }
    


    public static class Day6Solution
    {
        public static List<List<FloorTypes>> map = new List<List<FloorTypes>>();

        public static void Solution(string path)
        {
            var result = Solve(path);
            Console.WriteLine($"Part 1 result: {result.X}");
            Console.WriteLine($"Part 2 result: {result.Y}");
        }
        
        private static Vector2 Solve(string path)
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
            
            
            var amountToWalk = SimulateGuard(currentPosition);
            var amountOfObstacleOptions = GetAllLoopOptions(currentPosition, startDirection);
            
            var result = new Vector2(amountToWalk, amountOfObstacleOptions);
            return result;
        }


        private static int SimulateGuard(Vector2 currentPosition)
        {
            List<List<FloorTypes>> copiedMap = new List<List<FloorTypes>>(map.Count);

            foreach (var innerList in map)
            {
                // Create a new list with copied elements of each inner list
                copiedMap.Add(new List<FloorTypes>(innerList));
            } 
            
            Vector2 directionVector = new Vector2(-1, 0);
            var amountWalked = 1;
            while (true)
            {
                Vector2 posToCheck = currentPosition + directionVector;
                if (posToCheck.X < 0 || posToCheck.X > copiedMap.Count - 1 || posToCheck.Y < 0 || posToCheck.Y >= copiedMap[0].Count)
                    break;
                
                //check position
                var floorTypeAtPos = copiedMap[(int)posToCheck.X][(int)posToCheck.Y];
                
                switch (floorTypeAtPos)
                {
                    case FloorTypes.HaveNotWalked:
                        amountWalked++;
                        currentPosition = posToCheck;
                        copiedMap[(int)posToCheck.X][(int)posToCheck.Y] = FloorTypes.HaveWalked;
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
            List<List<FloorTypes>> copiedMap = new List<List<FloorTypes>>(map.Count);

            foreach (var innerList in map)
            {
                // Create a new list with copied elements of each inner list
                copiedMap.Add(new List<FloorTypes>(innerList));
            } 
            
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
                visited.Add((currentPosition, currentDirection));
                
                var posToCheck = currentPosition + currentDirection;
                if (IsOutOfBounds(posToCheck))
                    break;
                
                var floorTypeAtPos = copiedMap[(int)posToCheck.X][(int)posToCheck.Y];
                
                switch (floorTypeAtPos)
                {
                    case FloorTypes.HaveNotWalked:
                        var passed = new HashSet<(Vector2, Vector2)>();
                        var isLoop = IsLoopDFS(startPosition, new Vector2(-1,0),posToCheck, passed);
                        if (isLoop) totalAmount += 1;
                        copiedMap[(int)posToCheck.X][(int)posToCheck.Y] = FloorTypes.HaveWalked;
                        stack.Push((posToCheck, currentDirection));
                        break;
                    case FloorTypes.HaveWalked:
                        stack.Push((posToCheck, currentDirection));
                        break;
                    case FloorTypes.Obstacle:
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
                return true; 

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