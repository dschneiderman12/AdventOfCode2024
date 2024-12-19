using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day02
    {
        public List<List<int>> NumLists { get; set; } = new List<List<int>>();

        public void Setup()
        {
            string filePath = @"Inputs\Day02.txt";
            var input = Path.Combine(AppContext.BaseDirectory, filePath);

            StreamReader sr = new StreamReader(input);
            string line = sr.ReadLine();

            while (line != null)
            {
                List<int> intList = line.Split(" ").Select(int.Parse).ToList();
                NumLists.Add(intList);

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
                bool increasing;
                bool dampened = false;
                int fixCount = 0;

                for (int i = 0; i < nums.Count - 1; i++)
                {
                    increasing = nums[0] < nums[1]; // determine initially if increasing or decreasing based on first two numbers

                    if (!checkIfSafe(nums[i], nums[i + 1], increasing)) // if normal steps from part one don't work, attempt number removals rather than breaking loop
                    {
                        dampened = true;
                        fixCount = attemptToDampen(nums);
                    }

                    if (dampened && fixCount == 0) // if dampening didn't work, move on to next number list
                    {
                        break;
                    }

                    if (i == nums.Count - 2 || (fixCount > 0))  // add to safe count if no unsafe conditions are met and we get to the check of the last two numbers, or if a fix was found during dampening
                    {
                        safeCount++;
                        break; // need to break here so we don't double count the same list that had more than one fix
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

        private int attemptToDampen(List<int> nums)
        {
            int fixCount = 0;

            for (int j = 0; j < nums.Count; j++)
            {
                int removedNum = nums[j]; // remove current number to try again, stored to add back
                nums.RemoveAt(j);
                bool increasing = nums[0] < nums[1]; // reset increasing check after number removal

                for (int k = 0; k < nums.Count - 1; k++)
                {
                    if (!checkIfSafe(nums[k], nums[k + 1], increasing))
                    {
                        break;
                    }
                    if (k == nums.Count - 2)  // add to fix count if no unsafe conditions are met and we get to the check of the last two numbers
                    {
                        fixCount++;
                    }
                }

                if (fixCount > 0) // if fix was found, no need to try more
                {
                    break; 
                }

                nums.Insert(j, removedNum); // add removed number back for next attempt
            }

            return fixCount;
        }
    }
}
