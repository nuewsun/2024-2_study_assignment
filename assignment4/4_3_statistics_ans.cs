using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };

            int stdCount = data.GetLength(0) - 1;
            double[] avgScores = new double[3];
            for (int i = 2; i < 5; i++)
            {
                avgScores[i - 2] = Enumerable.Range(1, stdCount)
                    .Select(j => double.Parse(data[j, i]))
                    .Average();
            }

            double[,] minMaxScores = new double[3, 2];
            for (int i = 2; i < 5; i++)
            {
                var scores = Enumerable.Range(1, stdCount)
                    .Select(j => double.Parse(data[j, i]));
                minMaxScores[i - 2, 0] = scores.Max();
                minMaxScores[i - 2, 1] = scores.Min();
            }

            var studentScores = Enumerable.Range(1, stdCount)
                .Select(i => new
                {
                    Name = data[i, 1],
                    TotalScore = Enumerable.Range(2, 3)
                        .Sum(j => double.Parse(data[i, j]))
                })
                .OrderByDescending(s => s.TotalScore)
                .Select((s, index) => new { s.Name, Rank = GetOrdinal(index + 1) })
                .ToList();

            Console.WriteLine("Average Scores:");
            Console.WriteLine($"Math: {avgScores[0]:F2}");
            Console.WriteLine($"Science: {avgScores[1]:F2}");
            Console.WriteLine($"English: {avgScores[2]:F2}");

            Console.WriteLine("\nMax and min Scores:");
            Console.WriteLine($"Math: ({minMaxScores[0, 0]}, {minMaxScores[0, 1]})");
            Console.WriteLine($"Science: ({minMaxScores[1, 0]}, {minMaxScores[1, 1]})");
            Console.WriteLine($"English: ({minMaxScores[2, 0]}, {minMaxScores[2, 1]})");

            Console.WriteLine("\nStudents rank by total scores:");
            foreach (var student in studentScores)
            {
                Console.WriteLine($"{student.Name}: {student.Rank}");
            }
        }

        static string GetOrdinal(int n)
        {
            if (n <= 0) return n.ToString();

            switch (n % 100)
            {
                case 11:
                case 12:
                case 13:
                    return n + "th";
            }

            switch (n % 10)
            {
                case 1:
                    return n + "st";
                case 2:
                    return n + "nd";
                case 3:
                    return n + "rd";
                default:
                    return n + "th";
            }
        }
    }
}
