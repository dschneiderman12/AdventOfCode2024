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
                        var currentPosition = matrix[i][j];
                        var currentIndex = (i, j);
                        bool inBounds = true;

                        while (letterCheck <= 3 && inBounds && checkNextLetter(currentPosition, letterCheck))
                        {
                            var nextPos = nextPosition(currentIndex, currentDirection);
                            inBounds = nextPos != currentIndex;

                            currentIndex = (nextPos.nextRow, nextPos.nextCol);
                            currentPosition = matrix[nextPos.nextRow][nextPos.nextCol];

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

        public int PartTwo(List<char[]> matrix)
        {
            int xmasCount = 0;

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Direction[] directionArray = [Direction.UpLeft, Direction.DownRight, Direction.UpRight, Direction.DownLeft];
                    int directionCount = 0;
                    bool confirmedXmas = false;
                    bool inBounds = true;

                    while (matrix[i][j] == 'A' && directionCount <= 3 && !confirmedXmas && inBounds)
                    {
                        var currentIndex = (i, j);
                        char previousLetter = 'X';
                        char currentLetter;
                        int okCheck = 0;

                        foreach (Direction direction in directionArray)
                        {
                            var nextPos = nextPosition(currentIndex, direction);
                            inBounds = nextPos != currentIndex;

                            if (inBounds)
                            {
                                if (directionCount == 0 || directionCount == 2)
                                {
                                    previousLetter = matrix[nextPos.nextRow][nextPos.nextCol];
                                }
                                else
                                {
                                    currentLetter = matrix[nextPos.nextRow][nextPos.nextCol];
                                    if (compareLetters(previousLetter, currentLetter))
                                        okCheck++;
                                }
                            }

                            directionCount++;
                        }

                        if (okCheck == 2)
                        {
                            xmasCount++;
                        }
                    }
                }
            }

            return xmasCount;
        }

        private bool compareLetters(char previous, char current)
        {
            if ((previous == 'M' && current == 'S') || (previous == 'S' && current == 'M'))
            {
                return true;
            }
            return false;
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
