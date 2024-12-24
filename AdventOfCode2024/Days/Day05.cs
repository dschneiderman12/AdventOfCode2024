﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class Day05
    {
        //private static Dictionary<int, List<int>> OrderRules { get; set; } = new Dictionary<int, List<int>>();
        private static List<List<int>> OrderRules { get; set; } = new List<List<int>>();
        public List<List<int>> UpdatesList { get; set; } = new List<List<int>>();

        public void Setup()
        {
            string filePath = @"Inputs\Day05.txt";
            string fileContent = File.ReadAllText(filePath);
            string[] fileSplit = fileContent.Split(["\r\n\r\n"], StringSplitOptions.RemoveEmptyEntries);

            var rules = fileSplit[0].Split("\r\n");
            var updates = fileSplit[1].Split("\r\n");

            //foreach (string rule in rules)
            //{
            //    var ruleSplit = rule.Split('|').Select(int.Parse).ToList();
            //    if (OrderRules.ContainsKey(ruleSplit[0]))
            //    {
            //        OrderRules[ruleSplit[0]].Add(ruleSplit[1]);
            //    }
            //    else
            //    {
            //        OrderRules.Add(ruleSplit[0], new List<int> { ruleSplit[1] });
            //    }                
            //}

            foreach (string rule in rules)
            {
                var ruleSplit = rule.Split('|').Select(int.Parse).ToList();
                OrderRules.Add(ruleSplit);
            }

            foreach (string update in updates)
            {
                List<int> intList = update.Split(",").Select(int.Parse).ToList();
                UpdatesList.Add(intList);
            }
        }

        //private List<int> sortRules (String[] rulesToSort)
        //{
        //    var resultList = new List<int>();

        //    foreach (string rule in rulesToSort)
        //    {
        //        var ruleSplit = rule.Split('|').Select(int.Parse).ToList();
        //        bool hasFirst = resultList.Contains(ruleSplit[0]);
        //        bool hasSecond = resultList.Contains(ruleSplit[1]);

        //        if (!hasFirst && !hasSecond)
        //        {
        //            resultList.Add(ruleSplit[0]);
        //            resultList.Add(ruleSplit[1]);
        //        }
        //        else if (hasFirst && !hasSecond)
        //        {
        //            resultList.Insert(resultList.IndexOf(ruleSplit[0]) + 1, ruleSplit[1]);
        //        }
        //        else if (!hasFirst)
        //        {
        //            resultList.Insert(resultList.IndexOf(ruleSplit[1]), ruleSplit[0]);
        //        }
        //    }

        //    foreach (string rule in rulesToSort)
        //    {
        //        var ruleSplit = rule.Split('|').Select(int.Parse).ToList();

        //        if (resultList.IndexOf(ruleSplit[0]) > resultList.IndexOf(ruleSplit[1]))
        //        {
        //            resultList.Remove(ruleSplit[0]);
        //            resultList.Insert(resultList.IndexOf(ruleSplit[1]), ruleSplit[0]);
        //        }
        //    }

        //    return resultList;
        //}

        public int PartOne(List<List<int>> updatesList)
        {
            List<int> middles = new List<int>();

            foreach (List<int> update in updatesList)
            {
                if (checkUpdateList(update))
                {
                    int middleIndex = (update.Count - 1) / 2;
                    middles.Add(update[middleIndex]);
                }                
            }

            return middles.Sum();
        }


        private bool checkUpdateList(List<int> update)
        {    
            foreach (int num in update)
            {
                foreach (List<int> rule in OrderRules)
                {
                    if (rule.Contains(num))
                    {
                        for (int i = update.IndexOf(num); i < update.Count; i++)
                        {
                            if (rule.Contains(update[i]) &&
                                (rule.IndexOf(num) > rule.IndexOf(update[i])))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }          
    }
}
