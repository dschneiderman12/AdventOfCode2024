using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day02
    {
        public List<List<int>> numLists { get; set; } = new List<List<int>>();

        public void Setup()
        {  
            string filePath = @"Inputs\Day02.txt";
            var input = Path.Combine(AppContext.BaseDirectory, filePath);

            StreamReader sr = new StreamReader(input);
            string line = sr.ReadLine();

            while (line != null)
            {
                List<int> intList = line.Split(" ").Select(int.Parse).ToList();
                numLists.Add(intList);

                line = sr.ReadLine();
            }

            sr.Close();
        }

        public int PartOne(List<List<int>> numLists)
        {
            int safeCount = 0;

            foreach (List<int> nums in numLists)
            {
                bool increasing = nums[0] < nums[1]; // determine initially if increasing or decreasing based on first two numbers

                for (int i = 0; i < nums.Count - 1; i++)
                {
                    if (!checkIfSafe(nums[i], nums[i + 1], increasing))
                    {
                        break;
                    }
                    if (i == nums.Count - 2)  // add to safe count if no unsafe conditions are met and we get to the check of the last two numbers
                    {
                        safeCount++;
                    }
                }
            }

            return safeCount;
        }

        public int PartTwo(List<List<int>> numLists)
        {
            int safeCount = 0;

            foreach (List<int> nums in numLists)
            {
                bool increasing = nums[0] < nums[1]; // determine initially if increasing or decreasing based on first two numbers
                bool dampened = false;

                for (int i = 0; i < nums.Count - 1; i++)
                {
                    if (!checkIfSafe(nums[i], nums[i + 1], increasing))
                    {
                        if (!dampened)
                        {
                            dampened = true;
                            if (i == 0)
                            {
                                increasing = nums[1] < nums[2];
                            }
                            int removedNum = nums[i];
                            nums.Remove(removedNum);

                        }
                        else
                        {
                            break;
                        }
                    }

                    if (i == nums.Count - 2)  // add to safe count if no unsafe conditions are met and we get to the check of the last two numbers
                    {
                        safeCount++;
                    }
                }
            }

            return safeCount;
        }

        private bool checkIfSafe(int numOne, int numTwo, bool increasing)
        {
            if (numOne == numTwo) // if it did not increase or decrease between two numbers
            {
                return false;
            }
            if ((numOne < numTwo && !increasing) || (numOne > numTwo && increasing)) // if next series of two numbers are opposite of increasing or decreasing set at the beginning
            {
                return false;
            }
            if (Math.Abs(numOne - numTwo) > 3) // if the difference of two numbers in series is more than 3
            {
                return false;
            }
            return true;
        }
    }
}
