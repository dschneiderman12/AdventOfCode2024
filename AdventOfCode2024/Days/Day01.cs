using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day01
    {
        public List<int> left { get; set; } = new List<int>();
        public List<int> right { get; set; } = new List<int>();

        public void Setup()
        {
            string filePath = @"Inputs\Day01.txt";
            var input = Path.Combine(AppContext.BaseDirectory, filePath);

            StreamReader sr = new StreamReader(input);
            string line = sr.ReadLine();

            while (line != null)
            {
                string[] split = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                left.Add(int.Parse(split[0]));
                right.Add(int.Parse(split[1]));

                line = sr.ReadLine();
            }

            sr.Close();
        }

        public int PartOne(List<int> left, List<int> right)
        {
            int result = 0;

            left.Sort();
            right.Sort();

            for (int i = 0; i < left.Count; i++)
            {
                int diff = left[i] - right[i];

                result += Math.Abs(diff);
            }

            return result;
        }

        public int PartTwo(List<int> left, List<int> right)
        {
            int result = 0;

            foreach (int i in left)
            {
                int count = 0;

                for (int j = 0; j < right.Count; j++)
                {
                    if (i == right[j])
                        count++;
                }

                result += i * count;
            }

            return result;
        }
    }
}
