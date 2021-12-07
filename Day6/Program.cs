using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> fish = ParseData();
            long count = AgeFish(fish);
            Console.WriteLine(count);
        }

        private static void DisplayFish(List<int> fish)
        {
            foreach (var item in fish)
            {
                Console.WriteLine(fish);
            }
        }

        private static long AgeFish(List<int> input)
        {
                Dictionary<int, long> fishBreadingCount = new Dictionary<int, long>()
                {
                    {0, 0},
                    {1, 0 },
                    {2, 0 },
                    {3, 0 },
                    {4, 0 },
                    {5, 0 },
                    {6, 0 },
                    {7, 0 },
                    {8, 0 }
                };
                for (var i = 0; i < input.Count; i++)
                {
                    fishBreadingCount[input[i]]++;
                }

                for (int i = 0; i < 256; i++)
                {
                    long newFishes = fishBreadingCount[0];
                    fishBreadingCount[0] = 0;

                    for (int j = 1; j < fishBreadingCount.Count; j++)
                    {
                        fishBreadingCount[j - 1] = fishBreadingCount[j];
                    }

                    fishBreadingCount[8] = newFishes;
                    fishBreadingCount[6] += newFishes;
                }
                long res = 0;
                foreach (var timer in fishBreadingCount)
                {
                    res += timer.Value;
                }

                return res;
        }

        static List<int> ParseData(){
            string[] data = System.IO.File.ReadAllLines("input.txt");
            List<int> fish = new List<int>();
            string[] startFish = data[0].Split(",");

            foreach (var item in startFish)
            {
                fish.Add(Convert.ToInt32(item));
            }
            return fish;    
        }
    }
}
