using AdventOfCode2024.Days;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("--------Advent of Code--------");
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            #region Day One
            Day01 day01 = new Day01();
            day01.Setup();
            string answerOne = day01.PartOne(day01.left, day01.right).ToString();
            string answerTwo = day01.PartTwo(day01.left, day01.right).ToString();

            Console.WriteLine("Day 01 - part one: " + answerOne);
            Console.WriteLine("Day 01 - part two: " + answerTwo);
            #endregion

            Console.WriteLine();
            Console.WriteLine("------------------------------");
        }
    }
}
