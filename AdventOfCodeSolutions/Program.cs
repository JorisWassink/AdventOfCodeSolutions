using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCodeSolutions
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = "input.txt";
            var examples = "example.txt";
            SolveYear(2024, inputs);
        }

        private static void SolveDay(int year, int day, string inputFile)
        {
            string baseFolder = Path.Combine(Environment.CurrentDirectory, "InputFiles", year.ToString());
            string inputFolder = Path.Combine(baseFolder, $"Day{day}");
            string inputPath = Path.Combine(inputFolder, inputFile);

            if (!Directory.Exists(inputFolder) || !File.Exists(inputPath))
            {
                Console.WriteLine($"Skipping Day{day} of {year}: folder or input.txt not found.");
                return;
            }

            Console.WriteLine($"~~{year}: Day {day}~~");

            string ns = $"AdventOfCodeSolutions._{year}.Day{day}";
            string className = $"{ns}.Day{day}Solution";
            string assemblyQualifiedName = $"{className}, {Assembly.GetExecutingAssembly().GetName().Name}";

            Type type = Type.GetType(assemblyQualifiedName);
            if (type == null)
            {
                Console.WriteLine($"Class {className} not found.");
                return;
            }

            MethodInfo method = type.GetMethod("Solution", BindingFlags.Public | BindingFlags.Static);
            if (method == null)
            {
                Console.WriteLine($"Method 'Solution' not found in {className}.");
                return;
            }

            method.Invoke(null, new object[] { inputPath });
        }

        private static void SolveYear(int year, string inputFiles)
        {
            string baseFolder = Path.Combine(Environment.CurrentDirectory, "InputFiles", year.ToString());
            if (!Directory.Exists(baseFolder))
            {
                Console.WriteLine($"Folder '{baseFolder}' not found!");
                return;
            }

            var dayFolders = Directory.GetDirectories(baseFolder, "Day*");

            foreach (var folder in dayFolders)
            {
                string folderName = Path.GetFileName(folder);
                if (!folderName.StartsWith("Day", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (!int.TryParse(folderName.Substring(3), out int day))
                    continue;

                SolveDay(year, day, inputFiles);
            }
        }
    }
}
