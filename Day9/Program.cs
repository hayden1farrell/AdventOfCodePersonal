using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] gasValues = ParseData();
            
            Part1(gasValues);
            Part2(gasValues);
        }

        static void Part1(int[,] gasValues){
            int sum = 0;
            for(int y = 0; y < gasValues.GetLength(0); y++){
                for(int x = 0; x < gasValues.GetLength(1); x++){
                    bool lowPoint = CheckNear(gasValues, x, y);
                    if(lowPoint == true){
                        sum += (gasValues[y,x]) + 1;
                    }
                }
            }
            Console.WriteLine($"SUM: {sum}");
        }
  
        static List<string> GetLowPoints(int[,] gasValues){
            List<string> lowPoints = new List<string>();
            for(int y = 0; y < gasValues.GetLength(0); y++){
                for(int x = 0; x < gasValues.GetLength(1); x++){
                    bool lowPoint = CheckNear(gasValues, x, y);
                    if(lowPoint == true){
                        lowPoints.Add($"{y},{x}");
                    }
                }
            }
            return lowPoints;
        }

        static bool isValid(int[,] screen, int m, int n, int x, int y, int prevC, int newC)
        {
            if(x < 0 || x >= m || y < 0 || y >= n || screen[x, y] == 9 || screen[x,y]== newC)
                return false;
            return true;
        }

    static int floodFill(int[,] screen, int m, int n, int x, int y, int prevC, int newC)
    {
        int size = 0;
        List<Tuple<int,int>> queue = new List<Tuple<int,int>>();
  
        // Append the position of starting
        // pixel of the component
        queue.Add(new Tuple<int,int>(x, y));
  
        // Color the pixel with the new color
        //screen[x,y] = newC;
  
        // While the queue is not empty i.e. the
        // whole component having prevC color
        // is not colored with newC color
        while(queue.Count > 0)
        {
            // Dequeue the front node
            Tuple<int,int> currPixel = queue[queue.Count - 1];
            queue.RemoveAt(queue.Count - 1);
  
            int posX = currPixel.Item1;
            int posY = currPixel.Item2;
  
            // Check if the adjacent
            // pixels are valid
            if(isValid(screen, m, n, posX + 1, posY, prevC, newC))
            {
                // Color with newC
                // if valid and enqueue
                screen[posX + 1,posY] = newC;
                size++;
                queue.Add(new Tuple<int,int>(posX + 1, posY));
            }
  
            if(isValid(screen, m, n, posX-1, posY, prevC, newC))
            {
                screen[posX-1,posY]= newC;
                size++;
                queue.Add(new Tuple<int,int>(posX-1, posY));
            }
  
            if(isValid(screen, m, n, posX, posY + 1, prevC, newC))
            {
                screen[posX,posY + 1]= newC;
                size++;
                queue.Add(new Tuple<int,int>(posX, posY + 1));
            }
  
            if(isValid(screen, m, n, posX, posY-1, prevC, newC))
            {
                size++;
                screen[posX,posY-1]= newC;
                queue.Add(new Tuple<int,int>(posX, posY-1));
            }
        }
        return size;
    }
        static void Part2(int[,] gasValues){
            List<string> lowPoints = GetLowPoints(gasValues);
            List<int> basinsSize = new List<int>();
            int m = gasValues.GetLength(0);
        
            // Column of the display
            int n = gasValues.GetLength(1);
        
            for(int i = 0; i < lowPoints.Count; i++){
                string[] currentLow = lowPoints[i].Split(",");
                // Co-ordinate provided by the user
                int x = Convert.ToInt32(currentLow[1].ToString());
                int y = Convert.ToInt32(currentLow[0].ToString());

                // Current color at that co-ordinate
                int prevC = gasValues[y,x];
            
                // New color that has to be filled
                int newC = 22;
                int size = floodFill(gasValues, m, n, y, x, prevC, newC);
                basinsSize.Add(size);
            }
            
            basinsSize.Sort();

            Console.WriteLine("Largest basins: " + basinsSize.Last() * basinsSize[basinsSize.Count - 2] * basinsSize[basinsSize.Count - 3]);
        }
        static bool CheckNear(int[,] gasvalues, int x, int y){
            int currentPoint = gasvalues[y,x];

            if(x > 0){  // skips all values on the left hand side
                int leftPoint = gasvalues[y,x-1];
                if(leftPoint <= currentPoint)
                    return false;
            }
            if(x + 1 < gasvalues.GetLength(1)){  // doesnt check for values on the right wall
                int rightPoint = gasvalues[y,x+1];
                if(rightPoint <= currentPoint)
                    return false;
            }
            if(y > 0){  // skips all values on the left hand side
                int topPoint = gasvalues[y-1,x];
                if(topPoint <= currentPoint)
                    return false;
            }
            if(y + 1 < gasvalues.GetLength(0)){  // doesnt check for values on the right wall
                int bottomPoint = gasvalues[y+1,x];
                if(bottomPoint <= currentPoint)
                    return false;
            }
            return true;
        }

        static int[,] ParseData(){
            int[,] gas = new int[100,100];
            string filename = "input.txt";
            int y =0;
            using (StreamReader reader = File.OpenText(filename))
            {
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    int x =0;
                    foreach (var value in line)
                    {
                        gas[y,x] = Convert.ToInt32(value.ToString());
                        x++;
                    }
                    y++;
                }
            }   
            return gas;
        }
    }
}
