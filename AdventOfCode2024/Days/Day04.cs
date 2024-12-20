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
                    Direction currentDirection = Direction.Up;
                    int directionCount = 0;

                    while (matrix[i][j] == 'X' && directionCount <= 7)
                    {
                        int letterCheck = 0;
                        var currentLetter = matrix[i][j];
                        var currentPosition = (i, j);
                        bool inBounds = true;

                        while (letterCheck <= 3 && inBounds && checkNextLetter(currentLetter, letterCheck))
                        {
                            var nextPos = nextPosition(currentPosition, currentDirection);
                            inBounds = nextPos != currentPosition;

                            currentPosition = (nextPos.nextRow, nextPos.nextCol);
                            currentLetter = matrix[nextPos.nextRow][nextPos.nextCol];

                            letterCheck++;

                            if (letterCheck == 4)
                            {
                                xmasCount++;
                            }
                        }

                        currentDirection = nextDirection(currentDirection);
                        directionCount++;
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
            int nextRow = current.row;
            int nextCol = current.col;

            switch (direction)
            {
                case Direction.Up:
                    if (checkInBounds(nextRow - 1, nextCol))
                        nextRow -= 1;
                    break;

                case Direction.Down:
                    if (checkInBounds(nextRow + 1, nextCol))
                        nextRow += 1;
                    break;

                case Direction.Left:
                    if (checkInBounds(nextRow, nextCol - 1))
                        nextCol -= 1;
                    break;

                case Direction.Right:
                    if (checkInBounds(nextRow, nextCol + 1))
                        nextCol += 1;
                    break;

                case Direction.UpLeft:
                    if (checkInBounds(nextRow - 1, nextCol - 1))
                    {
                        nextRow -= 1;
                        nextCol -= 1;
                    }
                    break;

                case Direction.UpRight:
                    if (checkInBounds(nextRow - 1, nextCol + 1))
                    {
                        nextRow -= 1;
                        nextCol += 1;
                    }
                    break;

                case Direction.DownLeft:
                    if (checkInBounds(nextRow + 1, nextCol - 1))
                    {
                        nextRow += 1;
                        nextCol -= 1;
                    }
                    break;

                case Direction.DownRight:
                    if (checkInBounds(nextRow + 1, nextCol + 1))
                    {
                        nextRow += 1;
                        nextCol += 1;
                    }
                    break;
            }

            return (nextRow, nextCol);
        }

        private bool checkInBounds(int row, int col)
        {
            if (row < matrix.Count && row >= 0 && col < matrix[row].Length && col >= 0)
            {
                return true;
            }
            return false;
        }

        private Direction nextDirection(Direction direction)
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
