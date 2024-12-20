using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day04
    {
        public List<char[]> matrix = new List<char[]>();
        private int[] currentPosition = [2];

        public void Setup()
        {
            string filePath = @"Inputs\Day04.txt";
            var lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                matrix.Add(line.ToCharArray());
            }
        }

        public int PartOne(List<char[]> matrix)
        {
            int xmasCount = 0;

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    int letterCheck = 0;
                    var currentLetter = matrix[i][j];
                    var currentPosition = (i, j);
                    Direction currentDirection = Direction.Up;

                    while (letterCheck <= 3 && checkNextLetter(currentLetter, letterCheck))
                    {
                        var position = nextPosition(currentPosition, Direction.Up);
                        currentLetter = matrix[position.nextRow][position.nextCol];
                        letterCheck++;
                        if (letterCheck == 4)
                        {
                            xmasCount++;
                        }
                    }
                }
            }

            return xmasCount;
        }

        private bool checkNextLetter(char letter, int position)
        {
            char expected;
            switch (position)
            {
                case 0:
                    expected = 'X';
                    break;
                case 1:
                    expected = 'M';
                    break;
                case 2:
                    expected = 'A';
                    break;
                case 3:
                    expected = 'S';
                    break;
                default:
                    return false;
            }
            if (letter == expected)
            {
                return true;
            }
            return false;
        }

        private (int nextRow, int nextCol) nextPosition((int row, int col) current, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return (current.row - 1, current.col);
                case Direction.Down:
                    return (current.row + 1, current.col);
                case Direction.Left:
                    return (current.row, current.col - 1);
                case Direction.Right:
                    return (current.row, current.col + 1);
                case Direction.UpLeft:
                    return (current.row - 1, current.col - 1);
                case Direction.UpRight:
                    return (current.row - 1, current.col + 1);
                case Direction.DownLeft:
                    return (current.row + 1, current.col - 1);
                case Direction.DownRight:
                    return (current.row + 1, current.col + 1);
                default:
                    return (current.row, current.col);
            }
        }

        private Direction nextDirection (Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.UpLeft,
                Direction.UpLeft => Direction.UpRight,
                Direction.UpRight => Direction.DownLeft,
                Direction.DownLeft => Direction.DownRight,
                _ => Direction.Up
            };
        }

        private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }
}
}
