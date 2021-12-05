using System;
using System.Collections.Generic;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> coordinates = ParseData();
            CoordinatesToLines(coordinates);
        }

        private static void CoordinatesToLines(List<string> coordinates)
        {
            int x1, x2, y1, y2 = 0;
            List<string> linesFromPoints = new List<string>();
            for (int i = 0; i < coordinates.Count; i++)
            {
                string[] spots = coordinates[i].Split(" ");
                x1 = int.Parse(spots[0]); y1 = int.Parse(spots[1]); x2 = int.Parse(spots[2]); y2 = int.Parse(spots[3]);
                string coordinate = "";

                if(x1 == x2){   // if the x axis has no change
                    bool looking = true;
                    if(y1 < y2) coordinate = $"{x1},{y1}";
                    else coordinate = $"{x1},{y2}";

                    linesFromPoints.Add(coordinate);
                    while(looking == true){
                        if(y1 + 1 <=  y2)    // if the first coordinate is less
                        {
                            y1++;
                            coordinate = $"{x1},{y1}";
                        }
                        else if(y2 + 1 <=  y1){
                            y2++;
                            coordinate = $"{x1},{y2}";
                        }else{
                            looking = false;
                            break;
                        }
                        linesFromPoints.Add(coordinate);
                    }       
                }
                else if(y1 == y2){   // if the y axis has no change
                    bool looking = true;

                    if(x1 < x2) coordinate = $"{x1},{y1}";
                    else coordinate = $"{x2},{y1}";

                    linesFromPoints.Add(coordinate);
                    while(looking == true){
                        if(x1 + 1 <=  x2)    // if the first coordinate is less
                        {
                            x1++;
                            coordinate = $"{x1},{y1}";
                        }
                        else if(x2 + 1 <=  x1){
                            x2++;
                            coordinate = $"{x2},{y1}";
                        }else{
                            looking = false;
                            break;
                        }
                        linesFromPoints.Add(coordinate);
                    }       
                }
                else{ // diagonal
                    bool looking = true;

                    coordinate = $"{x1},{y1}";
                    linesFromPoints.Add(coordinate);
                    coordinate = $"{x2},{y2}";
                    linesFromPoints.Add(coordinate);

                    while(looking == true){
                        if(x1 + 1 <  x2)    // if the first coordinate is less
                        {
                            if(y1 + 1 <=  y2){
                                y1++;
                            }                        
                            else if(y1 + 1 >=  y2){
                                y1--;
                            }
                            x1++; 
                            coordinate = $"{x1},{y1}";
                        }
                        else if(x2 + 1 <  x1){
                            if(y1 + 1 <=  y2){
                                y1++;
                            }                        
                            else if(y1 + 1 >=  y2){
                                y1--;
                            }
                            x1--;
                            coordinate = $"{x1},{y1}";
                        }else{
                            looking = false;
                            break;
                        }
                        //Console.WriteLine(coordinate);
                        //Console.ReadKey();
                        linesFromPoints.Add(coordinate);
                    }   
                }
            }
            int pointsOfIntersection = 0;

            List<string> SeenPoints = new List<string>();

            for (int i = 0; i < linesFromPoints.Count; i++)
            {
                for (int j = i+1; j < linesFromPoints.Count; j++)
                {
                    if(linesFromPoints[i] == linesFromPoints[j]){
                        if(SeenPoints.Contains(linesFromPoints[j]) == false){
                            Console.WriteLine(linesFromPoints[j]);
                            SeenPoints.Add(linesFromPoints[j]);
                            pointsOfIntersection++;
                        }
                    }
                }
            }

            Console.WriteLine($"Dangerous at: {pointsOfIntersection} points");
        }

        static List<string> ParseData(){
            string[] data = System.IO.File.ReadAllLines("input.txt");
            List<string> coordinates = new List<string>();
            for (int i = 0; i < data.Length; i++)
            {
                string[] postions = data[i].Split(" -> ");
                
                coordinates.Add($"{postions[0].Split(",")[0]} {postions[0].Split(",")[1]} {postions[1].Split(",")[0]} {postions[1].Split(",")[1]}");
            }

            return coordinates;
        }
    }
}
// 16425