using System;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = System.IO.File.ReadAllLines("input.txt");
            int horizontal =0;
            int vertical = 0;

            for (int i = 0; i < data.Length; i++)
            {
                string[] info = data[i].Split(" ");
                if(info[0] == "forward"){
                    horizontal += int.Parse(info[1]);
                }
                if(info[0] == "down"){
                    vertical += int.Parse(info[1]);
                }
                if(info[0] == "up"){
                    vertical -= int.Parse(info[1]);
                }
            }
            Console.WriteLine($"Horizontal: {horizontal} Vertical: {vertical} Output: {horizontal * vertical}");

            horizontal = 0;
            int aim = 0;
            long depth = 0;

            for (int i = 0; i < data.Length; i++)
            {
                string[] info = data[i].Split(" ");
                if(info[0] == "forward"){
                    horizontal += int.Parse(info[1]);
                    depth += int.Parse(info[1]) * aim;
                }
                if(info[0] == "down"){
                    aim += int.Parse(info[1]);
                }
                if(info[0] == "up"){
                    aim -= int.Parse(info[1]);
                }
            }            
            Console.WriteLine($"Horizontal: {horizontal} depth: {depth} aim {aim} Output: {horizontal * depth}");
        }
    }
}
