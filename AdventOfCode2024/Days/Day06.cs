using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day06
    {
        private readonly string[] _input;
        private (int startRow, int startCol) _startPositions;
        private readonly Dictionary<(int row, int col), char> _positions = [];

        public Day06()
        {
            _input = File.ReadAllLines("./Inputs/Day06.txt");
            Setup();
        }

        public Day06(string path)
        {
            _input = File.ReadAllLines(path);
            Setup();
        }

        void Setup()
        {
            var rows = _input.Length;
            var cols = _input[0].ToCharArray().Length;
            var matrix = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = _input[i][j];

                    if (matrix[i, j] == '^')
                    {
                        _startPositions.startRow = i;
                        _startPositions.startCol = j;
                    }

                    _positions.Add((i, j), matrix[i, j]);
                }
            }
        }

        public int PartOne()
        {
            var currentDirection = Direction.Up;
            var currentPosition =
                _positions.Keys.FirstOrDefault(x => x.row == _startPositions.startRow && x.col == _startPositions.startCol);
            HashSet<(int row, int col)> visited = [];
            visited.Add(currentPosition);

            while (currentPosition.row > 0 && currentPosition.row < _input.Length - 1 && currentPosition.col > 0 &&
                   currentPosition.col < _input[0].ToCharArray().Length - 1)
            {
                var checkNextPosition = MoveNextPosition(currentPosition, currentDirection);

                if (_positions[checkNextPosition] == '#')
                {
                    currentDirection = MoveNextDirection(currentDirection);
                    currentPosition = MoveNextPosition(currentPosition, currentDirection);
                }
                else
                {
                    currentPosition = checkNextPosition;
                }

                visited.Add(currentPosition);
            }

            return visited.Count;
        }

        public int PartTwo()
        {
            var currentDirection = Direction.Up;
            var currentPosition = _positions.Keys.FirstOrDefault(x => x.row == _startPositions.startRow && x.col == _startPositions.startCol);
            HashSet<(int row, int col)> visited = [];
            HashSet<(int row, int col)> obstruction = [];
            visited.Add(currentPosition);

            while (currentPosition.row > 0 && currentPosition.row < _input.Length - 1 && currentPosition.col > 0 &&
                   currentPosition.col < _input[0].ToCharArray().Length - 1)
            {
                var checkNextPosition = MoveNextPosition(currentPosition, currentDirection);

                if (_positions[checkNextPosition] == '#')
                {
                    currentDirection = MoveNextDirection(currentDirection);
                    currentPosition = MoveNextPosition(currentPosition, currentDirection);
                }
                else
                {
                    currentPosition = checkNextPosition;

                    bool obstructionLoop = obstructionPath();
                    if (obstructionLoop)
                    {
                        obstruction.Add(currentPosition);
                    }
                }

                visited.Add(currentPosition);
            }

            return obstruction.Count;
        }

        private bool obstructionPath()
        {

            return false;
        }

        private Direction MoveNextDirection(Direction currentDirection)
        {
            return currentDirection switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                _ => Direction.Up
            };
        }

        private (int nextRow, int nextCol) MoveNextPosition((int row, int col) currentPosition, Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    return (currentPosition.row - 1, currentPosition.col);
                case Direction.Right:
                    return (currentPosition.row, currentPosition.col + 1);
                case Direction.Down:
                    return (currentPosition.row + 1, currentPosition.col);
                case Direction.Left:
                default:
                    return (currentPosition.row, currentPosition.col - 1);
            }
        }

        enum Direction
        {
            Up,
            Down,
            Left,
            Right,
        }
    }
}
