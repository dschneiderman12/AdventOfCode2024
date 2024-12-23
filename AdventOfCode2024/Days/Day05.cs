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
        private static Dictionary<int, List<int>> OrderRules { get; set; } = new Dictionary<int, List<int>>();
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

        public int PartOne(List<List<int>> updatesList)
        {
            List<int> middles = new List<int>();

            foreach (List<int> update in updatesList)
            {
                middles.Add(checkUpdateList(update));
            }

            return middles.Sum();
        }

        private int checkUpdateList(List<int> update)
        {
            int addToTotal = 0;
            bool updateOK = true;

            foreach (int num in update)
            {
                if (updateOK)
                {
                    List<int>? rule = OrderRules.TryGetValue(num, out List<int>? value) ? value : null;

                    for (int i = update.IndexOf(num) + 1; i < update.Count; i++)
                    {
                        if (rule != null && !rule.Contains(update[i]))
                        {
                            updateOK = false;
                            break;
                        }

                        //if (i == update.Count - 1)
                        //{
                        //    List<int>? finalRule = OrderRules.TryGetValue(update[i], out List<int>? _) ? value : null;

                        //    if (finalRule != null && finalRule.Contains(update[i - 1]))
                        //    {
                        //        updateOK = false;
                        //    }                        
                        //}
                    }
                }
                else
                {
                    break;
                }
            }

            if (updateOK)
            {
                int middleIndex = (update.Count - 1) / 2;
                addToTotal += update[middleIndex];
            }

            return addToTotal;
        }
    }
}
