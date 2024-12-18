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
            //Day01 day01 = new Day01();
            //day01.Setup();

            //string answer01One = day01.PartOne(day01.left, day01.right).ToString();
            //string answer01Two = day01.PartTwo(day01.left, day01.right).ToString();

            //Console.WriteLine("Day 01 - Part One: " + answer01One);
            //Console.WriteLine("Day 01 - Part Two: " + answer01Two);
            #endregion

            #region Day Two
            Day02 day02 = new Day02();
            day02.Setup();

            string answer02One = day02.PartOne(day02.numLists).ToString();
            //string answer02Two = day02.PartTwo(day01.left, day01.right).ToString();

            Console.WriteLine("Day 02 - Part One: " + answer02One);
            //Console.WriteLine("Day 01 - Part Two: " + answer02Two);
            #endregion

            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.ReadLine();
        }
    }
}
