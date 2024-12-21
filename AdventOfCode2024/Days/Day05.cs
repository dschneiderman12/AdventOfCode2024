using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day05
    {
        public Dictionary<int, List<int>> OrderRules { get; set; } = new Dictionary<int, List<int>>();
        public List<List<int>> UpdatesList { get; set; } = new List<List<int>>();

        public void Setup()
        {
            string filePath = @"Inputs\Day05.txt";
            string fileContent = File.ReadAllText(filePath);
            string[] fileSplit = fileContent.Split(["\r\n\r\n"], StringSplitOptions.RemoveEmptyEntries);

            var rules = fileSplit[0].Split("\r\n");

            var updates = fileSplit[1].Split("\r\n");

            foreach (string rule in rules)
            {
                var ruleSplit = rule.Split('|').Select(int.Parse).ToList();
                if (OrderRules.ContainsKey(ruleSplit[0]))
                {
                    OrderRules[ruleSplit[0]].Add(ruleSplit[1]);
                }
                else
                {
                    OrderRules.Add(ruleSplit[0], new List<int> { ruleSplit[1] });
                }                
            }

            foreach (string update in updates)
            {
                List<int> intList = update.Split(",").Select(int.Parse).ToList();
                UpdatesList.Add(intList);
            }
        }

        public int PartOne(Dictionary<int, List<int>> orderRules, List<List<int>> updatesList)
        {
            List<int> middles = new List<int>();




            return middles.Sum();
        }
    }
}
