using System;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = System.IO.File.ReadAllLines("input.txt");
            int oldValue = int.MaxValue;
            int total = 0;
            foreach (string item in data)
            {
                if(oldValue < int.Parse(item))
                    total++;
                oldValue = int.Parse(item);
            }
            Console.WriteLine("The total for part i:  " + total);

            int previous = int.MaxValue;
            int greaterSums = 0;
            for(int i = 0; i < data.Length - 2; i++){
                int totalsum = 0;
                for(int j = i; j < i + 3; j++){
                    totalsum += int.Parse(data[j]);
                }
                if(previous < totalsum)
                    greaterSums++;
                previous= totalsum;

            }
            Console.WriteLine("The total for part ii: " + greaterSums);
        }
    }
}
