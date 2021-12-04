using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = System.IO.File.ReadAllLines("input.txt");
            string gammaRate = "";
            string epslionRate = "";

            for(int horiztonal = 0; horiztonal < data[0].Length; horiztonal++){
                int zeroCount = 0;
                int oneCount = 0;
                for (int vertical = 0; vertical < data.Length; vertical++)
                {
                    string currentLine = data[vertical];
                    char currentBinary = currentLine[horiztonal];

                    if(currentBinary == '0') zeroCount++;
                    else oneCount++;
                }
                if(zeroCount > oneCount){
                    gammaRate += '0';
                    epslionRate += '1';
                }
                else{
                    gammaRate += '1';
                    epslionRate += '0';
                }
            }
            int gammaNumber = Convert.ToInt32(gammaRate, 2);
            int epslionNumber = Convert.ToInt32(epslionRate, 2);
            Console.WriteLine($"Gamma: {gammaNumber} epslion: {epslionNumber} Power: {gammaNumber * epslionNumber}");
            Part2();
        }
        static void Part2(){
            string[] data = System.IO.File.ReadAllLines("input.txt");

            List<string> validData = data.ToList<string>();
            Console.ReadKey();

            while(validData.Count != 1){
                for(int horiztonal = 0; horiztonal < data[0].Length; horiztonal++){
                    int zeroCount = 0;
                    int oneCount = 0;
                    List<int> postionsToRemove = new List<int>();
                    for (int vertical = 0; vertical < data.Length; vertical++)
                    {
                        string currentLine = "";
                        foreach (var item in validData)
                        {
                            currentLine = item;
                            char currentBinary = currentLine[horiztonal];

                            if(currentBinary == '0') zeroCount++;
                            else oneCount++;
                        }
                    }
                    if(oneCount >= zeroCount){
                        int count = validData.Count;
                        for(int i =0; i < count; i++){
                            if(validData[i][horiztonal] == '0'){
                                postionsToRemove.Add(i);
                            }
                        }
                    }
                    else{
                        int count = validData.Count;
                        for(int i =0; i < count; i++){
                            if(validData[i][horiztonal] == '1'){
                                postionsToRemove.Add(i);
                            }
                        }
                    }

                    validData = RemoveFromList(validData, postionsToRemove);
                    if(validData.Count == 1){
                        break;
                    }
                }
            }
            Console.WriteLine($"Complete last one is : {Convert.ToInt32(validData[0], 2)}");
            int co2 = IHateThis();
            Console.WriteLine(co2 * Convert.ToInt32(validData[0], 2));
        }
        static int IHateThis(){
            string[] data = System.IO.File.ReadAllLines("input.txt");

            List<string> validData = data.ToList<string>();
            Console.ReadKey();

            while(validData.Count != 1){
                for(int horiztonal = 0; horiztonal < data[0].Length; horiztonal++){
                    int zeroCount = 0;
                    int oneCount = 0;
                    List<int> postionsToRemove = new List<int>();
                    for (int vertical = 0; vertical < data.Length; vertical++)
                    {
                        string currentLine = "";
                        foreach (var item in validData)
                        {
                            currentLine = item;
                            char currentBinary = currentLine[horiztonal];

                            if(currentBinary == '0') zeroCount++;
                            else oneCount++;
                        }
                    }
                    if(zeroCount <= oneCount){
                        int count = validData.Count;
                        for(int i =0; i < count; i++){
                            if(validData[i][horiztonal] == '1'){
                                postionsToRemove.Add(i);
                            }
                        }
                    }
                    else{
                        int count = validData.Count;
                        for(int i =0; i < count; i++){
                            if(validData[i][horiztonal] == '0'){
                                postionsToRemove.Add(i);
                            }
                        }
                    }
                    validData = RemoveFromList(validData, postionsToRemove);
                                        
                    if(validData.Count == 1){
                        break;
                    }
                }
            }
            Console.WriteLine($"Complete last one is : {Convert.ToInt32(validData[0], 2)}");
            return Convert.ToInt32(validData[0], 2);
        }

        private static List<string> RemoveFromList(List<string> validData, List<int> postionsToRemove)
        {
            int count = postionsToRemove.Count;
            for(int i =0; i < count; i++){
                int index = postionsToRemove[i];
                validData.RemoveAt(index - i);
            }
            return validData;
        }
    }
}
