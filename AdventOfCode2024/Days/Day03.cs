using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day03
    {
        public List<string> Matches { get; set; } = new List<string>();

        public void SetupPartOne()
        {
            string filePath = @"Inputs\Day03.txt";
            var input = Path.Combine(AppContext.BaseDirectory, filePath);

            StreamReader sr = new StreamReader(input);
            string line = sr.ReadLine();

            while (line != null)
            {
                string pattern = @"mul\(\d+,\d+\)";

                MatchCollection matchCollection = Regex.Matches(line, pattern);

                foreach (Match match in matchCollection)
                {
                    Matches.Add(match.Value);
                }

                line = sr.ReadLine();
            }

            sr.Close();
        }

        public void SetupPartTwo()
        {
            string filePath = @"Inputs\Day03.txt";
            var input = Path.Combine(AppContext.BaseDirectory, filePath);

            StreamReader sr = new StreamReader(input);
            string line = sr.ReadLine();

            Matches.Clear();

            while (line != null)
            {
                string pattern = @"mul\(\d+,\d+\)|don\'t\(\)|do\(\)";

                MatchCollection matchCollection = Regex.Matches(line, pattern);

                foreach (Match match in matchCollection)
                {
                    Matches.Add(match.Value);
                }

                line = sr.ReadLine();
            }

            sr.Close();
        }

        public int PartOne(List<string> matches)
        {
            int result = 0;

            foreach (string match in matches)
            {
                List<int> numbers = ExtractNumbers(match);

                result += numbers[0] * numbers[1];
            }

            return result;
        }

        public int PartTwo(List<string> matches)
        {
            int result = 0;
            bool enabled = true;

            foreach (string match in matches)
            {
                if (match == @"don't()")
                {
                    enabled = false;
                }
                else if (match == @"do()")
                {
                    enabled = true;
                }

                if (enabled && match.Substring(0,3) == "mul")
                {
                    List<int> numbers = ExtractNumbers(match);

                    result += numbers[0] * numbers[1];
                }
            }

            return result;
        }

        private List<int> ExtractNumbers(string input)
        {
            List<int> numbers = new List<int>();

            Regex regex = new Regex(@"\d+");

            foreach (Match match in regex.Matches(input))
            {
                numbers.Add(int.Parse(match.Value));
            }

            return numbers;
        }
    }
}
